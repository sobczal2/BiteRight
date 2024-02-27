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
import com.sobczal2.biteright.screens.ProductDetailsScreen
import com.sobczal2.biteright.screens.ProfileScreen
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.util.getUUID
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()
    authManager.subscribeToLogoutEvent {
        handleNavigationEvent(NavigationEvent.NavigateToWelcome, navController)
    }
    NavHost(
        navController = navController,
        startDestination = if (authManager.isLoggedIn) Routes.START else Routes.WELCOME,
        modifier = Modifier
    ) {
        val handleNavigationEvent: (NavigationEvent) -> Unit = { event ->
            handleNavigationEvent(event, navController)
        }
        composable(Routes.WELCOME) {
            WelcomeScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.START) {
            StartScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.CURRENT_PRODUCTS) {
            CurrentProductsScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.ALL_PRODUCTS) {
            AllProductsScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.PROFILE) {
            ProfileScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.CREATE_PRODUCT) {
            CreateProductScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.PRODUCT_DETAILS) {
            ProductDetailsScreen(
                handleNavigationEvent = handleNavigationEvent,
                productId = it.arguments?.getUUID("productId")!!
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
            is NavigationEvent.NavigateBack -> navController.popBackStack()
            is NavigationEvent.NavigateToWelcome -> navController.navigate(Routes.WELCOME)
            is NavigationEvent.NavigateToStart -> navController.navigate(Routes.START)
            is NavigationEvent.NavigateToCurrentProducts -> navController.navigate(Routes.CURRENT_PRODUCTS)
            is NavigationEvent.NavigateToAllProducts -> navController.navigate(Routes.ALL_PRODUCTS)
            is NavigationEvent.NavigateToTemplates -> navController.navigate(Routes.TEMPLATES)
            is NavigationEvent.NavigateToProfile -> navController.navigate(Routes.PROFILE)
            is NavigationEvent.NavigateToCreateProduct -> navController.navigate(Routes.CREATE_PRODUCT)
            is NavigationEvent.NavigateToProductDetails -> navController.navigate(
                Routes.productDetails(navigationEvent.productId)
            )
            is NavigationEvent.NavigateToEditProduct -> navController.navigate(
                Routes.editProduct(navigationEvent.productId)
            )
        }
    }
}