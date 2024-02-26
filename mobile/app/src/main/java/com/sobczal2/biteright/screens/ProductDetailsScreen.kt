package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProductDetailsScreenEvent
import com.sobczal2.biteright.state.ProductDetailsScreenState
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.ProductDetailsViewModel
import java.util.UUID

@Composable
fun ProductDetailsScreen(
    viewModel: ProductDetailsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
    productId: UUID
) {
    val state = viewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        viewModel.sendEvent(ProductDetailsScreenEvent.LoadDetails(productId))
    }

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
    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.SpaceBetween
        ) {
            Text(
                text = "Product details ${state.product?.name}",
            )
        }
    }
}

@Composable
@BiteRightPreview
fun ProductDetailsScreenPreview() {
    ProductDetailsScreenContent()
}