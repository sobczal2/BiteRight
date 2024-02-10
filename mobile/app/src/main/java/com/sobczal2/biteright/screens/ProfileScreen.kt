package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.state.ProfileScreenState
import com.sobczal2.biteright.ui.components.common.MainAppLayout
import com.sobczal2.biteright.ui.components.common.MainAppLayoutActions
import com.sobczal2.biteright.ui.components.common.MainAppLayoutTab
import com.sobczal2.biteright.viewmodels.ProfileViewModel

@Composable
fun ProfileScreen(
    viewModel: ProfileViewModel = hiltViewModel(),
    mainAppLayoutActions: MainAppLayoutActions
) {
    val state = viewModel.state.collectAsState()

    ProfileScreenContent(
        state = state.value,
        mainAppLayoutActions = mainAppLayoutActions,
        onLogoutClick = viewModel::logout
    )
}

@Composable
fun ProfileScreenContent(
    state: ProfileScreenState = ProfileScreenState(),
    mainAppLayoutActions: MainAppLayoutActions = MainAppLayoutActions(),
    onLogoutClick: () -> Unit = {}
) {
    MainAppLayout(
        currentTab = MainAppLayoutTab.PROFILE,
        mainAppLayoutActions = mainAppLayoutActions
    ) {paddingValues ->
        Surface(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues = paddingValues)
        ) {
            Button(onClick = onLogoutClick) {
                Text("Logout")
            }
        }
    }
}