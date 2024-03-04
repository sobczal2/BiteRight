package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.SnackbarHostState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.HelpScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.HelpScreenState
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.components.common.ErrorSnackbarHost
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.help.HelpCarousel
import com.sobczal2.biteright.ui.components.help.HelpCarouselEntry
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.HelpViewModel


@Composable
fun HelpScreen(
    viewModel: HelpViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()
    val snackbarHostState = remember { SnackbarHostState() }

    SurfaceLoader(
        loading = state.value.isLoading()
    ) {
        HelpScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            topLevelNavigate = topLevelNavigate,
            snackbarHostState = snackbarHostState,
        )
    }
}

@Composable
fun HelpScreenContent(
    state: HelpScreenState = HelpScreenState(),
    sendEvent: (HelpScreenEvent) -> Unit = { },
    topLevelNavigate: (Routes) -> Unit = { },
    snackbarHostState: SnackbarHostState,
) {
    Scaffold(
        snackbarHost = {
            ErrorSnackbarHost(
                snackbarHostState = snackbarHostState,
            )
        }
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
            HelpCarousel(
                entries = state.entries,
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues),
                onContinue = {
                    topLevelNavigate(Routes.HomeGraph())
                }
            )
        }
    }
}