package com.sobczal2.biteright.ui.components.common

import androidx.compose.material3.Icon
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import com.sobczal2.biteright.R

@Composable
fun Logo(
    modifier: Modifier = Modifier
) {
    Icon(
        painter = painterResource(id = R.drawable.ic_logo),
        contentDescription = "logo",
        tint = Color.Unspecified,
        modifier = modifier
    )
}