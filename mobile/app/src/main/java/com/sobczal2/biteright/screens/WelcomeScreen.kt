package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.animation.animateColor
import androidx.compose.animation.core.InfiniteRepeatableSpec
import androidx.compose.animation.core.LinearEasing
import androidx.compose.animation.core.RepeatMode
import androidx.compose.animation.core.keyframes
import androidx.compose.animation.core.rememberInfiniteTransition
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Offset
import androidx.compose.ui.graphics.Brush
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.ui.components.BiteRightLogo
import com.sobczal2.biteright.ui.components.ErrorBoxWrapped
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.WelcomeViewModel

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(), navigateToHome: () -> Unit = {}
) {
    val state = viewModel.state.collectAsState()

    val context = LocalContext.current

    WelcomeScreenContent(
        onGetStartedClick = {
            viewModel.onGetStartedClick(
                context = context, onSuccess = navigateToHome
            )
        }, state = state.value
    )
}


@Composable
fun WelcomeScreenContent(
    onGetStartedClick: () -> Unit = {}, state: WelcomeScreenState = WelcomeScreenState()
) {

    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(MaterialTheme.dimension.lg),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            BiteRightLogo(
                modifier = Modifier
                    .size(300.dp)
            )
            OutlinedButton(
                onClick = onGetStartedClick,
            ) {
                Text(
                    text = stringResource(id = R.string.get_started),
                    style = MaterialTheme.typography.displaySmall
                )
            }
            ErrorBoxWrapped(
                message = state.generalError,
            )
        }
    }
}

@Composable
@Preview
@Preview(uiMode = Configuration.UI_MODE_NIGHT_YES)
fun WelcomeScreenPreview() {
    BiteRightTheme {
        WelcomeScreenContent()
    }
}