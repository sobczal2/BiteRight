package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.events.CurrentProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.CurrentProductsScreenState
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.products.ProductItem
import com.sobczal2.biteright.ui.components.products.ProductSummaryItemState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.util.getCategoryPhotoUrl
import com.sobczal2.biteright.viewmodels.CurrentProductsViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import java.time.LocalDate
import java.time.LocalDateTime
import java.util.UUID
import kotlin.time.Duration.Companion.milliseconds

@Composable
fun CurrentProductsScreen(
    viewModel: CurrentProductsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    ScaffoldLoader(
        loading = state.value.globalLoading
    ) {
        CurrentProductsScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun CurrentProductsScreenContent(
    state: CurrentProductsScreenState = CurrentProductsScreenState(),
    sendEvent: (CurrentProductsScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val coroutineScope = rememberCoroutineScope()
    val animationDuration = 300.milliseconds

    HomeLayout(
        currentTab = HomeLayoutTab.CURRENT_PRODUCTS,
        handleNavigationEvent = handleNavigationEvent,
        floatingActionButton = {
            FloatingActionButton(
                onClick = {
                    handleNavigationEvent(NavigationEvent.NavigateToCreateProduct)
                },
            ) {
                Column(
                    modifier = Modifier.padding(MaterialTheme.dimension.sm),
                    horizontalAlignment = androidx.compose.ui.Alignment.CenterHorizontally
                ) {
                    Text(text = stringResource(id = R.string.add_product))
                    Icon(
                        imageVector = Icons.Default.Add,
                        contentDescription = stringResource(id = R.string.add_product)
                    )
                }
            }
        }
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.xl),
        ) {
            Text(
                text = "Current Products",
                style = MaterialTheme.typography.displayMedium.plus(
                    TextStyle(textAlign = TextAlign.Center)
                ),
                modifier = Modifier
                    .fillMaxWidth()
            )
            LazyColumn(
                content = {
                    items(
                        items = state.currentProducts,
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
                                coroutineScope.launch {
                                    delay(animationDuration)
                                    sendEvent(
                                        CurrentProductsScreenEvent.OnProductDispose(
                                            simpleProductDto.id
                                        )
                                    )
                                }
                                true
                            },
                            imageRequestBuilder = state.imageRequestBuilder,
                        )

                        if (simpleProductDto != state.currentProducts.last()) {
                            HorizontalDivider(
                                modifier = Modifier.fillMaxWidth(),
                                color = MaterialTheme.colorScheme.onSurface
                            )
                        }
                    }
                },
            )
        }
    }
}

@Composable
@BiteRightPreview
fun CurrentProductsScreenPreview() {
    BiteRightTheme {
        CurrentProductsScreenContent(
            state = CurrentProductsScreenState(
                currentProducts = listOf(
                    SimpleProductDto(
                        id = UUID.randomUUID(),
                        name = "Product 1",
                        expirationDate = LocalDate.now(),
                        categoryId = UUID.randomUUID(),
                        amountPercentage = 50.0,
                        disposed = false,
                        addedDateTime = LocalDateTime.now()
                    ),
                )
            )
        )
    }
}