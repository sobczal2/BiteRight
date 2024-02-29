package com.sobczal2.biteright.ui.components.users

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
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.components.currencies.SimplifiedCurrencyItem
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.TimeZone

data class TimeZoneFormFieldState(
    val value: TimeZone,
    val error: String? = null,
)

@Composable
fun TimeZoneFormField(
    state: TimeZoneFormFieldState,
    onChange: (TimeZone) -> Unit,
    searchTimeZones: suspend (String, PaginationParams) -> PaginatedList<TimeZone>,
    modifier: Modifier = Modifier,
) {
    var dialogOpen by remember { mutableStateOf(false) }

    Column(
        modifier = modifier,
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmall
        ) {
            TimeZoneItem(
                timeZone = state.value,
                selected = false,
                onClick = {
                    dialogOpen = true
                },
                label = stringResource(id = R.string.time_zone),
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
            )
        }
    }

    if (dialogOpen) {
        SearchDialog(
            search = searchTimeZones,
            keySelector = { it },
            onDismissRequest = { dialogOpen = false },
            selectedItem = state.value,
        ) { timeZone, selected ->
            TimeZoneListItem(
                timeZone = timeZone,
                selected = selected,
                modifier = Modifier.clickable {
                    onChange(timeZone)
                    dialogOpen = false
                }
            )
        }
    }
}

@Composable
@BiteRightPreview
fun TimeZoneFormFieldPreview() {
    TimeZoneFormField(
        state = TimeZoneFormFieldState(TimeZone.getTimeZone("UTC")),
        onChange = {},
        searchTimeZones = { _, _ -> emptyPaginatedList() },
    )
}