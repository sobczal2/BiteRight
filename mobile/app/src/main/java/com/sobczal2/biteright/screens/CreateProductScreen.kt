package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.ScreenLoader
import com.sobczal2.biteright.ui.components.amounts.AmountFormField
import com.sobczal2.biteright.ui.components.categories.CategoryFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormField
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.CreateProductViewModel

@Composable
fun CreateProductScreen(
    viewModel: CreateProductViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    ScreenLoader(loading = state.value.globalLoading) {
        CreateProductScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent
        )
    }
}

@Composable
fun CreateProductScreenContent(
    state: CreateProductScreenState = CreateProductScreenState(),
    sendEvent: (CreateProductScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val focusManager = LocalFocusManager.current

    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.xxl)
                .verticalScroll(rememberScrollState()),
            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md),
        ) {
            TextFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.nameFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnNameChange(it))
                },
                options = TextFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.name)) },
                )
            )
            TextFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.descriptionFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnDescriptionChange(it))
                },
                options = TextFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.description)) },
                    singleLine = false,
                    minLines = 3,
                    maxLines = 5,
                )
            )
            PriceFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.priceFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnPriceChange(it))
                },
            )
            ExpirationDateFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.expirationDateFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnExpirationDateChange(it))
                },
            )

            CategoryFormField(
                state = state.categoryFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnCategoryChange(it))
                },
            )

            AmountFormField(
                state = state.amountFormFieldState,
                onChange = {
                    sendEvent(CreateProductScreenEvent.OnAmountChange(it))
                }
            );

            ButtonWithLoader(
                onClick = {
                    focusManager.clearFocus()
                    sendEvent(
                        CreateProductScreenEvent.OnSubmitClick(
                            onSuccess = {
                                handleNavigationEvent(NavigationEvent.NavigateToCurrentProducts)
                            }
                        )
                    )
                },
                loading = state.formSubmitting
            ) {
                Text(text = stringResource(id = R.string.create_product))
            }
        }
    }
}


@Composable
@Preview(apiLevel = 33)
@Preview(apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
fun CreateProductScreenPreview() {
    BiteRightTheme {
        CreateProductScreenContent()
    }
}