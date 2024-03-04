package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Scaffold
import androidx.compose.material3.SnackbarHostState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.HelpScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.HelpScreenState
import com.sobczal2.biteright.ui.components.common.ErrorSnackbarHost
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.help.HelpCarousel
import com.sobczal2.biteright.ui.components.help.HelpCarouselEntry
import com.sobczal2.biteright.viewmodels.HelpViewModel


@Composable
fun HelpScreen(
    viewModel: HelpViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()
    val snackbarHostState = remember { SnackbarHostState() }

    SurfaceLoader(
        loading = state.value.isLoading()
    ) {
        HelpScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            topLevelNavigate = topLevelNavigate,
            snackbarHostState = snackbarHostState,
        )
    }
}

private val entries = listOf(
    HelpCarouselEntry(
        title = "Title 1",
        description = "Description 1",
        image = R.drawable.current_products_screenshot,
    ),
    HelpCarouselEntry(
        title = "Title 2",
        description = "Description 2",
        image = R.drawable.create_product_screenshot,
    ),
    HelpCarouselEntry(
        title = "Title 3",
        description = "Description 3",
        image = R.drawable.current_products_with_example_screenshot,
    ),
    HelpCarouselEntry(
        title = "Title 4",
        description = "Description 4",
        image = R.drawable.current_products_change_amount_screenshot,
    ),
    HelpCarouselEntry(
        title = "Title 5",
        description = "Description 5",
        image = R.drawable.current_products_dispose_screenshot,
    ),
)

@Composable
fun HelpScreenContent(
    state: HelpScreenState = HelpScreenState(),
    sendEvent: (HelpScreenEvent) -> Unit = { },
    topLevelNavigate: (Routes) -> Unit = { },
    snackbarHostState: SnackbarHostState,
) {

    Scaffold(
        snackbarHost = {
            ErrorSnackbarHost(
                snackbarHostState = snackbarHostState,
            )
        }
    ) { paddingValues ->
        Column {
            HelpCarousel(
                entries = entries,
                modifier = Modifier
                    .fillMaxSize()
                    .padding(paddingValues)
            )
        }
    }
}