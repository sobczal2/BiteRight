package com.sobczal2.biteright.ui.screens

import android.util.Log
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import com.sobczal2.biteright.core.Auth0Manager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.core.Routes
import com.sobczal2.biteright.ui.components.common.AppLogo
import com.sobczal2.biteright.ui.theme.spacing
import com.sobczal2.biteright.viewmodel.screens.WelcomeViewModel

@Composable
fun WelcomeScreen(
    navController: NavController,
    auth0Manager: Auth0Manager,
    welcomeViewModel: WelcomeViewModel = hiltViewModel()
) {
    val state = welcomeViewModel.state.collectAsState()
    val context = LocalContext.current

    Column {
        AppLogo()
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.large))
        Button(onClick = {
            auth0Manager.login(
                onSuccess = {
                    welcomeViewModel.clearError()
                    navController.navigate(Routes.Onboard)
                    Log.d("WelcomeScreen", "idToken: ${it.idToken}")
                    Log.d("WelcomeScreen", "accessToken: ${it.accessToken}")
                },
                onFailure = {
                    welcomeViewModel.setError(it)
                }
            )
        }) {
            Text(stringResource(R.string.get_started))
        }
        if (state.value.error != null) {
            Text(text = state.value.error!!)
        }
    }
}