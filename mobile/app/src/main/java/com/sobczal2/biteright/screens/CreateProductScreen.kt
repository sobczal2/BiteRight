package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.text.KeyboardActions
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.SnackbarHostState
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusDirection
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardCapitalization
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
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.categories.CategoryFormField
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.ErrorSnackbarHost
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormField
import com.sobczal2.biteright.ui.components.products.MaxAmountFormField
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.CreateProductViewModel

@Composable
fun CreateProductScreen(
    viewModel: CreateProductViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit
) {
    val state = viewModel.state.collectAsStateWithLifecycle()
    val snackbarHostState = remember { SnackbarHostState() }
    viewModel.snackbarHostState = snackbarHostState

    SurfaceLoader(
        loading = state.value.isLoading()
    ) {
        CreateProductScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            searchCategories = viewModel::searchCategories,
            searchCurrencies = viewModel::searchCurrencies,
            searchUnits = viewModel::searchUnits,
            topLevelNavigate = topLevelNavigate,
            snackbarHostState = snackbarHostState
        )
    }
}

@Composable
fun CreateProductScreenContent(
    state: CreateProductScreenState = CreateProductScreenState(),
    sendEvent: (CreateProductScreenEvent) -> Unit = {},
    searchCategories: suspend (String, PaginationParams) -> PaginatedList<CategoryDto> = { _, _ -> emptyPaginatedList() },
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto> = { _, _ -> emptyPaginatedList() },
    searchUnits: suspend (String, PaginationParams) -> PaginatedList<UnitDto> = { _, _ -> emptyPaginatedList() },
    topLevelNavigate: (Routes) -> Unit = {},
    snackbarHostState: SnackbarHostState,
) {
    val focusManager = LocalFocusManager.current

    Scaffold(
        snackbarHost = {
            ErrorSnackbarHost(
                snackbarHostState = snackbarHostState,
            )
        }
    ) { paddingValues ->
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
                verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md),
            ) {
                Text(
                    text = stringResource(id = R.string.create_product),
                    style = MaterialTheme.typography.displaySmall.copy(
                        textAlign = TextAlign.Center
                    ),
                    modifier = Modifier.fillMaxWidth()
                )
                TextFormField(
                    modifier = Modifier.fillMaxWidth(),
                    state = state.nameFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnNameChange(it))
                    },
                    options = TextFormFieldOptions(
                        label = { Text(text = stringResource(id = R.string.name)) },
                        keyboardOptions = KeyboardOptions.Default.copy(
                            capitalization = KeyboardCapitalization.Words,
                            imeAction = ImeAction.Next,
                        ),
                    ),
                )

                CategoryFormField(
                    state = state.categoryFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnCategoryChange(it))
                    },
                    searchCategories = searchCategories,
                    imageRequestBuilder = state.imageRequestBuilder,
                    modifier = Modifier
                )

                ExpirationDateFormField(
                    modifier = Modifier,
                    state = state.expirationDateFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnExpirationDateChange(it))
                    },
                )

                MaxAmountFormField(
                    state = state.maxAmountFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnAmountChange(it))
                    },
                    searchUnits = searchUnits,
                )

                PriceFormField(
                    state = state.priceFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnPriceChange(it))
                    },
                    searchCurrencies = searchCurrencies
                )

                TextFormField(
                    modifier = Modifier
                        .fillMaxWidth(),
                    state = state.descriptionFieldState,
                    onChange = {
                        sendEvent(CreateProductScreenEvent.OnDescriptionChange(it))
                    },
                    options = TextFormFieldOptions(
                        label = { Text(text = stringResource(id = R.string.description)) },
                        singleLine = false,
                        minLines = 3,
                        maxLines = 5,
                        keyboardOptions = KeyboardOptions.Default.copy(
                            capitalization = KeyboardCapitalization.Sentences,
                            imeAction = ImeAction.Done,
                        ),
                        keyboardActions = KeyboardActions(
                            onDone = {
                                focusManager.clearFocus()
                            }
                        )
                    )
                )
            }
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
                OutlinedButton(
                    onClick = {
                        topLevelNavigate(Routes.HomeGraph())
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                    colors = ButtonDefaults.outlinedButtonColors().copy(
                        contentColor = MaterialTheme.colorScheme.error,
                    )
                ) {
                    Text(text = stringResource(id = R.string.cancel))
                }

                ButtonWithLoader(
                    onClick = {
                        focusManager.clearFocus()
                        sendEvent(
                            CreateProductScreenEvent.OnSubmitClick(
                                onSuccess = {
                                    topLevelNavigate(Routes.HomeGraph())
                                }
                            )
                        )
                    },
                    loading = state.formSubmitting,
                    modifier = Modifier.weight(0.5f),
                ) {
                    Text(text = stringResource(id = R.string.save))
                }
            }
        }
    }
}


@Composable
@BiteRightPreview
fun CreateProductScreenPreview() {
    BiteRightTheme {
        CreateProductScreenContent(snackbarHostState = SnackbarHostState())
    }
}