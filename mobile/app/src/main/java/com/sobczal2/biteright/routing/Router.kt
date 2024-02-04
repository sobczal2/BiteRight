package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.viewmodels.StartViewModel

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()
    NavHost(
        navController = navController,
        startDestination = if (authManager.isLoggedIn) Routes.START else Routes.WELCOME
    ) {
        composable(Routes.WELCOME) {
            WelcomeScreen(
                navigateToStart = { navController.navigate(Routes.START) },
                authManager = authManager,
            )
        }
        composable(Routes.START) {
            StartScreen(
                navigateToHome = { navController.navigate(Routes.HOME) }
            )
        }
    }
}