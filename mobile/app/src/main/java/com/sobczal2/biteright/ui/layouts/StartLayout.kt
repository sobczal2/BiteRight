package com.sobczal2.biteright.ui.layouts

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.SnackbarHostState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.components.common.ErrorSnackbarHost
import com.sobczal2.biteright.ui.theme.dimension

@Composable
fun StartLayout(
    content: @Composable (SnackbarHostState) -> Unit
) {
    val snackbarHostState = remember { SnackbarHostState() }
    Scaffold(
        snackbarHost = {
            ErrorSnackbarHost(snackbarHostState = snackbarHostState)
        },
    ) { paddingValues ->
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            modifier = Modifier
                .padding(paddingValues)
                .padding(top = MaterialTheme.dimension.xxl),
        ) {
            BiteRightLogo(
                modifier = Modifier.size(250.dp)
            )
            content(snackbarHostState)
        }
    }
}