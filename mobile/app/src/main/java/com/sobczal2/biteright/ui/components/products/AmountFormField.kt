package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Card
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.graphics.RectangleShape
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
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
                value = ""
            )
        )
    }
    var maxAmountTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    val amountTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    LaunchedEffect(state.value) {
        if (state.value.currentAmount != null && currentAmountTextFieldState.value != "%.2f".format(
                state.value.currentAmount
            )
        )
            currentAmountTextFieldState = currentAmountTextFieldState.copy(
                value = "%.2f".format(state.value.currentAmount)
            )

        if (state.value.maxAmount != null && maxAmountTextFieldState.value != "%.2f".format(
                state.value.maxAmount
            )
        )
            maxAmountTextFieldState = maxAmountTextFieldState.copy(
                value = "%.2f".format(state.value.maxAmount)
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
                Column(
                    modifier = Modifier
                        .weight(0.6f)
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
                                keyboardType = KeyboardType.Number
                            ),
                        ),
                        modifier = Modifier
                            .onFocusChanged {
                                if (!it.isFocused) {
                                    currentAmountTextFieldState =
                                        when (val price =
                                            currentAmountTextFieldState.value.toDoubleOrNull()) {
                                            null -> {
                                                currentAmountTextFieldState.copy(
                                                    value = "",
                                                )
                                            }

                                            else -> {
                                                currentAmountTextFieldState.copy(
                                                    value = "%.2f".format(price)
                                                )
                                            }
                                        }
                                    when (val newAmount =
                                        currentAmountTextFieldState.value.toDoubleOrNull()) {
                                        null -> {
                                            onChange(
                                                state.value.copy(
                                                    currentAmount = null
                                                )
                                            )
                                        }

                                        else -> {
                                            if (state.value.maxAmount == null || newAmount > state.value.maxAmount!!) {
                                                onChange(
                                                    state.value.copy(
                                                        currentAmount = newAmount,
                                                        maxAmount = newAmount
                                                    )
                                                )
                                            } else {
                                                onChange(
                                                    state.value.copy(
                                                        currentAmount = newAmount
                                                    )
                                                )
                                            }
                                        }
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
                                keyboardType = KeyboardType.Number
                            ),
                        ),
                        modifier = Modifier
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
                                                    value = "%.2f".format(price)
                                                )
                                            }
                                        }
                                    when (val newAmount =
                                        maxAmountTextFieldState.value.toDoubleOrNull()) {
                                        null -> {
                                            onChange(
                                                state.value.copy(
                                                    maxAmount = null
                                                )
                                            )
                                        }

                                        else -> {
                                            if (state.value.currentAmount == null || newAmount < state.value.currentAmount!!) {
                                                onChange(
                                                    state.value.copy(
                                                        maxAmount = newAmount,
                                                        currentAmount = newAmount
                                                    )
                                                )
                                            } else {
                                                onChange(
                                                    state.value.copy(
                                                        maxAmount = newAmount
                                                    )
                                                )
                                            }
                                        }
                                    }
                                }
                            }
                    )
                }
                SimplifiedUnitItem(
                    unit = state.value.unit,
                    selected = false,
                    onClick = {
                        dialogOpen = true
                    },
                    label = stringResource(id = R.string.unit),
                    modifier = Modifier
                        .weight(0.4f)
                        .padding(start = MaterialTheme.dimension.sm)
                )
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