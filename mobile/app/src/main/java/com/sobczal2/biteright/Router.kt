package com.sobczal2.biteright

import android.content.Context
import androidx.compose.runtime.Composable
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.ui.screens.HomeScreen
import com.sobczal2.biteright.ui.screens.WelcomeScreen

object Routes {
    const val Home = "home"
    const val Welcome = "welcome"
}

@Composable
fun BiteRightRouter(auth0Manager: Auth0Manager) {
    val navController = rememberNavController()
    NavHost(navController = navController, startDestination = Routes.Welcome) {
        composable(Routes.Welcome) { WelcomeScreen(navController, auth0Manager) }
        composable(Routes.Home) { HomeScreen() }
    }
}