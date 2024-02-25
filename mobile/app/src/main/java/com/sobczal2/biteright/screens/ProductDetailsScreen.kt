package com.sobczal2.biteright.screens

import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProductDetailsScreenEvent
import com.sobczal2.biteright.state.ProductDetailsScreenState
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.viewmodels.ProductDetailsViewModel

@Composable
fun ProductDetailsScreen(
    viewModel: ProductDetailsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    ScaffoldLoader(loading = state.value.globalLoading) {
        ProductDetailsScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun ProductDetailsScreenContent(
    state: ProductDetailsScreenState = ProductDetailsScreenState(),
    sendEvent: (ProductDetailsScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
//    Text(text = "ProductDetailsScreenContent for ${state.productId}")
}