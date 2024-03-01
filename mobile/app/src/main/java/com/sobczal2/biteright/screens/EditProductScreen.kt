package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
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
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.events.EditProductScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.EditProductScreenState
import com.sobczal2.biteright.ui.components.categories.CategoryFormField
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.products.AmountFormField
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormField
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.EditProductViewModel
import java.util.UUID

@Composable
fun EditProductScreen(
    viewModel: EditProductViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
    productId: UUID
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    LaunchedEffect(Unit) {
        viewModel.sendEvent(EditProductScreenEvent.LoadDetails(productId))
    }

    SurfaceLoader(loading = state.value.isLoading()) {
        EditProductScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            searchCategories = viewModel::searchCategories,
            searchCurrencies = viewModel::searchCurrencies,
            searchUnits = viewModel::searchUnits,
            topLevelNavigate = topLevelNavigate
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
    topLevelNavigate: (Routes) -> Unit = {}
) {
    val focusManager = LocalFocusManager.current

    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .verticalScroll(rememberScrollState())
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.SpaceBetween
        ) {
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = MaterialTheme.dimension.xl),
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

                AmountFormField(
                    state = state.amountFieldState,
                    onChange = {
                        sendEvent(EditProductScreenEvent.OnAmountChange(it))
                    },
                    searchUnits = searchUnits
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

            Column(
                modifier = Modifier.fillMaxWidth(),
            ) {
                ButtonWithLoader(
                    onClick = {
                        sendEvent(
                            EditProductScreenEvent.OnDeleteClick(
                                onSuccess =
                                {
                                    topLevelNavigate(Routes.HomeGraph())
                                }
                            )
                        )
                    },
                    modifier = Modifier.fillMaxWidth(),
                    shape = MaterialTheme.shapes.extraSmall,
                    colors = ButtonDefaults.buttonColors().copy(
                        contentColor = MaterialTheme.colorScheme.onError,
                        containerColor = MaterialTheme.colorScheme.error
                    ),
                    loading = state.deleteSubmitting
                ) {
                    Text(text = stringResource(id = R.string.delete))
                }
                Row(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
                ) {
                    OutlinedButton(
                        onClick = {
                            topLevelNavigate(Routes.ProductDetails(state.productId!!))
                        },
                        modifier = Modifier.weight(0.5f),
                        shape = MaterialTheme.shapes.extraSmall,
                        colors = ButtonDefaults.outlinedButtonColors().copy(
                            contentColor = MaterialTheme.colorScheme.error,
                        )
                    )
                    {
                        Text(text = stringResource(id = R.string.cancel))
                    }

                    ButtonWithLoader(
                        loading = state.formSubmitting,
                        onClick = {
                            focusManager.clearFocus()
                            sendEvent(EditProductScreenEvent.OnSubmitClick(
                                onSuccess = {
                                    topLevelNavigate(Routes.ProductDetails(state.productId!!))
                                }
                            ))
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
}

@Composable
@BiteRightPreview
fun EditProductScreenPreview() {
    EditProductScreenContent(
        state = EditProductScreenState(),
    )
}