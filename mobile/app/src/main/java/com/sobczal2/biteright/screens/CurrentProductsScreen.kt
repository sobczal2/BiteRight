package com.sobczal2.biteright.screens

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.combinedClickable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
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
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.events.CurrentProductsScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.CurrentProductsScreenState
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.products.ChangeAmountDialog
import com.sobczal2.biteright.ui.components.products.SwipeableProductListItem
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.CurrentProductsViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import java.time.Instant
import java.time.LocalDate
import java.util.UUID

@Composable
fun CurrentProductsScreen(
    viewModel: CurrentProductsViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
    paddingValues: PaddingValues
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    SurfaceLoader(
        loading = state.value.isLoading(),
        modifier = Modifier
            .fillMaxSize()
            .padding(paddingValues),
    ) {
        CurrentProductsScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues),
            topLevelNavigate = topLevelNavigate
        )
    }
}

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun CurrentProductsScreenContent(
    modifier: Modifier = Modifier,
    state: CurrentProductsScreenState = CurrentProductsScreenState(),
    sendEvent: (CurrentProductsScreenEvent) -> Unit = {},
    topLevelNavigate: (Routes) -> Unit = {},
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

    Column(
        modifier = modifier
            .padding(
                start = MaterialTheme.dimension.md,
                end = MaterialTheme.dimension.md,
                top = MaterialTheme.dimension.md
            ),
    ) {
        Text(
            text = stringResource(id = R.string.current_products),
            style = MaterialTheme.typography.displaySmall.plus(
                TextStyle(textAlign = TextAlign.Center)
            ),
            modifier = Modifier
                .fillMaxWidth()
                .padding(bottom = MaterialTheme.dimension.md)
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
                                    topLevelNavigate(
                                        Routes.ProductDetails(
                                            productId = simpleProductDto.id
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
                        addedDateTime = Instant.now()
                    ),
                )
            )
        )
    }
}