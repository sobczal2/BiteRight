package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.Image
import androidx.compose.runtime.Composable
import androidx.compose.ui.res.painterResource
import com.sobczal2.biteright.R

@Composable
fun AppLogo() {
    Image(
        painter = painterResource(id = R.drawable.ic_logo),
        contentDescription = "BiteRight logo"
    )
}

@Composable
fun AppLogoSquare() {
    Image(
        painter = painterResource(id = R.drawable.ic_logo_square),
        contentDescription = "BiteRight logo"
    )
}