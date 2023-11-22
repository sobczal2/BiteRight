package com.sobczal2.biteright.ui.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.sobczal2.biteright.ui.components.common.Logo
import com.sobczal2.biteright.ui.components.common.SpacerType
import com.sobczal2.biteright.ui.components.common.VSpacer
import com.sobczal2.biteright.ui.components.common.forms.BiteRightButton
import com.sobczal2.biteright.viewmodel.screens.SignInViewModel

@Composable
fun SignInScreen(
    signInViewModel: SignInViewModel = viewModel()
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
        VSpacer(type = SpacerType.ExtraLarge)
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.email,
            onValueChange = { email -> signInViewModel.onEmailChanged(email) },
            label = { Text(text = "email") })
        VSpacer(type = SpacerType.Small)
        TextField(
            modifier = Modifier.fillMaxWidth(),
            value = state.password,
            onValueChange = { password -> signInViewModel.onPasswordChanged(password) },
            label = { Text(text = "password") },
            visualTransformation = PasswordVisualTransformation(
                mask = '*'
            )
        )
        VSpacer(type = SpacerType.Large)
        BiteRightButton(
            text = "Sign in",
            loading = state.loading,
            enabled = state.submitEnabled,
            onClick = { signInViewModel.onSignInClicked() }
        )
    }
}

@Preview
@Composable
fun SignInScreenPreview() {
    SignInScreen()
}
