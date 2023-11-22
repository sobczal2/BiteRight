package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.width
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp

@Composable
fun VSpacer(
    type: SpacerType,
    multiplier: Int = 1
) {
    Spacer(
        modifier = Modifier
            .height(type.value * multiplier)
    )
}

@Composable
fun HSpacer(
    type: SpacerType,
    multiplier: Int = 1
) {
    Spacer(
        modifier = Modifier
            .width(type.value * multiplier)
    )
}

enum class SpacerType {
    Small,
    Medium,
    Large,
    ExtraLarge,
}

private val SpacerType.value: Dp
    get() {
        return when (this) {
            SpacerType.Small -> 8.dp
            SpacerType.Medium -> 16.dp
            SpacerType.Large -> 24.dp
            SpacerType.ExtraLarge -> 32.dp
        }
    }