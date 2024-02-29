package com.sobczal2.biteright.screens

import androidx.compose.foundation.clickable
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
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.AllProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.AllProductsScreenState
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.products.AddProductActionButton
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
    val state = viewModel.state.collectAsStateWithLifecycle()
    ScaffoldLoader(loading = state.value.isLoading()) {
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
            AddProductActionButton {
                handleNavigationEvent(NavigationEvent.NavigateToCreateProduct)
            }
        }) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(
                    start = MaterialTheme.dimension.md,
                    end = MaterialTheme.dimension.md,
                    top = MaterialTheme.dimension.md
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
                    SwipeableProductListItem(
                        simpleProductDto = simpleProductDto,
                        onDispose = {
                            sendEvent(AllProductsScreenEvent.OnProductDispose(simpleProductDto.id))
                            false
                        },
                        onRestore = {
                            sendEvent(AllProductsScreenEvent.OnProductRestore(simpleProductDto.id))
                            false
                        },
                        imageRequestBuilder = state.imageRequestBuilder,
                        modifier = Modifier
                            .clickable {
                                handleNavigationEvent(
                                    NavigationEvent.NavigateToProductDetails(simpleProductDto.id)
                                )
                            }
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