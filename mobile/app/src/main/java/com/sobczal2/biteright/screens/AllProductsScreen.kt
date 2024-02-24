package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.ExtendedFloatingActionButton
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.AllProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.AllProductsScreenState
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.products.SwipeableProductListItem
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
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
    HomeLayout(currentTab = HomeLayoutTab.ALL_PRODUCTS,
        handleNavigationEvent = handleNavigationEvent,
        floatingActionButton = {
            ExtendedFloatingActionButton(
                onClick = {
                    handleNavigationEvent(NavigationEvent.NavigateToCreateProduct)
                },
            ) {
                Icon(
                    imageVector = Icons.Default.Add,
                    contentDescription = stringResource(id = R.string.add_product),
                    modifier = Modifier.padding(end = MaterialTheme.dimension.sm)
                )
                Text(text = stringResource(id = R.string.add_product))
            }
        }) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(
                    start = MaterialTheme.dimension.xl,
                    end = MaterialTheme.dimension.xl,
                    top = MaterialTheme.dimension.xl
                ),
        ) {
            Text(
                text = stringResource(id = R.string.all_products),
                style = MaterialTheme.typography.displayMedium.plus(
                    TextStyle(textAlign = TextAlign.Center)
                ),
                modifier = Modifier.fillMaxWidth()
            )

            ErrorBox(error = state.globalError)

            LazyColumn(content = {
                items(items = state.paginatedProductSource.items,
                    key = { it.id }) { simpleProductDto ->
                    SwipeableProductListItem(simpleProductDto = simpleProductDto, onDispose = {
                        sendEvent(AllProductsScreenEvent.OnProductDispose(simpleProductDto.id))
                        false
                    }, onRestore = {
                        sendEvent(AllProductsScreenEvent.OnProductRestore(simpleProductDto.id))
                        false
                    }, imageRequestBuilder = state.imageRequestBuilder
                    )

                    if (simpleProductDto != state.paginatedProductSource.items.last()) {
                        HorizontalDivider(
                            modifier = Modifier.fillMaxWidth(),
                            color = MaterialTheme.colorScheme.onSurface
                        )
                    }
                }

                item {
                    if (state.paginatedProductSource.hasMore.value) {
                        Row(
                            modifier = Modifier.fillMaxWidth(),
                            horizontalArrangement = Arrangement.Center
                        ) {
                            CircularProgressIndicator()
                        }
                        LaunchedEffect(Unit) {
                            sendEvent(AllProductsScreenEvent.FetchMoreProducts)
                        }
                    } else {
                        Box(
                            modifier = Modifier.height(MaterialTheme.dimension.xxl)
                        )
                    }
                }
            })
        }
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