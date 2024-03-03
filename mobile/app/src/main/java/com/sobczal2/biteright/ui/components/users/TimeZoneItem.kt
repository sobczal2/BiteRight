package com.sobczal2.biteright.ui.components.users

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.size
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
import com.sobczal2.biteright.ui.theme.dimension
import java.util.TimeZone

@Composable
fun TimeZoneListItem(
    timeZone: TimeZone,
    selected: Boolean,
    modifier: Modifier = Modifier,
) {
    ListItem(
        headlineContent = {
            Text(text = timeZone.id)
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
fun TimeZoneItem(
    timeZone: TimeZone,
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
                text = timeZone.id,
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
                    contentDescription = stringResource(id = R.string.selected),
                    modifier = Modifier.size(MaterialTheme.dimension.xl),
                )
            }
        }
    }
}