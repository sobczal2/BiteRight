package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.Row
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.VerticalDivider
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.sobczal2.biteright.util.BiteRightPreview

@Composable
fun DisplayPair(
    label: String,
    value: String,
    modifier: Modifier = Modifier
) {
    Row(
        modifier = modifier,
    ) {
        Text(
            text = label,
            modifier = Modifier.weight(0.5f),
            style = MaterialTheme.typography.titleSmall
        )
        Text(
            text = value,
            modifier = Modifier.weight(0.5f),
            style = MaterialTheme.typography.bodyLarge
        )
    }
}

@Composable
@BiteRightPreview
fun DisplayPairPreview() {
    DisplayPair(
        label = "Label",
        value = "Value"
    )
}