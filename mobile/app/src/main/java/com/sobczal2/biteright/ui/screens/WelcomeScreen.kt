package com.sobczal2.biteright.ui.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.navigation.NavController
import androidx.navigation.compose.rememberNavController
import com.sobczal2.biteright.Routes
import com.sobczal2.biteright.ui.components.common.Logo
import com.sobczal2.biteright.ui.components.common.SpacerType
import com.sobczal2.biteright.ui.components.common.VSpacer
import com.sobczal2.biteright.ui.components.common.forms.BiteRightButton

@Composable
fun WelcomeScreen(navController: NavController) {
    Column(
        modifier = Modifier
            .fillMaxSize()
            .padding(50.dp),
        horizontalAlignment = Alignment.CenterHorizontally,
        verticalArrangement = Arrangement.Center
    ) {
        Logo(
            modifier = Modifier
                .fillMaxWidth()
                .height(200.dp)
        )
        VSpacer(type = SpacerType.ExtraLarge)
        BiteRightButton(
            text = "Sign up",
            loading = false,
            enabled = true,
            onClick = { navController.navigate(Routes.SignUp) }
        )
        VSpacer(type = SpacerType.Medium)
        BiteRightButton(
            text = "Sign in",
            loading = false,
            enabled = true,
            onClick = { navController.navigate(Routes.SignIn) }
        )
    }
}

@Preview
@Composable
fun WelcomeScreenPreview() {
    val navController = rememberNavController()
    WelcomeScreen(navController = navController)
}