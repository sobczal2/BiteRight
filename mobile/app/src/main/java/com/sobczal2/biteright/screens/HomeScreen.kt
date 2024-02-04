package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.state.HomeViewModelState
import com.sobczal2.biteright.util.asString
import com.sobczal2.biteright.viewmodels.HomeViewModel

@Composable
fun HomeScreen(
    viewModel: HomeViewModel = hiltViewModel(),
) {
    val state = viewModel.state.collectAsState()

    HomeScreenContent(
        state = state.value,
    )
}

@Composable
fun HomeScreenContent(
    state: HomeViewModelState = HomeViewModelState(),
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
            Text(text = "Username: ${state.username}")
            Text(text = "Email: ${state.email}")
            Text(text = "Loading: ${state.loading}")
            Text(text = "API Error: ${state.error?.asString()}")
        }
    }
}