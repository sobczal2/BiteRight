package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.screens.CurrentProductsScreen
import com.sobczal2.biteright.screens.ProfileScreen
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.ui.components.common.MainAppLayoutActions

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()
    authManager.subscribeToLogoutEvent { navController.navigate(Routes.WELCOME) }
    val mainAppLayoutActions = MainAppLayoutActions(
        onCurrentProductsClick = { navController.navigate(Routes.CURRENT_PRODUCTS) },
        onAllProductsClick = { navController.navigate(Routes.ALL_PRODUCTS) },
        onTemplatesClick = { navController.navigate(Routes.TEMPLATES) },
        onProfileClick = { navController.navigate(Routes.PROFILE) },
    )
    NavHost(
        navController = navController,
        startDestination = if (authManager.isLoggedIn) Routes.CURRENT_PRODUCTS else Routes.WELCOME
    ) {
        composable(Routes.WELCOME) {
            WelcomeScreen(
                navigateToHome = { navController.navigate(Routes.CURRENT_PRODUCTS) },
            )
        }
        composable(Routes.START) {
            StartScreen(
                navigateToHome = { navController.navigate(Routes.CURRENT_PRODUCTS) }
            )
        }
        composable(Routes.CURRENT_PRODUCTS) {
            CurrentProductsScreen(
                navigateToStart = { navController.navigate(Routes.START) },
                mainAppLayoutActions = mainAppLayoutActions
            )
        }
        composable(Routes.PROFILE) {
            ProfileScreen(
                mainAppLayoutActions = mainAppLayoutActions
            )
        }
    }
}