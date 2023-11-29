package com.sobczal2.biteright.ui.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.viewmodel.compose.viewModel
import androidx.navigation.NavController
import com.sobczal2.biteright.Routes
import com.sobczal2.biteright.ui.components.common.Logo
import com.sobczal2.biteright.ui.components.common.forms.BiteRightButton
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.spacing
import com.sobczal2.biteright.viewmodel.screens.SignInViewModel

@Composable
fun SignInScreen(
    navController: NavController,
    signInViewModel: SignInViewModel = hiltViewModel()
) {
    val state by signInViewModel.state.collectAsState()
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(50.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Logo(
            modifier = Modifier
                .fillMaxWidth()
                .height(200.dp)
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.extraLarge))
        TextField(
            modifier = Modifier
                .fillMaxWidth(),
            value = state.email,
            onValueChange = { email -> signInViewModel.onEmailChanged(email) },
            label = { Text(text = "email") })
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.small))
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.password,
            onValueChange = { password -> signInViewModel.onPasswordChanged(password) },
            label = { Text(text = "password") },
            visualTransformation = PasswordVisualTransformation(
                mask = '*'
            )
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.small))
        BiteRightButton(
            text = "Sign in",
            loading = state.loading,
            enabled = state.submitEnabled,
            onClick = {
                signInViewModel.onSignInClicked(
                    onSuccess = { navController.navigate(Routes.Home) }
                )
            }
        )

        if (state.error.isNotEmpty()) {
            Spacer(modifier = Modifier.height(MaterialTheme.spacing.small))
            Text(text = state.error)
        }
    }
}

@Preview(showBackground = true, uiMode = Configuration.UI_MODE_NIGHT_YES)
@Composable
fun SignInScreenPreview() {
    BiteRightTheme {
        SignInScreen(navController = NavController(LocalContext.current))
    }
}
