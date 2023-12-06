package com.sobczal2.biteright.ui.screens

import BiteRightErrorLabel
import android.content.res.Configuration.UI_MODE_NIGHT_YES
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
import com.sobczal2.biteright.viewmodel.screens.SignUpViewModel

@Composable
fun SignUpScreen(
    navController: NavController,
    signUpViewModel: SignUpViewModel = hiltViewModel()
) {
    val state by signUpViewModel.state.collectAsState()
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
        BiteRightErrorLabel(
            enabled = state.error.email.isNotEmpty(),
            text = state.error.email,
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = MaterialTheme.spacing.small)
        )
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.email,
            onValueChange = { email -> signUpViewModel.onEmailChanged(email) },
            label = { Text(text = "email") },
            isError = state.error.email.isNotEmpty()
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.small))
        BiteRightErrorLabel(
            enabled = state.error.name.isNotEmpty(),
            text = state.error.name,
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = MaterialTheme.spacing.small)
        )
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.name,
            onValueChange = { name -> signUpViewModel.onNameChanged(name) },
            label = { Text(text = "name") },
            isError = state.error.name.isNotEmpty()
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.small))
        BiteRightErrorLabel(
            enabled = state.error.password.isNotEmpty(),
            text = state.error.password,
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = MaterialTheme.spacing.small)
        )
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.password,
            onValueChange = { password -> signUpViewModel.onPasswordChanged(password) },
            label = { Text(text = "password") },
            visualTransformation = PasswordVisualTransformation(
                mask = '*'
            ),
            isError = state.error.password.isNotEmpty()
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.medium))
        BiteRightErrorLabel(
            enabled = state.error.unknown.isNotEmpty(),
            text = state.error.unknown,
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = MaterialTheme.spacing.small)
        )
        BiteRightButton(
            text = "Sign up",
            loading = state.loading,
            enabled = state.submitEnabled,
            onClick = {
                signUpViewModel.onSignUpClicked(
                    onSuccess = { navController.navigate(Routes.Home) }
                )
            }
        )
    }
}

@Preview(showBackground = true, uiMode = UI_MODE_NIGHT_YES)
@Composable
fun SignUpScreenPreview() {
    BiteRightTheme {
        SignUpScreen(navController = NavController(LocalContext.current))
    }
}