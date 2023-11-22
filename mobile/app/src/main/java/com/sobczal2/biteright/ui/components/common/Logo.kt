package com.sobczal2.biteright.ui.components.common

import android.content.res.Configuration
import androidx.compose.material3.Icon
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.BiteRightTheme

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

@Preview(showBackground = true, uiMode = Configuration.UI_MODE_NIGHT_YES)
@Composable
fun LogoPreview() {
    BiteRightTheme {
        Logo()
    }
}