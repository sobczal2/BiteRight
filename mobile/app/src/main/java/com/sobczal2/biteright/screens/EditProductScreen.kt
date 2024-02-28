package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.products.DetailedProductDto
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.dto.units.UnitSystemDto
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.events.EditProductScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.EditProductScreenState
import com.sobczal2.biteright.ui.components.categories.CategoryFormField
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormField
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.EditProductViewModel
import java.time.Instant
import java.time.LocalDate
import java.util.UUID

@Composable
fun EditProductScreen(
    viewModel: EditProductViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
    productId: UUID
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    LaunchedEffect(Unit) {
        viewModel.sendEvent(EditProductScreenEvent.LoadDetails(productId))
    }

    ScaffoldLoader(loading = state.value.globalLoading) {
        EditProductScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            searchCategories = viewModel::searchCategories,
            searchCurrencies = viewModel::searchCurrencies,
            searchUnits = viewModel::searchUnits,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun EditProductScreenContent(
    state: EditProductScreenState = EditProductScreenState(),
    sendEvent: (EditProductScreenEvent) -> Unit = {},
    searchCategories: suspend (String, PaginationParams) -> PaginatedList<CategoryDto> = { _, _ -> emptyPaginatedList() },
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto> = { _, _ -> emptyPaginatedList() },
    searchUnits: suspend (String, PaginationParams) -> PaginatedList<UnitDto> = { _, _ -> emptyPaginatedList() },
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
        ) {
            Column(
                modifier = Modifier.fillMaxWidth(),
                verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
                Text(
                    text = stringResource(id = R.string.edit_product),
                    style = MaterialTheme.typography.displaySmall.copy(
                        textAlign = TextAlign.Center
                    ),
                    modifier = Modifier.fillMaxWidth()
                )

                TextFormField(
                    modifier = Modifier.fillMaxWidth(),
                    state = state.nameFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnNameChange(it))
                    },
                    options = TextFormFieldOptions(
                        label = { Text(text = stringResource(id = R.string.name)) },
                    )
                )

                CategoryFormField(
                    state = state.categoryFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnCategoryChange(it))
                    },
                    searchCategories = searchCategories,
                    imageRequestBuilder = state.imageRequestBuilder
                )

                ExpirationDateFormField(
                    modifier = Modifier,
                    state = state.expirationDateFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnExpirationDateChange(it))
                    },
                )

                PriceFormField(
                    state = state.priceFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnPriceChange(it))
                    },
                    searchCurrencies = searchCurrencies
                )

                TextFormField(
                    modifier = Modifier.fillMaxWidth(),
                    state = state.descriptionFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnDescriptionChange(it))
                    },
                    options = TextFormFieldOptions(
                        label = { Text(text = stringResource(id = R.string.description)) },
                        singleLine = false,
                        minLines = 3,
                        maxLines = 5,
                    )
                )
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
                    Text(text = stringResource(id = R.string.cancel))
                }

                Button(
                    onClick = {
                        // TODO: Implement save product
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = stringResource(id = R.string.save))
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun EditProductScreenPreview() {
    EditProductScreenContent(
        state = EditProductScreenState(),
    )
}