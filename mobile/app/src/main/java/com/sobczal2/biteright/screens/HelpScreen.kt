package com.sobczal2.biteright.screens

import androidx.compose.runtime.Composable
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.events.HelpScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.HelpScreenState
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.viewmodels.HelpViewModel

@Composable
fun HelpScreen(
    viewModel: HelpViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    SurfaceLoader(
        loading = state.value.isLoading()
    ) {
        HelpScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            topLevelNavigate = topLevelNavigate,
        )
    }
}

@Composable
fun HelpScreenContent(
    state: HelpScreenState = HelpScreenState(),
    sendEvent: (HelpScreenEvent) -> Unit = { },
    topLevelNavigate: (Routes) -> Unit = { },
) {

}