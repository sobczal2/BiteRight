package com.sobczal2.biteright.ui.screens

import android.util.Log
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxHeight
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.sobczal2.biteright.core.Auth0Manager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.core.Routes
import com.sobczal2.biteright.ui.components.common.AppLogo
import com.sobczal2.biteright.ui.components.common.MessageBox
import com.sobczal2.biteright.ui.components.common.MessageType
import com.sobczal2.biteright.ui.theme.size
import com.sobczal2.biteright.ui.theme.spacing
import com.sobczal2.biteright.viewmodel.screens.WelcomeViewModel

@Composable
fun WelcomeScreen(
    navController: NavController,
    auth0Manager: Auth0Manager,
    welcomeViewModel: WelcomeViewModel = hiltViewModel()
) {
    val state = welcomeViewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        if(welcomeViewModel.isLoggedIn()) {
            navController.navigate(Routes.Onboard)
        }
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(MaterialTheme.spacing.medium),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        AppLogo(
            Modifier
                .fillMaxHeight(0.3f)
                .fillMaxWidth()
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.large))
        Button(
            modifier = Modifier
                .fillMaxWidth()
                .height(MaterialTheme.size.large),
            onClick = {
                auth0Manager.login(
                    onSuccess = { credentials ->
                        welcomeViewModel.login(credentials)
                        welcomeViewModel.clearError()
                        navController.navigate(Routes.Onboard)
                        Log.d("WelcomeScreen", "idToken: ${credentials.idToken}")
                        Log.d("WelcomeScreen", "accessToken: ${credentials.accessToken}")
                    },
                    onFailure = {
                        welcomeViewModel.setError(it)
                    }
                )
            }) {
            Text(
                text = stringResource(R.string.get_started),
                style = MaterialTheme.typography.headlineMedium
            )
        }
        if (state.value.error != null) {
            MessageBox(
                message = state.value.error!!,
                messageType = MessageType.Error,
            )
        }
    }
}

@Composable
@Preview
fun WelcomeScreenPreview() {
    WelcomeScreen(
        navController = NavController(LocalContext.current),
        Auth0Manager(LocalContext.current)
    )
}