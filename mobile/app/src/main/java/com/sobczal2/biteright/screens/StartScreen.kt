package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.StartScreenEvent
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.StartViewModel


@Composable
fun StartScreen(
    viewModel: StartViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    LaunchedEffect(Unit) {
        if (viewModel.isOnboarded()) {
            handleNavigationEvent(NavigationEvent.NavigateToCurrentProducts)
        }
    }

    ScaffoldLoader(
        loading = state.value.globalLoading
    ) {
        StartScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun StartScreenContent(
    state: StartScreenState = StartScreenState(),
    sendEvent: (StartScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
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
            TextFormField(
                state = state.usernameFieldState,
                onChange = {
                    sendEvent(StartScreenEvent.OnUsernameChange(it))
                },
                options = TextFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.username)) },
                ),
                modifier = Modifier.fillMaxWidth()
            )
            ButtonWithLoader(
                onClick = {
                    sendEvent(StartScreenEvent.OnNextClick {
                        handleNavigationEvent(NavigationEvent.NavigateToCurrentProducts)
                    })
                },
                loading = state.formSubmitting,
                content = {
                    Text(
                        text = stringResource(id = R.string.next),
                        style = MaterialTheme.typography.displaySmall
                    )
                }
            )
            ErrorBox(error = state.globalError)
        }
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
fun StartScreenPreview() {
    BiteRightTheme {
        StartScreenContent()
    }
}