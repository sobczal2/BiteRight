package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.screens.HomeScreen
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.viewmodels.StartViewModel

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()
    authManager.subscribeToLogoutEvent { navController.navigate(Routes.WELCOME) }
    NavHost(
        navController = navController,
        startDestination = if (authManager.isLoggedIn) Routes.HOME else Routes.WELCOME
    ) {
        composable(Routes.WELCOME) {
            WelcomeScreen(
                navigateToHome = { navController.navigate(Routes.HOME) },
            )
        }
        composable(Routes.START) {
            StartScreen(
                navigateToHome = { navController.navigate(Routes.HOME) }
            )
        }
        composable(Routes.HOME) {
            HomeScreen(
                navigateToStart = { navController.navigate(Routes.START) }
            )
        }
    }
}