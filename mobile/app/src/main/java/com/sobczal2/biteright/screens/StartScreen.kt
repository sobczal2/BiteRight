package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.ui.components.BigLoader
import com.sobczal2.biteright.ui.components.BiteRightLogo
import com.sobczal2.biteright.ui.components.ErrorBoxWrapped
import com.sobczal2.biteright.ui.components.ValidatedTextField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.ResourceIdOrString
import com.sobczal2.biteright.viewmodels.StartViewModel


@Composable
fun StartScreen(
    viewModel: StartViewModel = hiltViewModel(),
    navigateToHome: () -> Unit,
) {
    val state = viewModel.state.collectAsState()

    if (state.value.loading) {
        BigLoader()
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
        modifier = Modifier
            .fillMaxSize()
    ) {
        Column(
            modifier = Modifier.fillMaxSize(),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            BiteRightLogo(
                modifier = Modifier
                    .size(300.dp)
            )
            ValidatedTextField(
                value = state.username,
                onValueChange = onUsernameChange,
                error = state.usernameError,
                label = { Text(text = stringResource(id = R.string.username)) },
                singleLine = true,
                imeAction = ImeAction.Done
            )
            Button(
                onClick = onNextClick,
            ) {
                if (state.loading) {
                    CircularProgressIndicator()
                } else {
                    Text(
                        text = stringResource(id = R.string.next),
                        style = MaterialTheme.typography.displaySmall
                    )
                }
            }
            ErrorBoxWrapped(message = state.generalError)
        }
    }
}

@Composable
@Preview
@Preview("Dark Theme", uiMode = Configuration.UI_MODE_NIGHT_YES)
fun StartScreenPreview() {
    val state =
        StartScreenState().copy(usernameError = ResourceIdOrString(R.string.username_length_error))
    BiteRightTheme {
        StartScreenContent(
            state = state,
            onUsernameChange = {},
            onNextClick = {}
        )
    }
}