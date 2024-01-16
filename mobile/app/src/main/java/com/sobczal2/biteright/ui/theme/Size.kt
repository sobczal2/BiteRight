package com.sobczal2.biteright.ui.theme

import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.runtime.compositionLocalOf
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp

data class Size(
    val small: Dp = 32.dp,
    val medium: Dp = 48.dp,
    val large: Dp = 64.dp,
)

val LocalSize = compositionLocalOf { Size() }

val MaterialTheme.size: Size
    @Composable
    get() = LocalSize.current