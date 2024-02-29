package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.screens.AllProductsScreen
import com.sobczal2.biteright.screens.CreateProductScreen
import com.sobczal2.biteright.screens.CurrentProductsScreen
import com.sobczal2.biteright.screens.EditProductScreen
import com.sobczal2.biteright.screens.EditProfileScreen
import com.sobczal2.biteright.screens.ProductDetailsScreen
import com.sobczal2.biteright.screens.ProfileScreen
import com.sobczal2.biteright.screens.StartScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.ui.components.common.ComingSoonBanner
import com.sobczal2.biteright.util.getUUID
import com.sobczal2.biteright.viewmodels.AllProductsViewModel
import com.sobczal2.biteright.viewmodels.CurrentProductsViewModel
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
            val viewModel = hiltViewModel<CurrentProductsViewModel>()
            LaunchedEffect(navController.currentBackStackEntry?.destination?.route) {
                if (navController.currentBackStackEntry?.destination?.route == Routes.CURRENT_PRODUCTS)
                    viewModel.fetchCurrentProducts()
            }
            CurrentProductsScreen(
                viewModel = viewModel,
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.ALL_PRODUCTS) {
            val viewModel = hiltViewModel<AllProductsViewModel>()
            LaunchedEffect(navController.currentBackStackEntry?.destination?.route) {
                if (navController.currentBackStackEntry?.destination?.route == Routes.ALL_PRODUCTS)
                    viewModel.fetchAllProducts()
            }
            AllProductsScreen(
                viewModel = viewModel,
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
        composable(Routes.EDIT_PRODUCT) {
            EditProductScreen(
                handleNavigationEvent = handleNavigationEvent,
                productId = it.arguments?.getUUID("productId")!!
            )
        }
        composable(Routes.EDIT_PROFILE) {
            EditProfileScreen(
                handleNavigationEvent = handleNavigationEvent
            )
        }
        composable(Routes.TEMPLATES) {
            ComingSoonBanner {
                handleNavigationEvent(NavigationEvent.NavigateBack, navController)
            }
        }
    }
}

private fun handleNavigationEvent(
    navigationEvent: NavigationEvent,
    navController: NavController
) {
    if (navController.currentDestination?.route == navigationEvent.route) return
    CoroutineScope(Dispatchers.Main).launch {
        if (navigationEvent.route == null) {
            when (navigationEvent) {
                NavigationEvent.NavigateBack -> navController.popBackStack()
                else -> throw IllegalArgumentException("Unknown navigation event: $navigationEvent")
            }
        } else {
            navController.navigate(navigationEvent.route)
        }
    }
}