package com.sobczal2.biteright.screens

import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.state.StartScreenState

@Composable
fun StartScreen(
    state: StartScreenState,
    onUsernameChange: (String) -> Unit = {},
    onNextClick: () -> Unit = {}
) {

}

@Composable
@Preview
fun StartScreenPreview() {
    StartScreen(
        state = StartScreenState()
    )
}