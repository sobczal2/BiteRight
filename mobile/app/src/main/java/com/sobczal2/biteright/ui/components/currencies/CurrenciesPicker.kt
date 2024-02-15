package com.sobczal2.biteright.ui.components.currencies

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.AttachMoney
import androidx.compose.material.icons.filled.Done
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.window.Dialog
import androidx.compose.ui.window.DialogProperties
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import java.util.UUID

@Composable
fun CurrenciesDialog(
    availableCurrencies: List<CurrencyDto>,
    selectedCurrency: CurrencyDto?,
    onSelectionChange: (CurrencyDto?) -> Unit,
    onDismissRequest: () -> Unit
) {
    Dialog(
        onDismissRequest = onDismissRequest,
        properties = DialogProperties(
            dismissOnBackPress = true,
            dismissOnClickOutside = true,
        ),
    ) {
        Surface(
            shape = MaterialTheme.shapes.medium,
            modifier = Modifier
                .padding(MaterialTheme.dimension.lg)
        ) {
            Column(
                modifier = Modifier
                    .padding(MaterialTheme.dimension.sm)
            ) {
                Row {
                    Icon(Icons.Default.AttachMoney, contentDescription = "Currency")
                    Text(
                        text = stringResource(id = R.string.select_currency),
                        modifier = Modifier
                    )
                }
                LazyColumn(
                    content = {
                        items(
                            items = availableCurrencies,
                            key = { currency -> currency.id }
                        ) {
                            CurrencyItem(
                                currency = it,
                                selected = it.id == selectedCurrency?.id,
                                onClick = { onSelectionChange(it) }
                            )
                            if (it != availableCurrencies.last()) {
                                HorizontalDivider()
                            }
                        }
                    })
            }
        }
    }
}

@Composable
fun CurrencyItem(
    currency: CurrencyDto,
    onClick: () -> Unit,
    selected: Boolean,
) {
    Surface(
        modifier = Modifier
            .fillMaxSize(),
        onClick = onClick
    ) {
        Box(modifier = Modifier.padding(start = MaterialTheme.dimension.md)) {
            Row {
                if (selected) {
                    Icon(
                        Icons.Default.Done, contentDescription = "Selected",
                        modifier = Modifier.padding(end = MaterialTheme.dimension.sm)
                    )
                }
                Text(
                    text = "${currency.name} (${currency.symbol})",
                )
            }
        }
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun CurrencyItemPreview() {
    BiteRightTheme {
        CurrencyItem(
            currency = CurrencyDto(
                UUID.fromString("e3f3e3e3-3e3e-3e3e-3e3e-3e3e3e3e3e3e"),
                "Pound sterling",
                "£",
                "GBP"
            ),
            onClick = {},
            selected = false
        )
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun CurrenciesDialogPreview() {
    val selectedCurrency = CurrencyDto(
        UUID.fromString("e3f3e3e3-3e3e-3e3e-3e3e-3e3e3e3e3e3e"),
        "Pound sterling",
        "£",
        "GBP"
    )
    BiteRightTheme {
        CurrenciesDialog(
            availableCurrencies = listOf(
                selectedCurrency,
                CurrencyDto(UUID.randomUUID(), "Euro", "€", "EUR"),
                CurrencyDto(UUID.randomUUID(), "United States dollar", "$", "USD")
            ),
            selectedCurrency = selectedCurrency,
            onSelectionChange = {},
            onDismissRequest = {}
        )
    }
}