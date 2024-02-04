package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.ui.components.FullScreenLoader
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.asString
import com.sobczal2.biteright.viewmodels.StartViewModel


@Composable
fun StartScreen(
    viewModel: StartViewModel = hiltViewModel(),
    navigateToHome: () -> Unit,
) {
    val state = viewModel.state.collectAsState()

    if (state.value.loading) {
        FullScreenLoader()
    } else {
        StartScreenContent(state = state.value,
            onUsernameChange = { viewModel.onUsernameChange(it) },
            onNextClick = { viewModel.onNextClick(navigateToHome) })
    }
}

@Composable
fun StartScreenContent(
    state: StartScreenState = StartScreenState(),
    onUsernameChange: (String) -> Unit = {},
    onNextClick: () -> Unit = {}
) {
    Surface(
        modifier = Modifier.fillMaxSize()
    ) {
        Column(
            modifier = Modifier.fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            Text(
                text = stringResource(id = R.string.app_name),
                style = MaterialTheme.typography.displayLarge
            )
            TextField(value = state.username,
                onValueChange = onUsernameChange,
                label = { Text(text = stringResource(id = R.string.username)) })
            Button(
                onClick = onNextClick,
                enabled = state.canContinue,
            ) {
                if (state.loading) {
                    CircularProgressIndicator()
                } else {
                    Text(
                        text = stringResource(id = R.string.next),
                        style = MaterialTheme.typography.displayMedium
                    )
                }
            }
            state.error?.let {
                Text(
                    text = it.asString(), style = MaterialTheme.typography.bodySmall
                )
            }
        }
    }
}

@Preview
@Composable
fun StartScreenPreview() {
    BiteRightTheme {
        StartScreenContent()
    }
}