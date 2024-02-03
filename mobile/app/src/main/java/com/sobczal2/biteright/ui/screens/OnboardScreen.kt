package com.sobczal2.biteright.ui.screens

import android.content.Context
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
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
import com.sobczal2.biteright.R
import com.sobczal2.biteright.core.Routes
import com.sobczal2.biteright.ui.components.common.AppLogo
import com.sobczal2.biteright.ui.theme.size
import com.sobczal2.biteright.ui.theme.spacing
import com.sobczal2.biteright.viewmodel.screens.OnboardViewModel

@Composable
fun OnboardScreen(
    navController: NavController,
    context: Context = LocalContext.current,
    onboardViewModel: OnboardViewModel = hiltViewModel()
) {
    val state = onboardViewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        if (onboardViewModel.isOnboarded(context)) {
            navController.navigate(Routes.Home)
        }
    }

    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(MaterialTheme.spacing.large),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally
    ) {
        AppLogo()
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.large))
        TextField(
            value = state.value.username, onValueChange = {
                onboardViewModel.setUsername(it)
            },
            label = {
                Text(text = stringResource(id = R.string.username))
            },
            singleLine = true,
            modifier = Modifier
                .fillMaxWidth()
                .padding(MaterialTheme.spacing.medium)
        )
        Spacer(modifier = Modifier.height(MaterialTheme.spacing.large))
        Button(
            modifier = Modifier
                .fillMaxWidth()
                .height(MaterialTheme.size.large),
            onClick = {
                navController.navigate("home")
            }
        ) {
            Text(text = stringResource(id = R.string.next))
        }

        if (state.value.error.isNotEmpty()) {
            Text(text = state.value.error)
        }
    }
}

@Composable
@Preview
fun OnboardScreenPreview() {
    OnboardScreen(navController = NavController(LocalContext.current))
}
