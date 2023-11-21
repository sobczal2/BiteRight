package com.sobczal2.biteright

import androidx.compose.runtime.Composable
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.ui.screens.SignInScreen
import com.sobczal2.biteright.ui.screens.SignUpScreen
import com.sobczal2.biteright.ui.screens.WelcomeScreen

object Routes {
    const val Welcome = "welcome"
    const val SignUp = "signUp"
    const val SignIn = "signIn"
}

@Composable
fun BiteRightRouter() {
    val navController = rememberNavController()
    NavHost(navController = navController, startDestination = Routes.Welcome) {
        composable(Routes.Welcome) { WelcomeScreen(navController) }
        composable(Routes.SignUp) { SignUpScreen() }
        composable(Routes.SignIn) { SignInScreen() }
    }
}