package com.sobczal2.biteright.ui.components.amounts

import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.interaction.collectIsPressedAsState
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material.icons.filled.KeyboardArrowUp
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.dto.units.UnitSystemDto
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

data class FormAmountWithUnit(
    val amount: Double?,
    val unit: UnitDto?
)

data class AmountFormFieldState(
    var value: FormAmountWithUnit = FormAmountWithUnit(
        null,
        null
    ),
    val amountError: String? = null,
    val unitError: String? = null,
    val availableUnits: List<UnitDto> = emptyList()
)

@Composable
fun AmountFormField(
    state: AmountFormFieldState,
    onChange: (FormAmountWithUnit) -> Unit,
    modifier: Modifier = Modifier
) {
    var dropDownExpanded by remember { mutableStateOf(false) }
    var amountTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    val dropDownTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    var selectedUnit by remember {
        mutableStateOf<UnitDto?>(null)
    }

    val amountTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    var amountFieldFocused by remember { mutableStateOf(false) }

    LaunchedEffect(state.value) {
        selectedUnit = state.value.unit
        if (!amountFieldFocused) {
            amountTextFieldState = amountTextFieldState.copy(
                value = if (state.value.amount != null) {
                    String.format("%.2f", state.value.amount)
                } else {
                    ""
                }
            )
        }
    }

    LaunchedEffect(keys = arrayOf(amountTextFieldState, selectedUnit)) {
        onChange(
            FormAmountWithUnit(
                amount = amountTextFieldState.value.toDoubleOrNull(),
                unit = selectedUnit
            )
        )
    }

    Row(
        modifier = modifier
    ) {
        TextFormField(
            state = amountTextFieldState.copy(
                error = state.amountError
            ),
            onChange = {
                if (it.isEmpty() || amountTypingRegex.matches(it)) {
                    amountTextFieldState = amountTextFieldState.copy(
                        value = it
                    )
                }
            },
            options = TextFormFieldOptions(
                label = { Text(text = stringResource(id = R.string.amount)) },
                shape = MaterialTheme.shapes.extraSmall.copy(
                    topEnd = CornerSize(0.dp),
                    bottomEnd = CornerSize(0.dp),
                    bottomStart = CornerSize(0.dp)
                ),
                trailingIcon = {
                    Text(text = state.value.unit?.abbreviation ?: "")
                },
                keyboardOptions = KeyboardOptions.Default.copy(
                    keyboardType = KeyboardType.Number
                ),
            ),
            modifier = Modifier
                .weight(0.5f)
                .onFocusChanged {
                    amountFieldFocused = it.isFocused
                    if (!it.isFocused) {
                        amountTextFieldState =
                            when (val price = amountTextFieldState.value.toDoubleOrNull()) {
                                null -> {
                                    amountTextFieldState.copy(
                                        value = "",
                                    )
                                }

                                else -> {
                                    amountTextFieldState.copy(
                                        value = String.format("%.2f", price)
                                    )
                                }

                            }
                    }
                }
        )
        Column(
            modifier = Modifier
                .weight(0.5f)
        ) {
            var textFieldSize by remember { mutableStateOf(Size.Zero) }
            val interactionSource = remember { MutableInteractionSource() }
            val isPressed: Boolean by interactionSource.collectIsPressedAsState()

            LaunchedEffect(isPressed) {
                if (isPressed) {
                    dropDownExpanded = true
                }
            }

            TextFormField(
                state = dropDownTextFieldState.copy(
                    value = state.value.unit?.name ?: stringResource(id = R.string.none),
                    error = state.unitError
                ),
                onChange = {},
                options = TextFormFieldOptions(
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topStart = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp),
                        bottomEnd = CornerSize(0.dp)
                    ),
                    trailingIcon = {
                        Icon(
                            imageVector = if (dropDownExpanded) {
                                Icons.Default.KeyboardArrowUp
                            } else {
                                Icons.Default.KeyboardArrowDown
                            },
                            contentDescription = null
                        )
                    },
                    readOnly = true,
                    interactionSource = interactionSource,
                    colors = TextFieldDefaults.colors()
                        .copy(
                            errorTextColor = MaterialTheme.colorScheme.error
                        ),
                ),
                modifier = Modifier
                    .fillMaxWidth()
                    .onGloballyPositioned {
                        textFieldSize = it.size.toSize()
                    }
            )

            DropdownMenu(
                expanded = dropDownExpanded,
                onDismissRequest = {
                    dropDownExpanded = false
                },
                modifier = Modifier
                    .width(with(LocalDensity.current) { textFieldSize.width.toDp() })
            ) {
                DropdownMenuItem(
                    leadingIcon = if (state.value.unit == null) {
                        {
                            Icon(Icons.Default.Done, contentDescription = null)
                        }
                    } else {
                        null
                    },
                    text = {
                        Text(text = stringResource(id = R.string.none))
                    },
                    onClick = {
                        onChange(
                            state.value.copy(
                                unit = null
                            )
                        )
                        dropDownExpanded = false
                    },
                )
                state.availableUnits.forEach { unit ->
                    DropdownMenuItem(
                        leadingIcon = if (unit == state.value.unit) {
                            {
                                Icon(Icons.Default.Done, contentDescription = null)
                            }
                        } else {
                            null
                        },
                        text = {
                            Text(text = "${unit.name} (${unit.abbreviation})")
                        },
                        onClick = {
                            dropDownExpanded = false
                            onChange(
                                state.value.copy(
                                    unit = unit
                                )
                            )
                        },
                    )
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun AmountFormFieldPreview() {
    val units = listOf(
        UnitDto(
            id = UUID.randomUUID(),
            name = "Gram",
            abbreviation = "g",
            unitSystem = UnitSystemDto.Metric
        ),
        UnitDto(
            id = UUID.randomUUID(),
            name = "Kilogram",
            abbreviation = "kg",
            unitSystem = UnitSystemDto.Metric
        ),
        UnitDto(
            id = UUID.randomUUID(),
            name = "Pound",
            abbreviation = "lb",
            unitSystem = UnitSystemDto.Metric
        )
    )

    BiteRightTheme {
        AmountFormField(
            state = AmountFormFieldState(
                value = FormAmountWithUnit(
                    amount = 20.0,
                    unit = units.first()
                ),
                availableUnits = units
            ),
            onChange = {}
        )
    }
}