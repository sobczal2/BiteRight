package com.sobczal2.biteright.ui.screens

import android.content.Context
import android.content.res.Configuration
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.Auth0Manager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.components.common.AppLogo
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.spacing
import com.sobczal2.biteright.viewmodel.screens.WelcomeViewModel

@Composable
fun WelcomeScreen(
    navController: NavController,
    auth0Manager: Auth0Manager,
    welcomeViewModel: WelcomeViewModel = hiltViewModel()
) {
    Column {
        AppLogo()
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.large))
        Button(onClick = {
            auth0Manager.login(
                onSuccess = {
                    navController.navigate("home")
                },
                onFailure = {

                });
        }) {
            Text(stringResource(R.string.get_started))
        }
    }
}