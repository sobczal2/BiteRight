package com.sobczal2.biteright.ui.components.units

import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material3.Icon
import androidx.compose.material3.ListItem
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.ui.components.common.LabeledItem

@Composable
fun SimplifiedUnitItem(
    unit: UnitDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    onClick: () -> Unit,
    label: String? = null,
) {
    LabeledItem(
        text = unit.name,
        selected = selected,
        modifier = modifier,
        onClick = onClick,
        label = label
    )
}

@Composable
fun FullUnitItem(
    unit: UnitDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    onClick: () -> Unit,
    label: String? = null,
) {
    LabeledItem(
        text = "${unit.name} - ${unit.abbreviation}",
        selected = selected,
        modifier = modifier,
        onClick = onClick,
        label = label
    )
}

@Composable
fun UnitListItem(
    unit: UnitDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
) {
    ListItem(
        headlineContent = {
            Text(text = "${unit.name} - ${unit.abbreviation}")
        },
        trailingContent = {
            if (selected) {
                Icon(imageVector = Icons.Default.Done, contentDescription = "Selected")
            }
        },
        modifier = modifier
    )
}