package com.sobczal2.biteright.routing

import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.routing.RouterHelper.RunOnNavigate
import com.sobczal2.biteright.screens.AllProductsScreen
import com.sobczal2.biteright.screens.CurrentProductsScreen
import com.sobczal2.biteright.screens.ProfileScreen
import com.sobczal2.biteright.ui.components.common.ComingSoonBanner
import com.sobczal2.biteright.ui.layouts.HomeLayout
import com.sobczal2.biteright.viewmodels.AllProductsViewModel
import com.sobczal2.biteright.viewmodels.CurrentProductsViewModel
import com.sobczal2.biteright.viewmodels.ProfileViewModel

@Composable
fun HomeRouter(
    topLevelNavigate: (Routes) -> Unit,
) {
    val navController = rememberNavController()

    fun homeNavigate(route: Routes.HomeGraph) {
        navController.navigate(route.routeWithParams)
    }

    val currentBackStackEntry = navController.currentBackStackEntryFlow.collectAsStateWithLifecycle(null)

    HomeLayout(
        currentRoute = currentBackStackEntry.value?.destination.toRoute(),
        homeNavigate = ::homeNavigate,
        topLevelNavigate = topLevelNavigate,
    ) { paddingValues ->
        NavHost(
            navController = navController,
            startDestination = Routes.HomeGraph.CurrentProducts.route,
        ) {
            composable(Routes.HomeGraph.CurrentProducts.route) {
                val viewModel = hiltViewModel<CurrentProductsViewModel>()
                RunOnNavigate(
                    navController = navController,
                    route = Routes.HomeGraph.CurrentProducts.route
                ) {
                    viewModel.fetchCurrentProducts()
                }
                CurrentProductsScreen(
                    viewModel = viewModel,
                    topLevelNavigate = topLevelNavigate,
                    paddingValues = paddingValues
                )
            }
            composable(Routes.HomeGraph.AllProducts.route) {
                val viewModel = hiltViewModel<AllProductsViewModel>()
                RunOnNavigate(
                    navController = navController,
                    route = Routes.HomeGraph.AllProducts.route
                ) {
                    viewModel.fetchAllProducts()
                }
                AllProductsScreen(
                    viewModel = viewModel,
                    topLevelNavigate = topLevelNavigate,
                    paddingValues = paddingValues
                )
            }
            composable(Routes.HomeGraph.Templates.route) {
                ComingSoonBanner(
                    modifier = Modifier
                        .fillMaxSize()
                        .padding(paddingValues)
                )
            }
            composable(Routes.HomeGraph.Profile.route) {
                val viewModel = hiltViewModel<ProfileViewModel>()
                RunOnNavigate(
                    navController = navController,
                    route = Routes.HomeGraph.Profile.route
                ) {
                    viewModel.fetchUserData()
                }
                ProfileScreen(
                    viewModel = viewModel,
                    topLevelNavigate = topLevelNavigate,
                    paddingValues = paddingValues
                )
            }
        }
    }
}