package com.sobczal2.biteright.routing

import androidx.compose.animation.core.tween
import androidx.compose.animation.scaleIn
import androidx.compose.animation.scaleOut
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Size
import androidx.navigation.NavController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.screens.AllProductsScreen
import com.sobczal2.biteright.screens.CreateProductScreen
import com.sobczal2.biteright.screens.CurrentProductsScreen
import com.sobczal2.biteright.screens.ProfileScreen
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()
    authManager.subscribeToLogoutEvent {
        handleNavigationEvent(NavigationEvent.NavigateToWelcome, navController)
    }
    var screenSize by remember { mutableStateOf(Size.Zero) }
    NavHost(
        navController = navController,
        startDestination = if (authManager.isLoggedIn) Routes.START else Routes.WELCOME,
        enterTransition =
        {
            scaleIn(
                animationSpec = tween(1000)
            )
        },
        exitTransition = {
            scaleOut(
                animationSpec = tween(1000)
            )
        },
        modifier = Modifier
    ) {
        composable(Routes.WELCOME) {
            WelcomeScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
        composable(Routes.START) {
            StartScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
        composable(Routes.CURRENT_PRODUCTS) {
            CurrentProductsScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
        composable(Routes.ALL_PRODUCTS) {
            AllProductsScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
        composable(Routes.PROFILE) {
            ProfileScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
        composable(Routes.CREATE_PRODUCT) {
            CreateProductScreen(
                handleNavigationEvent = { event -> handleNavigationEvent(event, navController) }
            )
        }
    }
}

private fun handleNavigationEvent(
    navigationEvent: NavigationEvent,
    navController: NavController
) {
    CoroutineScope(Dispatchers.Main).launch {
        when (navigationEvent) {
            is NavigationEvent.NavigateToWelcome -> navController.navigate(Routes.WELCOME)
            is NavigationEvent.NavigateToStart -> navController.navigate(Routes.START)
            is NavigationEvent.NavigateToCurrentProducts -> navController.navigate(Routes.CURRENT_PRODUCTS)
            is NavigationEvent.NavigateToAllProducts -> navController.navigate(Routes.ALL_PRODUCTS)
            is NavigationEvent.NavigateToTemplates -> navController.navigate(Routes.TEMPLATES)
            is NavigationEvent.NavigateToProfile -> navController.navigate(Routes.PROFILE)
            is NavigationEvent.NavigateToCreateProduct -> navController.navigate(Routes.CREATE_PRODUCT)
        }
    }
}