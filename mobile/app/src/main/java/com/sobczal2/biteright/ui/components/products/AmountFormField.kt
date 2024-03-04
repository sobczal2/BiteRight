package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.clickable
import androidx.compose.foundation.focusable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
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
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.graphics.RectangleShape
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.IntSize
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.units.SimplifiedUnitItem
import com.sobczal2.biteright.ui.components.units.UnitListItem
import com.sobczal2.biteright.ui.theme.dimension
import java.util.Locale

data class FormAmountWithUnit(
    val currentAmount: Double?,
    val maxAmount: Double?,
    val unit: UnitDto
) {
    companion object {
        val Empty = FormAmountWithUnit(
            maxAmount = null,
            currentAmount = null,
            unit = UnitDto.Empty
        )
    }
}

data class AmountFormFieldState(
    var value: FormAmountWithUnit,
    val error: String? = null,
)

@Composable
fun AmountFormField(
    state: AmountFormFieldState,
    onChange: (FormAmountWithUnit) -> Unit,
    searchUnits: suspend (String, PaginationParams) -> PaginatedList<UnitDto>,
    modifier: Modifier = Modifier
) {
    var dialogOpen by remember { mutableStateOf(false) }
    var currentAmountTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = state.value.currentAmount?.let { "%.2f".format(Locale.US, it) } ?: ""
            )
        )
    }
    var maxAmountTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = state.value.maxAmount?.let { "%.2f".format(Locale.US, it) } ?: ""
            )
        )
    }

    val amountTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    LaunchedEffect(state.value) {
        currentAmountTextFieldState = TextFormFieldState(
            value = state.value.currentAmount?.let { "%.2f".format(Locale.US, it) } ?: ""
        )
        maxAmountTextFieldState = TextFormFieldState(
            value = state.value.maxAmount?.let { "%.2f".format(Locale.US, it) } ?: ""
        )
    }

    Column(
        modifier = modifier
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmall.copy(
                bottomStart = CornerSize(0.dp),
            ),
            modifier = Modifier
        ) {
            Row(
                modifier = Modifier,
                verticalAlignment = Alignment.CenterVertically,
            ) {
                var columnSize by remember {
                    mutableStateOf(IntSize.Zero)
                }
                Column(
                    modifier = Modifier
                        .weight(0.6f)
                        .onGloballyPositioned {
                            columnSize = it.size
                        }
                ) {
                    TextFormField(
                        state = currentAmountTextFieldState,
                        onChange = {
                            if (it.isEmpty() || amountTypingRegex.matches(it)) {
                                currentAmountTextFieldState = currentAmountTextFieldState.copy(
                                    value = it
                                )
                            }
                        },
                        options = TextFormFieldOptions(
                            label = { Text(text = stringResource(id = R.string.current_amount)) },
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
                                imeAction = ImeAction.Next
                            ),
                        ),
                        modifier = Modifier
                            .onFocusChanged { focusState ->
                                if (!focusState.isFocused) {
                                    val newValue = recalculateStateForCurrentValue(
                                        currentAmountTextFieldState.value,
                                        state.value
                                    )
                                    if (newValue != state.value) {
                                        onChange(newValue)
                                    } else {
                                        currentAmountTextFieldState =
                                            currentAmountTextFieldState.copy(
                                                value = newValue.currentAmount?.let {
                                                    "%.2f".format(
                                                        Locale.US,
                                                        it
                                                    )
                                                } ?: ""
                                            )
                                    }
                                }
                            }
                    )
                    TextFormField(
                        state = maxAmountTextFieldState,
                        onChange = {
                            if (it.isEmpty() || amountTypingRegex.matches(it)) {
                                maxAmountTextFieldState = maxAmountTextFieldState.copy(
                                    value = it
                                )
                            }
                        },
                        options = TextFormFieldOptions(
                            label = { Text(text = stringResource(id = R.string.max_amount)) },
                            shape = RectangleShape,
                            trailingIcon = {
                                Text(text = state.value.unit.abbreviation)
                            },
                            keyboardOptions = KeyboardOptions.Default.copy(
                                keyboardType = KeyboardType.Number,
                                imeAction = ImeAction.Next
                            ),
                        ),
                        modifier = Modifier
                            .onFocusChanged { focusState ->
                                if (!focusState.isFocused) {
                                    val newValue = recalculateStateForMaxValue(
                                        maxAmountTextFieldState.value,
                                        state.value
                                    )
                                    if (newValue != state.value) {
                                        onChange(newValue)
                                    } else {
                                        maxAmountTextFieldState = maxAmountTextFieldState.copy(
                                            value = newValue.maxAmount?.let {
                                                "%.2f".format(
                                                    Locale.US,
                                                    it
                                                )
                                            } ?: ""
                                        )
                                    }
                                }
                            }
                    )
                }

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
                                columnSize.height.toDp()
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

fun recalculateStateForCurrentValue(
    currentTextValue: String,
    previousState: FormAmountWithUnit
): FormAmountWithUnit {
    val currentAmount = currentTextValue.toDoubleOrNull()

    when {
        currentAmount == null -> {
            return FormAmountWithUnit(
                currentAmount = null,
                maxAmount = previousState.maxAmount,
                unit = previousState.unit
            )
        }

        currentAmount > (previousState.maxAmount ?: Double.MAX_VALUE) -> {
            return FormAmountWithUnit(
                currentAmount = currentAmount,
                maxAmount = currentAmount,
                unit = previousState.unit
            )
        }

        else -> {
            return FormAmountWithUnit(
                currentAmount = currentAmount,
                maxAmount = previousState.maxAmount,
                unit = previousState.unit
            )
        }
    }
}

fun recalculateStateForMaxValue(
    maxTextValue: String,
    previousState: FormAmountWithUnit
): FormAmountWithUnit {
    val maxAmount = maxTextValue.toDoubleOrNull()

    when {
        maxAmount == null -> {
            return FormAmountWithUnit(
                currentAmount = previousState.currentAmount,
                maxAmount = null,
                unit = previousState.unit
            )
        }

        maxAmount < (previousState.currentAmount ?: 0.0) -> {
            return FormAmountWithUnit(
                currentAmount = maxAmount,
                maxAmount = maxAmount,
                unit = previousState.unit
            )
        }

        else -> {
            return FormAmountWithUnit(
                currentAmount = previousState.currentAmount,
                maxAmount = maxAmount,
                unit = previousState.unit
            )
        }
    }
}