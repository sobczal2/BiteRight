package com.sobczal2.biteright.screens

import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.events.AllProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.AllProductsScreenState
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.AllProductsViewModel

@Composable
fun AllProductsScreen(
    viewModel: AllProductsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    ScaffoldLoader(loading = state.value.globalLoading) {
        AllProductsScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun AllProductsScreenContent(
    state: AllProductsScreenState,
    sendEvent: (AllProductsScreenEvent) -> Unit,
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    HomeLayout(
        currentTab = HomeLayoutTab.ALL_PRODUCTS,
        handleNavigationEvent = handleNavigationEvent
    ) {

    }
}

@Composable
@BiteRightPreview
fun AllProductsScreenPreview() {
    BiteRightTheme {
        AllProductsScreenContent(
            state = AllProductsScreenState(),
            sendEvent = {},
            handleNavigationEvent = {},
        )
    }
}