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
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.viewmodels.WelcomeViewModel

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(),
    navigateToStart: () -> Unit = {},
) {
    val state = viewModel.state.collectAsState()
    WelcomeScreenContent(
        onGetStartedClick = {
            viewModel.onGetStartedClick(
                onSuccess = navigateToStart
            )
        },
        error = state.value.error
    )
}

@Composable
@Preview(showBackground = true)
fun WelcomeScreenContent(
    onGetStartedClick: () -> Unit = {},
    error: String? = null,
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
                    text = stringResource(id = R.string.welcome_main_button_text),
                    style = MaterialTheme.typography.displayMedium
                )
            }
        }

        error?.let {
            Text(
                text = it,
                style = MaterialTheme.typography.bodySmall
            )
        }
    }
}