package com.sobczal2.biteright.ui.components.currencies

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material3.Icon
import androidx.compose.material3.ListItem
import androidx.compose.material3.LocalContentColor
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

@Composable
fun SimplifiedCurrencyItem(
    currency: CurrencyDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    label: String? = null,
    labelColor: Color = LocalContentColor.current
) {
    Row(
        modifier = modifier,
        verticalAlignment = Alignment.CenterVertically,
    ) {
        Column {
            if (label != null) {
                Text(
                    text = label,
                    style = MaterialTheme.typography.bodySmall.copy(
                        color = labelColor
                    )
                )
            }
            Text(
                text = currency.code,
                style = MaterialTheme.typography.bodyLarge.copy(
                    color = MaterialTheme.colorScheme.onSurface
                )
            )
        }
        Box(
            contentAlignment = Alignment.Center,
            modifier = Modifier.size(MaterialTheme.dimension.xl)
        ) {
            if (selected) {
                Icon(
                    Icons.Default.Done,
                    contentDescription = stringResource(id = R.string.selected_str),
                    modifier = Modifier.size(MaterialTheme.dimension.xl),
                )
            }
        }
    }
}

@Composable
fun FullCurrencyItem(
    currency: CurrencyDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    label: String? = null,
    labelColor: Color = LocalContentColor.current
) {
    Row(
        modifier = modifier,
        verticalAlignment = Alignment.CenterVertically,
        horizontalArrangement = Arrangement.SpaceBetween,
    ) {
        Column {
            if (label != null) {
                Text(
                    text = label,
                    style = MaterialTheme.typography.bodySmall.copy(
                        color = labelColor
                    )
                )
            }
            Text(
                text = "${currency.name} - ${currency.symbol}",
                style = MaterialTheme.typography.bodyLarge.copy(
                    color = MaterialTheme.colorScheme.onSurface
                )
            )
        }
        Box(
            contentAlignment = Alignment.Center,
            modifier = Modifier.size(MaterialTheme.dimension.xl)
        ) {
            if (selected) {
                Icon(
                    Icons.Default.Done,
                    contentDescription = stringResource(id = R.string.selected_str),
                    modifier = Modifier.size(MaterialTheme.dimension.xl),
                )
            }
        }
    }
}

@Composable
fun CurrencyListItem(
    currency: CurrencyDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
) {
    ListItem(
        headlineContent = {
            Row(
                verticalAlignment = Alignment.CenterVertically,
                horizontalArrangement = Arrangement.Start,
                modifier = Modifier.fillMaxWidth()
            ) {
                Text(
                    text = currency.symbol,
                    modifier = Modifier.width(
                        MaterialTheme.dimension.xxl
                    )
                )
                Text(text = currency.name)
            }
        },
        trailingContent = {
            if (selected) {
                Icon(imageVector = Icons.Default.Done, contentDescription = "Selected")
            }
        },
        modifier = modifier
    )
}

@Composable
@BiteRightPreview
fun SimplifiedCurrencyItemPreview() {
    SimplifiedCurrencyItem(
        currency = CurrencyDto(
            id = UUID.randomUUID(),
            code = "USD",
            symbol = "$",
            name = "US Dollar",
        ),
        selected = true,
        label = "Currency",
    )
}