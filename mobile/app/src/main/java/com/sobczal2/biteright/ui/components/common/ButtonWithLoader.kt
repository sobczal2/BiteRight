package com.sobczal2.biteright.ui.components.common

import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview

@Composable
fun ButtonWithLoader(
    onClick: () -> Unit,
    loading: Boolean,
    modifier: Modifier = Modifier,
    content: @Composable () -> Unit
) {
    Button(
        onClick = onClick,
        modifier = modifier,
        enabled = !loading,
    ) {
        if (loading) {
            CircularProgressIndicator(
                color = MaterialTheme.colorScheme.onPrimary,
            )
        } else {
            content()
        }
    }
}

@Composable
@Preview(apiLevel = 33)
fun ButtonWithLoaderPreview() {
    ButtonWithLoader(
        onClick = {},
        loading = false,
    ) {
        Text("Button")
    }
}

@Composable
@Preview(apiLevel = 33)
fun ButtonWithLoaderLoadingPreview() {
    ButtonWithLoader(
        onClick = {},
        loading = true,
    ) {
        Text("Button")
    }
}