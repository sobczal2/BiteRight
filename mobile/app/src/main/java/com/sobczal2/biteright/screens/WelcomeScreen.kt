package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.asString
import com.sobczal2.biteright.viewmodels.WelcomeViewModel
import kotlinx.coroutines.CompletableDeferred
import kotlinx.coroutines.launch

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(),
    authManager: AuthManager,
    navigateToStart: () -> Unit = {},
) {
    val state = viewModel.state.collectAsState()
    val coroutineScope = rememberCoroutineScope()

    WelcomeScreenContent(
        onGetStartedClick = {
            coroutineScope.launch {
                val loginResult = CompletableDeferred<Either<Unit, Int>>()

                authManager.login(
                    onSuccess = {
                        loginResult.complete(Unit.left())
                    },
                    onFailure = { errorStringId ->
                        loginResult.complete(errorStringId.right())
                    }
                )

                val result = loginResult.await()
                viewModel.onGetStartedClick { result }
                if (result.isLeft()) {
                    navigateToStart()
                }
            }
        },
        state = state.value
    )
}


@Composable
fun WelcomeScreenContent(
    onGetStartedClick: () -> Unit = {},
    state: WelcomeScreenState = WelcomeScreenState()
) {
    Surface(
        modifier = Modifier
            .fillMaxSize()
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            Text(
                text = stringResource(id = R.string.app_name),
                style = MaterialTheme.typography.displayLarge
            )
            Button(onClick = onGetStartedClick) {
                Text(
                    text = stringResource(id = R.string.get_started),
                    style = MaterialTheme.typography.displayMedium
                )
            }
            state.error?.let {
                Text(
                    text = it.asString(),
                    style = MaterialTheme.typography.bodySmall
                )
            }
        }
    }
}

@Preview
@Composable
fun WelcomeScreenPreview() {
    BiteRightTheme {
        WelcomeScreenContent()
    }
}