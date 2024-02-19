package com.sobczal2.biteright.ui.components.products

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
import androidx.compose.material3.LocalTextStyle
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
import androidx.compose.ui.text.input.PlatformImeOptions
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

data class FormPriceWithCurrency(
    val price: Double?,
    val currency: CurrencyDto?
)

data class PriceFormFieldState(
    var value: FormPriceWithCurrency = FormPriceWithCurrency(
        price = null,
        currency = null
    ),
    val priceError: String? = null,
    val currencyError: String? = null,
    val availableCurrencies: List<CurrencyDto> = emptyList()
)

@Composable
fun PriceFormField(
    state: PriceFormFieldState,
    onChange: (FormPriceWithCurrency) -> Unit,
    modifier: Modifier = Modifier,
) {
    var dropDownExpanded by remember { mutableStateOf(false) }
    var priceTextFieldState by remember {
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

    var selectedCurrency by remember {
        mutableStateOf<CurrencyDto?>(null)
    }

    val moneyTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    var priceFieldFocused by remember { mutableStateOf(false) }

    LaunchedEffect(state.value) {
        selectedCurrency = state.value.currency
        if (!priceFieldFocused) {
            priceTextFieldState = priceTextFieldState.copy(
                value = if (state.value.price != null) {
                    String.format("%.2f", state.value.price)
                } else {
                    ""
                }
            )
        }
    }

    LaunchedEffect(keys = arrayOf(priceTextFieldState, selectedCurrency)) {
        onChange(
            FormPriceWithCurrency(
                price = priceTextFieldState.value.toDoubleOrNull(),
                currency = selectedCurrency
            )
        )
    }

    Row(
        modifier = modifier
    ) {
        TextFormField(
            state = priceTextFieldState.copy(
                error = state.priceError
            ),
            onChange = {
                if (it.isEmpty() || moneyTypingRegex.matches(it)) {
                    priceTextFieldState = priceTextFieldState.copy(
                        value = it
                    )
                }
            },
            options = TextFormFieldOptions(
                label = { Text(text = stringResource(id = R.string.price)) },
                shape = MaterialTheme.shapes.small.copy(
                    topEnd = CornerSize(0.dp),
                    bottomEnd = CornerSize(0.dp),
                    bottomStart = CornerSize(0.dp)
                ),
                trailingIcon = {
                    Text(text = state.value.currency?.symbol ?: "")
                },
                keyboardOptions = KeyboardOptions.Default.copy(
                    keyboardType = KeyboardType.Number,

                ),
            ),
            modifier = Modifier
                .weight(0.6f)
                .onFocusChanged {
                    priceFieldFocused = it.isFocused
                    if (!it.isFocused) {
                        priceTextFieldState =
                            when (val price = priceTextFieldState.value.toDoubleOrNull()) {
                                null -> {
                                    priceTextFieldState.copy(
                                        value = "",
                                    )
                                }

                                else -> {
                                    priceTextFieldState.copy(
                                        value = String.format("%.2f", price)
                                    )
                                }

                            }
                    }
                }
        )
        Column(
            modifier = Modifier
                .weight(0.4f)
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
                    value = state.value.currency?.code ?: stringResource(id = R.string.none),
                    error = state.currencyError
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
                    leadingIcon = if (state.value.currency == null) {
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
                                currency = null
                            )
                        )
                        dropDownExpanded = false
                    },
                )
                state.availableCurrencies.forEach { currency ->
                    DropdownMenuItem(
                        leadingIcon = if (currency == state.value.currency) {
                            {
                                Icon(Icons.Default.Done, contentDescription = null)
                            }
                        } else {
                            null
                        },
                        text = {
                            Text(text = currency.code)
                        },
                        onClick = {
                            dropDownExpanded = false
                            onChange(
                                state.value.copy(
                                    currency = currency
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
fun PriceFormFieldPreview() {
    val currencies = listOf(
        CurrencyDto(
            id = UUID.randomUUID(),
            code = "USD",
            name = "US Dollar",
            symbol = "$"
        ),
        CurrencyDto(
            id = UUID.randomUUID(),
            code = "EUR",
            name = "Euro",
            symbol = "€"
        ),
        CurrencyDto(
            id = UUID.randomUUID(),
            code = "PLN",
            name = "Polish Zloty",
            symbol = "zł"
        ),
    )

    BiteRightTheme {
        PriceFormField(
            state = PriceFormFieldState(
                value = FormPriceWithCurrency(
                    price = 20.0,
                    currency = currencies.first()
                ),
                availableCurrencies = currencies
            ),
            onChange = {},
        )
    }
}