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
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.CurrentProductsScreenState
import com.sobczal2.biteright.ui.components.common.BigLoader
import com.sobczal2.biteright.ui.components.common.MainAppLayout
import com.sobczal2.biteright.ui.components.common.MainAppLayoutActions
import com.sobczal2.biteright.ui.components.common.MainAppLayoutTab
import com.sobczal2.biteright.ui.components.products.ProductSummaryItem
import com.sobczal2.biteright.ui.components.products.ProductSummaryItemState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.getCategoryPhotoUrl
import com.sobczal2.biteright.viewmodels.CurrentProductsViewModel
import java.util.UUID

@Composable
fun CurrentProductsScreen(
    viewModel: CurrentProductsViewModel = hiltViewModel(),
    navigateToCreateProduct: () -> Unit,
    mainAppLayoutActions: MainAppLayoutActions,
) {
    val state = viewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        viewModel.fetchCurrentProducts()
    }

    if (state.value.loading) {
        BigLoader()
    } else {
        CurrentProductsScreenContent(
            state = state.value,
            mainAppLayoutActions = mainAppLayoutActions,
            navigateToCreateProduct = navigateToCreateProduct,
            disposeProduct = { viewModel.disposeProduct(it) }
        )
    }
}

@Composable
fun CurrentProductsScreenContent(
    state: CurrentProductsScreenState = CurrentProductsScreenState(),
    mainAppLayoutActions: MainAppLayoutActions = MainAppLayoutActions(),
    navigateToCreateProduct: () -> Unit = {},
    disposeProduct: (UUID) -> Unit = {}
) {
    MainAppLayout(
        currentTab = MainAppLayoutTab.CURRENT_PRODUCTS,
        mainAppLayoutActions = mainAppLayoutActions,
        floatingActionButton = {
            FloatingActionButton(
                onClick = navigateToCreateProduct
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
        Surface(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues = paddingValues)
        ) {
            Column {
                Text(
                    text = "Current Products",
                    style = MaterialTheme.typography.displayMedium.plus(
                        TextStyle(textAlign = TextAlign.Center)
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(MaterialTheme.dimension.sm)
                )
                LazyColumn(
                    content = {
                        items(
                            items = state.currentProducts,
                            key = { it.id }
                        ) { simpleProductDto ->
                            ProductSummaryItem(
                                productSummaryItemState = ProductSummaryItemState(
                                    name = simpleProductDto.name,
                                    expirationDate = simpleProductDto.expirationDate,
                                    categoryImageUri = getCategoryPhotoUrl(categoryId = simpleProductDto.categoryId),
                                    amountPercentage = simpleProductDto.amountPercentage,
                                    disposed = simpleProductDto.disposed,
                                ),
                                onClick = { /*TODO*/ },
                                onDeleted = { disposeProduct(simpleProductDto.id) }
                            )
                        }
                    },
                )
            }
        }
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun CurrentProductsScreenPreview() {
    BiteRightTheme {
        CurrentProductsScreenContent()
    }
}