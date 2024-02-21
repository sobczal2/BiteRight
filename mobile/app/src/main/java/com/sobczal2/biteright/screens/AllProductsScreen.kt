package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Button
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.events.AllProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.AllProductsScreenState
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.products.ProductItem
import com.sobczal2.biteright.ui.components.products.ProductSummaryItemState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.util.getCategoryPhotoUrl
import com.sobczal2.biteright.viewmodels.AllProductsViewModel
import java.time.LocalDate

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
        Column {
            LazyColumn(content = {
                items(
                    items = state.products.items,
                    key = { it.id }
                ) { simpleProductDto ->
                    ProductItem(
                        productSummaryItemState = ProductSummaryItemState(
                            name = simpleProductDto.name,
                            expirationDate = simpleProductDto.expirationDate
                                ?: LocalDate.MIN, // TODO: workaround for now
                            categoryImageUri = getCategoryPhotoUrl(categoryId = simpleProductDto.categoryId),
                            amountPercentage = simpleProductDto.amountPercentage,
                            disposed = simpleProductDto.disposed,
                        ),
                        onClick = { /*TODO*/ },
                        onDisposed = {
                            sendEvent(
                                AllProductsScreenEvent.OnProductDispose(
                                    simpleProductDto.id
                                )
                            )
                            false
                        },
                        onRestored = {
                            sendEvent(
                                AllProductsScreenEvent.OnProductRestore(
                                    simpleProductDto.id
                                )
                            )
                            false
                        },
                        imageRequestBuilder = state.imageRequestBuilder,
                    )

                    if (simpleProductDto != state.products.items.last()) {
                        HorizontalDivider(
                            modifier = Modifier.fillMaxWidth(),
                            color = MaterialTheme.colorScheme.onSurface
                        )
                    }
                }
            })
            Button(
                onClick = { sendEvent(AllProductsScreenEvent.ReloadProducts) }
            ) {
                Text("Co jest kurwa?")
            }
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