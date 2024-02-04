package com.sobczal2.biteright

import androidx.compose.runtime.Composable
import com.sobczal2.biteright.ui.theme.BiteRightTheme

@Composable
fun App(content: @Composable () -> Unit) {
    BiteRightTheme {
        content()
    }
}