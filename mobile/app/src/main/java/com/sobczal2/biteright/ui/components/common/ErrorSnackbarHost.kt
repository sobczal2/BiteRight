package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Snackbar
import androidx.compose.material3.SnackbarHost
import androidx.compose.material3.SnackbarHostState
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.sobczal2.biteright.ui.theme.dimension

@Composable
fun ErrorSnackbarHost(
    snackbarHostState: SnackbarHostState,
) {
    SnackbarHost(
        hostState = snackbarHostState,
        modifier = Modifier
            .padding(MaterialTheme.dimension.sm),
        snackbar = {snackbarData ->
            Snackbar(
                snackbarData = snackbarData,
                containerColor = MaterialTheme.colorScheme.error,
                contentColor = MaterialTheme.colorScheme.onError,
            )
        }
    )
}