package com.sobczal2.biteright.screens

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.combinedClickable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.ExtendedFloatingActionButton
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.events.CurrentProductsScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.CurrentProductsScreenState
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.products.ChangeAmountDialog
import com.sobczal2.biteright.ui.components.products.SwipeableProductListItem
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
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

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun CurrentProductsScreenContent(
    state: CurrentProductsScreenState = CurrentProductsScreenState(),
    sendEvent: (CurrentProductsScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val coroutineScope = rememberCoroutineScope()

    if (state.changeAmountDialogTargetId != null) {
        val product =
            state.currentProducts.find { it.id == state.changeAmountDialogTargetId } ?: return
        ChangeAmountDialog(
            onDismiss = {
                sendEvent(CurrentProductsScreenEvent.OnChangeAmountDialogDismiss)
            },
            onConfirm = {
                sendEvent(CurrentProductsScreenEvent.OnChangeAmountDialogConfirm(product.id, it))
            },
            loading = state.changeAmountDialogLoading,
            currentAmount = product.currentAmount,
            maxAmount = product.maxAmount,
            unitName = product.unitAbbreviation,
            productName = product.name,
        )
    }

    HomeLayout(
        currentTab = HomeLayoutTab.CURRENT_PRODUCTS,
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
        }
    ) { paddingValues ->
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
                text = stringResource(id = R.string.current_products),
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
                        var visible by remember { mutableStateOf(true) }
                        SwipeableProductListItem(
                            simpleProductDto = simpleProductDto,
                            onDispose = { animationDuration ->
                                visible = false
                                coroutineScope.launch {
                                    delay(animationDuration.toLong())
                                    sendEvent(
                                        CurrentProductsScreenEvent.OnProductDispose(
                                            simpleProductDto.id
                                        )
                                    )
                                }
                                true
                            },
                            imageRequestBuilder = state.imageRequestBuilder,
                            visible = visible,
                            modifier = Modifier
                                .combinedClickable(
                                    onClick = {
                                        handleNavigationEvent(
                                            NavigationEvent.NavigateToProductDetails(
                                                simpleProductDto.id
                                            )
                                        )
                                    },
                                    onLongClick = {
                                        sendEvent(
                                            CurrentProductsScreenEvent.OnProductLongClick(
                                                simpleProductDto.id
                                            )
                                        )
                                    }
                                )
                        )

                        if (simpleProductDto != state.currentProducts.last()) {
                            HorizontalDivider(
                                modifier = Modifier.fillMaxWidth(),
                                color = MaterialTheme.colorScheme.onSurface
                            )
                        }
                    }

                    item {
                        Box(
                            modifier = Modifier
                                .height(MaterialTheme.dimension.xxl)
                        )
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
                        expirationDateKind = ExpirationDateKindDto.BestBefore,
                        expirationDate = LocalDate.now(),
                        categoryId = UUID.randomUUID(),
                        currentAmount = 10.0,
                        maxAmount = 100.0,
                        unitAbbreviation = "kg",
                        disposed = false,
                        addedDateTime = LocalDateTime.now()
                    ),
                )
            )
        )
    }
}