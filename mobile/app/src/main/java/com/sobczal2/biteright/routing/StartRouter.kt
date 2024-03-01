package com.sobczal2.biteright.routing

import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.screens.OnboardScreen
import com.sobczal2.biteright.screens.WelcomeScreen
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.layouts.StartLayout

@Composable
fun StartRouter(
    viewModel: StartRouterViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
) {
    val navController = rememberNavController()
    val currentBackStackEntry = navController.currentBackStackEntryFlow.collectAsStateWithLifecycle(null)

    fun startNavigate(route: Routes.StartGraph) {
        if (route.routeWithParams == currentBackStackEntry.value?.destination?.route) return
        navController.navigate(route.route)
    }


    LaunchedEffect(currentBackStackEntry.value) {
        viewModel.navigateAccordingToUserAuthProgress(
            startNavigate = ::startNavigate,
            topLevelNavigate = topLevelNavigate
        )
    }

    val loading = viewModel.loading.collectAsStateWithLifecycle()

    StartLayout{
        SurfaceLoader(
            loading = loading.value,
        ) {
            NavHost(
                navController = navController,
                startDestination = Routes.StartGraph.Welcome.route,
            ) {
                composable(Routes.StartGraph.Welcome.route) {
                    WelcomeScreen(
                        startNavigate = ::startNavigate
                    )
                }

                composable(Routes.StartGraph.Onboard.route) {
                    OnboardScreen(
                        topLevelNavigate = topLevelNavigate
                    )
                }
            }
        }
    }
}