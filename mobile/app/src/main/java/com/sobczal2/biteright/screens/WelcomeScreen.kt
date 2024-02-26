package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Devices
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.WelcomeScreenEvent
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.WelcomeViewModel

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    WelcomeScreenContent(
        state = state.value,
        sendEvent = viewModel::sendEvent,
        handleNavigationEvent = handleNavigationEvent,
    )
}


@Composable
fun WelcomeScreenContent(
    state: WelcomeScreenState = WelcomeScreenState(),
    sendEvent: (WelcomeScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val context = LocalContext.current
    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            BiteRightLogo(
                modifier = Modifier
                    .size(300.dp)
            )
            Button(
                onClick = {
                    sendEvent(WelcomeScreenEvent.OnGetStartedClick(context = context) {
                        handleNavigationEvent(NavigationEvent.NavigateToStart)
                    })
                }
            ) {
                Text(
                    text = stringResource(id = R.string.get_started),
                    style = MaterialTheme.typography.displaySmall
                )
            }
            ErrorBox(error = state.globalError)
        }
    }
}

@Composable
@BiteRightPreview
fun WelcomeScreenPreview() {
    BiteRightTheme {
        WelcomeScreenContent()
    }
}