package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.state.AuthState
import com.sobczal2.biteright.viewmodels.StartViewModel

@Composable
fun Router() {
    val navController = rememberNavController()
    val isAuthenticated by AuthState.isAuthenticated.collectAsState()
    NavHost(
        navController = navController,
        startDestination = if (isAuthenticated) Routes.HOME else Routes.WELCOME
    ) {
        composable(Routes.WELCOME) {
            WelcomeScreen(
                navigateToStart = { navController.navigate(Routes.HOME) }
            )
        }
        composable(Routes.START) {
            val viewModel = hiltViewModel<StartViewModel>()

        }
    }
}