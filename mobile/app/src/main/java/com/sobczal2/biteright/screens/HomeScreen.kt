package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.state.HomeViewModelState
import com.sobczal2.biteright.ui.components.BigLoader
import com.sobczal2.biteright.ui.components.BiteRightLogo
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.asString
import com.sobczal2.biteright.viewmodels.HomeViewModel

@Composable
fun HomeScreen(
    viewModel: HomeViewModel = hiltViewModel(), navigateToStart: () -> Unit = {}
) {
    val state = viewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        if (!viewModel.isOnboarded()) {
            navigateToStart()
        } else {
            viewModel.fetchUserData()
        }
    }

    if (state.value.loading) {
        BigLoader()
    } else {
        HomeScreenContent(
            state = state.value, onLogoutClick = viewModel::onLogoutClick
        )
    }
}

@Composable
fun HomeScreenContent(
    state: HomeViewModelState = HomeViewModelState(), onLogoutClick: () -> Unit = {}
) {
    Surface(
        modifier = Modifier.fillMaxSize()
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
            Text(text = "Username: ${state.username}")
            Text(text = "Email: ${state.email}")
            Text(text = "Loading: ${state.loading}")
            Text(text = "API Error: ${state.error?.asString()}")
            Button(onClick = onLogoutClick) {
                Text(text = "Logout")
            }
        }
    }
}

@Composable
@Preview
@Preview("Dark Theme", uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun HomeScreenPreview() {
    BiteRightTheme {
        HomeScreenContent()
    }
}