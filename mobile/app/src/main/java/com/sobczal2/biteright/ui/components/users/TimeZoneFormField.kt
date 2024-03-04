package com.sobczal2.biteright.ui.components.users

import androidx.compose.foundation.clickable
import androidx.compose.foundation.focusable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Card
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusDirection
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.extraSmallTop
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
    val focusManager = LocalFocusManager.current

    Column(
        modifier = modifier,
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmallTop
        ) {
            // Todo: Wrap these into single component
            val focusRequester = remember {
                FocusRequester()
            }
            var focused by remember { mutableStateOf(false) }
            val color =
                if (focused) TextFieldDefaults.colors().focusedLabelColor else TextFieldDefaults.colors().unfocusedLabelColor
            Box(
                modifier = Modifier
                    .clickable {
                        dialogOpen = true
                        focusRequester.requestFocus()
                    }
                    .focusRequester(focusRequester)
                    .onFocusChanged {
                        focused = it.isFocused
                    }
                    .focusable(),
                contentAlignment = Alignment.BottomStart,
            ) {
                TimeZoneItem(
                    timeZone = state.value,
                    selected = false,
                    label = stringResource(id = R.string.time_zone),
                    labelColor = color,
                    modifier = Modifier
                        .padding(start = MaterialTheme.dimension.sm)
                )

                HorizontalDivider(
                    thickness = if (focused) 2.dp else 1.dp,
                    color = color
                )
            }
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
                    focusManager.moveFocus(FocusDirection.Next)
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