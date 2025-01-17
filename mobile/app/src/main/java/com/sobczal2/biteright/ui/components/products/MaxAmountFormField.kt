package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.clickable
import androidx.compose.foundation.focusable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Card
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusDirection
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.IntSize
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.dto.units.UnitSystemDto
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.units.SimplifiedUnitItem
import com.sobczal2.biteright.ui.components.units.UnitListItem
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.extraSmallTop
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.Locale
import java.util.UUID

data class FormMaxAmountWithUnit(
    val maxAmount: Double?, val unit: UnitDto
) {
    companion object {
        val Empty = FormMaxAmountWithUnit(
            maxAmount = null,
            unit = UnitDto.Empty
        )
    }
}

data class MaxAmountFormFieldState(
    var value: FormMaxAmountWithUnit,
    val error: String? = null,
)

@Composable
fun MaxAmountFormField(
    state: MaxAmountFormFieldState,
    onChange: (FormMaxAmountWithUnit) -> Unit,
    searchUnits: suspend (String, PaginationParams) -> PaginatedList<UnitDto>,
    modifier: Modifier = Modifier
) {
    val focusManager = LocalFocusManager.current

    var dialogOpen by remember { mutableStateOf(false) }
    var maxAmountTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    val maxAmountTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    LaunchedEffect(state.value) {
        if (state.value.maxAmount != null && maxAmountTextFieldState.value != "%.2f".format(state.value.maxAmount))
            maxAmountTextFieldState = maxAmountTextFieldState.copy(
                value = "%.2f".format(Locale.US, state.value.maxAmount)
            )
    }

    Column(
        modifier = modifier
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmallTop,
            modifier = Modifier
        ) {
            Row(
                modifier = Modifier,
                verticalAlignment = Alignment.CenterVertically,
            ) {
                var textFieldSize by remember {
                    mutableStateOf(IntSize.Zero)
                }
                TextFormField(
                    state = maxAmountTextFieldState,
                    onChange = {
                        if (it.isEmpty() || maxAmountTypingRegex.matches(it)) {
                            maxAmountTextFieldState = maxAmountTextFieldState.copy(
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
                            Text(text = state.value.unit.abbreviation)
                        },
                        keyboardOptions = KeyboardOptions.Default.copy(
                            keyboardType = KeyboardType.Number,
                            imeAction = ImeAction.Next,
                        ),
                    ),
                    modifier = Modifier
                        .weight(0.6f)
                        .onFocusChanged {
                            if (!it.isFocused) {
                                maxAmountTextFieldState =
                                    when (val price =
                                        maxAmountTextFieldState.value.toDoubleOrNull()) {
                                        null -> {
                                            maxAmountTextFieldState.copy(
                                                value = "",
                                            )
                                        }

                                        else -> {
                                            maxAmountTextFieldState.copy(
                                                value = "%.2f".format(Locale.US, price)
                                            )
                                        }
                                    }
                                onChange(
                                    state.value.copy(
                                        maxAmount = maxAmountTextFieldState.value.toDoubleOrNull()
                                    )
                                )
                            }
                        }
                        .onGloballyPositioned {
                            textFieldSize = it.size
                        }
                )

                val focusRequester = remember {
                    FocusRequester()
                }
                var focused by remember { mutableStateOf(false) }
                val color =
                    if (focused) TextFieldDefaults.colors().focusedLabelColor else TextFieldDefaults.colors().unfocusedLabelColor

                Box(
                    modifier = Modifier
                        .weight(0.4f)
                        .height(
                            with(LocalDensity.current) {
                                textFieldSize.height.toDp()
                            }
                        )
                        .clickable {
                            dialogOpen = true
                            focusRequester.requestFocus()
                        }
                        .focusRequester(focusRequester)
                        .onFocusChanged {
                            focused = it.isFocused
                        }
                        .focusable(),
                    contentAlignment = Alignment.BottomStart
                ) {
                    Box(
                        modifier = Modifier
                            .fillMaxSize(),
                        contentAlignment = Alignment.CenterStart
                    ) {
                        SimplifiedUnitItem(
                            unit = state.value.unit,
                            selected = false,
                            label = stringResource(id = R.string.unit),
                            labelColor = color,
                            modifier = Modifier.padding(start = MaterialTheme.dimension.sm)
                        )
                    }

                    HorizontalDivider(
                        thickness = if (focused) 2.dp else 1.dp,
                        color = color
                    )
                }
            }
        }
        if (state.error != null) {
            Text(
                text = state.error,
                style = MaterialTheme.typography.bodySmall,
                color = MaterialTheme.colorScheme.error,
                modifier = Modifier.padding(start = MaterialTheme.dimension.sm)
            )
        }
    }

    if (dialogOpen) {
        SearchDialog(
            search = searchUnits,
            keySelector = { it.id },
            onDismissRequest = { dialogOpen = false },
            selectedItem = state.value.unit,
        ) { unit, selected ->
            UnitListItem(unit = unit, selected = selected, modifier = Modifier.clickable {
                focusManager.moveFocus(FocusDirection.Next)
                onChange(
                    state.value.copy(
                        unit = unit
                    )
                )
                dialogOpen = false
            })
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
        ), UnitDto(
            id = UUID.randomUUID(),
            name = "Kilogram",
            abbreviation = "kg",
            unitSystem = UnitSystemDto.Metric
        ), UnitDto(
            id = UUID.randomUUID(),
            name = "Pound",
            abbreviation = "lb",
            unitSystem = UnitSystemDto.Metric
        )
    )

    BiteRightTheme {
        MaxAmountFormField(state = MaxAmountFormFieldState(
            value = FormMaxAmountWithUnit(
                maxAmount = 20.0, unit = units.first()
            ), error = "Amount is required"
        ), onChange = {}, searchUnits = { _, _ ->
            PaginatedList(
                items = units,
                pageNumber = 0,
                pageSize = 3,
                totalPages = 1,
                totalCount = 3,
            )
        })
    }
}