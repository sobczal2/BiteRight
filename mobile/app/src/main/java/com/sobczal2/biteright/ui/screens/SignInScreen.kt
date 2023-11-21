package com.sobczal2.biteright.ui.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Button
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.PasswordVisualTransformation
import androidx.compose.ui.unit.dp
import androidx.lifecycle.viewmodel.compose.viewModel
import com.sobczal2.biteright.viewmodel.screens.SignInViewModel

@Composable
fun SignInScreen(
    signInViewModel: SignInViewModel = viewModel()
) {
    val state by signInViewModel.state.collectAsState()
    Column(
        modifier = Modifier.fillMaxSize(),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        TextField(
            value = state.email,
            onValueChange = { email -> signInViewModel.onEmailChanged(email) },
            label = { Text(text = "email") })
        Spacer(modifier = Modifier.height(8.dp))
        TextField(
            value = state.password,
            onValueChange = { password -> signInViewModel.onPasswordChanged(password) },
            label = { Text(text = "password") },
            visualTransformation = PasswordVisualTransformation(
                mask = '*'
            )
        )
        Spacer(modifier = Modifier.height(8.dp))
        Button(onClick = { }) {
            Text(text = "Sign in")
        }
    }
}
