package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.products.DetailedProductDto
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.dto.units.UnitSystemDto
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProductDetailsScreenEvent
import com.sobczal2.biteright.state.ProductDetailsScreenState
import com.sobczal2.biteright.ui.components.categories.CategoryImage
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.mediumStart
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.ProductDetailsViewModel
import java.time.LocalDate
import java.time.LocalDateTime
import java.util.UUID

@Composable
fun ProductDetailsScreen(
    viewModel: ProductDetailsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
    productId: UUID
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

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
    val product = state.product!!
    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
        ) {
            Card(
                modifier = Modifier
                    .fillMaxWidth()
            ) {
                Row {
                    CategoryImage(
                        categoryId = product.category.id,
                        shape = MaterialTheme.shapes.mediumStart,
                        modifier = Modifier.size(MaterialTheme.dimension.xxxl)
                    )
                    Column(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(MaterialTheme.dimension.sm),
                        verticalArrangement = Arrangement.Center
                    ) {
                        Text(
                            text = product.name,
                            style = MaterialTheme.typography.headlineMedium
                        )
                        Text(
                            text = product.category.name,
                            style = MaterialTheme.typography.labelMedium
                        )
                    }
                }
            }

            Card {
                Column(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(MaterialTheme.dimension.md),
                    verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.sm)
                ) {
                    Text(
                        text = product.description,
                        style = MaterialTheme.typography.bodyMedium
                    )
                    Text(
                        text = stringResource(
                            id = product.expirationDateKind.toLocalizedResourceID(),
                        ),
                        style = MaterialTheme.typography.bodyMedium
                    )
                }
            }

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
                OutlinedButton(
                    onClick = {
                        handleNavigationEvent(NavigationEvent.NavigateBack)
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                )
                {
                    Text(text = stringResource(id = R.string.back))
                }

                Button(
                    onClick = {
                        handleNavigationEvent(NavigationEvent.NavigateToEditProduct(productId = product.id))
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = stringResource(id = R.string.edit))
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun ProductDetailsScreenPreview() {
    ProductDetailsScreenContent(
        state = ProductDetailsScreenState(
            product = DetailedProductDto(
                UUID.randomUUID(),
                "Product name",
                "Product description",
                10.0,
                CurrencyDto(
                    UUID.randomUUID(),
                    "Złoty",
                    "zł",
                    "PLN"
                ),
                ExpirationDateKindDto.BestBefore,
                LocalDate.now(),
                CategoryDto(
                    UUID.randomUUID(),
                    "Category name"
                ),
                LocalDateTime.now(),
                10.0,
                100.0,
                UnitDto(
                    UUID.randomUUID(),
                    "Kilogram",
                    "kg",
                    UnitSystemDto.Metric
                ),
                false,
                null
            )
        )
    )
}