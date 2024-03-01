package com.sobczal2.biteright.routing

import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.screens.CreateProductScreen
import com.sobczal2.biteright.screens.EditProductScreen
import com.sobczal2.biteright.screens.EditProfileScreen
import com.sobczal2.biteright.screens.ProductDetailsScreen
import com.sobczal2.biteright.util.getUUID
import com.sobczal2.biteright.viewmodels.EditProfileViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Composable
fun Router(authManager: AuthManager) {
    val navController = rememberNavController()

    fun topLevelNavigate(route: Routes) {
        navController.navigate(route.routeWithParams)
    }

    LaunchedEffect(Unit) {
        authManager.subscribeToLogoutEvent {
            CoroutineScope(Dispatchers.Main).launch {
                navController.navigate(Routes.StartGraph().routeWithParams)
            }
        }
    }

    NavHost(
        navController = navController,
        startDestination = Routes.StartGraph().route,
        modifier = Modifier
    ) {
        composable(
            route = Routes.StartGraph().route,
        ) {
            StartRouter(
                topLevelNavigate = ::topLevelNavigate
            )
        }
        composable(
            route = Routes.HomeGraph().route
        ) {
            HomeRouter(
                topLevelNavigate = ::topLevelNavigate
            )
        }
        composable(Routes.CreateProduct.route) {
            CreateProductScreen(
                topLevelNavigate = ::topLevelNavigate
            )
        }
        composable(Routes.ProductDetails().route) {
            ProductDetailsScreen(
                productId = it.arguments?.getUUID("productId")!!,
                topLevelNavigate = ::topLevelNavigate,
            )
        }
        composable(Routes.EditProduct().route) {
            EditProductScreen(
                productId = it.arguments?.getUUID("productId")!!,
                topLevelNavigate = ::topLevelNavigate
            )
        }
        composable(Routes.EditProfile.route) {
            val viewModel = hiltViewModel<EditProfileViewModel>()
            RouterHelper.RunOnNavigate(
                navController = navController,
                route = Routes.EditProfile.route
            ) {
                viewModel.fetchUserData()
            }
            EditProfileScreen(
                viewModel = viewModel,
                topLevelNavigate = ::topLevelNavigate
            )
        }
    }
}