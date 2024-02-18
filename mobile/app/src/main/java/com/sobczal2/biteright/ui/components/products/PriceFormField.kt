package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
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
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.constraintlayout.compose.ConstraintLayout
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import java.util.UUID

data class PriceWithCurrency(
    val price: Double?, val currency: CurrencyDto?
)

data class PriceFormFieldState(
    var value: PriceWithCurrency = PriceWithCurrency(
        price = null, currency = null
    ),
    val priceError: String? = null,
    val currencyError: String? = null,
    val availableCurrencies: List<CurrencyDto>,
)

@Composable
fun PriceFormField(
    state: PriceFormFieldState,
    onChange: (PriceWithCurrency) -> Unit,
    modifier: Modifier = Modifier,
) {
    var expanded by remember { mutableStateOf(false) }
    var priceTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    val currencyTextFieldState by remember {
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

    LaunchedEffect(keys = arrayOf(priceTextFieldState, selectedCurrency)) {
        onChange(
            PriceWithCurrency(
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
                shape = MaterialTheme.shapes.extraSmall.copy(
                    topEnd = CornerSize(0.dp),
                    bottomEnd = CornerSize(0.dp),
                    bottomStart = CornerSize(0.dp)
                ),
                trailingIcon = {
                    Text(text = selectedCurrency?.symbol ?: "")
                },
                keyboardOptions = KeyboardOptions.Default.copy(
                    keyboardType = KeyboardType.Number
                ),
            ),
            modifier = Modifier
                .weight(0.5f)
                .onFocusChanged {
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
                .weight(0.5f)
        ) {
            DropdownMenu(
                expanded = expanded,
                onDismissRequest = {
                    expanded = false
                },
            ) {
                DropdownMenuItem(
                    leadingIcon = if (selectedCurrency == null) {
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
                        selectedCurrency = null
                        expanded = false
                    },
                )
                state.availableCurrencies.forEach { currency ->
                    DropdownMenuItem(
                        leadingIcon = if (currency == selectedCurrency) {
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
                            expanded = false
                            selectedCurrency = currency
                        },
                    )
                }
            }
            val interactionSource = remember { MutableInteractionSource() }
            val isPressed: Boolean by interactionSource.collectIsPressedAsState()

            LaunchedEffect(isPressed) {
                if (isPressed) {
                    expanded = true
                }
            }

            TextFormField(
                state = currencyTextFieldState.copy(
                    value = selectedCurrency?.code ?: stringResource(id = R.string.none),
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
                            imageVector = if (expanded) {
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
            )
        }
    }
}

@Composable
@Preview(apiLevel = 33, showBackground = true)
@Preview(apiLevel = 33, showBackground = true, uiMode = Configuration.UI_MODE_NIGHT_YES)
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
                value = PriceWithCurrency(
                    price = 0.0, currency = currencies.first()
                ),
                availableCurrencies = currencies
            ),
            onChange = {},
        )
    }
}