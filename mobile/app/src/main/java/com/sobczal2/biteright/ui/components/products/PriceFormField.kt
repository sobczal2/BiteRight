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
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.currencies.CurrencyListItem
import com.sobczal2.biteright.ui.components.currencies.SimplifiedCurrencyItem
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.extraSmallTop
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.Locale
import java.util.UUID

data class FormPriceWithCurrency(
    val price: Double?, val currency: CurrencyDto
) {
    companion object {
        val Empty = FormPriceWithCurrency(
            price = null, currency = CurrencyDto.Empty
        )
    }
}

data class PriceFormFieldState(
    var value: FormPriceWithCurrency,
    val error: String? = null,
)

@Composable
fun PriceFormField(
    state: PriceFormFieldState,
    onChange: (FormPriceWithCurrency) -> Unit,
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto>,
    modifier: Modifier = Modifier,
) {
    var dialogOpen by remember { mutableStateOf(false) }
    var priceTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = state.value.price?.let { "%.2f".format(Locale.US, it) } ?: "",
            )
        )
    }

    val moneyTypingRegex = Regex("""^\d+(\.\d{0,2})?$""")

    LaunchedEffect(state.value) {
        if (state.value.price != null && priceTextFieldState.value != "%.2f".format(Locale.US, state.value.price))
            priceTextFieldState = priceTextFieldState.copy(
                value = "%.2f".format(Locale.US, state.value.price)
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
                TextFormField(
                    state = priceTextFieldState,
                    onChange = {
                        if (it.isEmpty() || moneyTypingRegex.matches(it)) {
                            priceTextFieldState = priceTextFieldState.copy(
                                value = it
                            )
                        }
                    },
                    options = TextFormFieldOptions(
                        label = { Text(text = stringResource(id = R.string.price)) },
                        shape = MaterialTheme.shapes.extraSmallTop.copy(
                            topEnd = CornerSize(0.dp)
                        ),
                        trailingIcon = {
                            Text(text = state.value.currency.symbol)
                        },
                        keyboardOptions = KeyboardOptions.Default.copy(
                            keyboardType = KeyboardType.Number,

                            ),
                    ),
                    modifier = Modifier
                        .weight(0.6f)
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
                                                value = "%.2f".format(Locale.US, price)
                                            )
                                        }
                                    }

                                onChange(
                                    state.value.copy(
                                        price = priceTextFieldState.value.toDoubleOrNull()
                                    )
                                )
                            }
                        }
                )
                SimplifiedCurrencyItem(
                    currency = state.value.currency,
                    selected = false,
                    onClick = {
                        dialogOpen = true
                    },
                    label = stringResource(id = R.string.currency),
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
            search = searchCurrencies,
            keySelector = { it.id },
            onDismissRequest = { dialogOpen = false },
            selectedItem = state.value.currency,
        ) { currency, selected ->
            CurrencyListItem(
                currency = currency,
                selected = selected,
                modifier = Modifier
                    .clickable {
                        onChange(state.value.copy(currency = currency))
                        dialogOpen = false
                    }
            )
        }
    }
}

@Composable
@BiteRightPreview
fun PriceFormFieldPreview() {
    val currencies = listOf(
        CurrencyDto(
            id = UUID.randomUUID(), code = "USD", name = "US Dollar", symbol = "$"
        ),
        CurrencyDto(
            id = UUID.randomUUID(), code = "EUR", name = "Euro", symbol = "€"
        ),
        CurrencyDto(
            id = UUID.randomUUID(), code = "PLN", name = "Polish Zloty", symbol = "zł"
        ),
    )

    BiteRightTheme {
        PriceFormField(state = PriceFormFieldState(
            value = FormPriceWithCurrency(
                price = 20.0, currency = currencies.first(),
            ),
            error = "Error",
        ), onChange = {}, searchCurrencies = { _, _ ->
            PaginatedList(
                items = currencies,
                pageNumber = 0,
                pageSize = 3,
                totalPages = 1,
                totalCount = 3,
            )
        })
    }
}