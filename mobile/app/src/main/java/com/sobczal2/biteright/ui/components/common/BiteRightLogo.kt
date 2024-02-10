package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.Image
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.painterResource
import com.sobczal2.biteright.R

@Composable
fun BiteRightLogo(
    modifier: Modifier = Modifier
) {
    Image(
        painter = painterResource(R.drawable.logo),
        contentDescription = "BiteRight Logo",
        modifier = modifier
    )
}