package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.WelcomeScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.WelcomeViewModel

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(),
    startNavigate: (Routes.StartGraph) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    WelcomeScreenContent(
        state = state.value,
        sendEvent = viewModel::sendEvent,
        startNavigate = startNavigate
    )
}


@Composable
fun WelcomeScreenContent(
    state: WelcomeScreenState = WelcomeScreenState(),
    sendEvent: (WelcomeScreenEvent) -> Unit = {},
    startNavigate: (Routes.StartGraph) -> Unit = {},
) {
    val context = LocalContext.current
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(MaterialTheme.dimension.md),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.SpaceAround
    ) {
        Button(
            onClick = {
                sendEvent(
                    WelcomeScreenEvent.OnGetStartedClick(context = context) {
                        startNavigate(
                            Routes.StartGraph.Onboard
                        )
                    }
                )
            },
            modifier = Modifier
                .fillMaxWidth(),
            shape = MaterialTheme.shapes.extraSmall
        ) {
            Text(
                text = stringResource(id = R.string.get_started),
                style = MaterialTheme.typography.displaySmall
            )
        }
        ErrorBox(error = state.globalError)
    }
}

@Composable
@BiteRightPreview
fun WelcomeScreenPreview() {
    BiteRightTheme {
        WelcomeScreenContent()
    }
}