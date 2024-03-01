package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.navigation.NavController

object RouterHelper {
    @Composable
    fun RunOnNavigate(
        navController: NavController,
        route: String,
        action: (String) -> Unit
    ) {
        val currentRoute = navController.currentDestination?.route

        LaunchedEffect(currentRoute) {
            if (currentRoute == route) {
                action(route)
            }
        }
    }
}