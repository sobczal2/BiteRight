package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material.icons.filled.KeyboardArrowUp
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.common.forms.FormFieldEvents
import com.sobczal2.biteright.ui.components.common.forms.FormFieldState
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import java.util.UUID

data class PriceWithCurrency(
    val price: Double, val currency: CurrencyDto
)

data class PriceFormFieldState(
    override val value: PriceWithCurrency? = null,
    override val error: String? = null,
    val availableCurrencies: List<CurrencyDto>,
) : FormFieldState<PriceWithCurrency?>

typealias PriceFormFieldEvents = FormFieldEvents<PriceWithCurrency>

data class PriceFormFieldOptions(
    val label: @Composable (() -> Unit)? = null,
)

@Composable
fun PriceFormField(
    state: PriceFormFieldState,
    onEvent: (PriceFormFieldEvents) -> Unit,
    options: PriceFormFieldOptions,
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

    Row(
        modifier = modifier
    ) {
        TextFormField(
            state = priceTextFieldState, onEvent = { event ->
                when (event) {
                    is FormFieldEvents.OnValueChange -> {
                        priceTextFieldState = priceTextFieldState.copy(
                            value = event.value
                        )
                    }
                }
            },
            options = TextFormFieldOptions(
                label = options.label, shape = MaterialTheme.shapes.extraSmall.copy(
                    topEnd = CornerSize(0.dp),
                    bottomEnd = CornerSize(0.dp),
                    bottomStart = CornerSize(0.dp)
                ),
                trailingIcon = {
                    Text(text = state.value?.currency?.symbol ?: "")
                }
            ),
            modifier = Modifier.weight(0.6f)
        )
        Column(
            modifier = Modifier
                .weight(0.4f)
        ) {
            DropdownMenu(
                expanded = true,
                onDismissRequest = {
                    expanded = false
                },
            ) {
                state.availableCurrencies.forEach { currency ->
                    DropdownMenuItem(
                        leadingIcon = if (currency == state.value?.currency) {
                            {
                                Icon(Icons.Default.Done, contentDescription = null)
                            }
                        } else {
                            null
                        },
                        text = {
                            Text(
                                text = "Test",
                                color = Color.Red,
                            )
                        },
                        onClick = {},
                    )
                }
            }
            TextField(
                value = state.value?.currency?.code ?: stringResource(id = R.string.currency),
                onValueChange = {},
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
                enabled = false,
                modifier = Modifier
                    .clickable {
                        expanded = true
                    },
                colors = TextFieldDefaults.colors()
                    .copy(
                        disabledTextColor = MaterialTheme.colorScheme.onSurface,
                        disabledTrailingIconColor = MaterialTheme.colorScheme.onSurface,
                        disabledIndicatorColor = MaterialTheme.colorScheme.onSurface
                    )
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
                error = null,
                availableCurrencies = currencies
            ),
            onEvent = {},
            options = PriceFormFieldOptions(label = { Text(text = "Price") })
        )
    }
}