package com.sobczal2.biteright.ui.components.currencies

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Card
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.theme.dimension

data class CurrencyFormFieldState(
    val value: CurrencyDto,
    val error: String? = null,
)

@Composable
fun CurrencyFormField(
    state: CurrencyFormFieldState,
    onChange: (CurrencyDto) -> Unit,
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto>,
    modifier: Modifier = Modifier,
) {
    var dialogOpen by remember { mutableStateOf(false) }

    Column(
        modifier = modifier,
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmall
        ) {
            FullCurrencyItem(
                currency = state.value,
                selected = false,
                onClick = {
                    dialogOpen = true
                },
                label = stringResource(id = R.string.preferred_currency),
                modifier = Modifier
                    .padding(start = MaterialTheme.dimension.sm)
            )
        }

        if (state.error != null) {
            Text(
                text = state.error,
                style = MaterialTheme.typography.bodySmall,
                color = MaterialTheme.colorScheme.error,
                modifier = Modifier
                    .padding(start = MaterialTheme.dimension.sm)
            )
        }
    }

    if (dialogOpen) {
        SearchDialog(
            search = searchCurrencies,
            keySelector = { it.id },
            onDismissRequest = { dialogOpen = false },
            selectedItem = state.value,
        ) { currency, selected ->
            CurrencyListItem(
                currency = currency,
                selected = selected,
                modifier = Modifier
                    .clickable {
                        onChange(currency)
                        dialogOpen = false
                    }
            )
        }
    }
}