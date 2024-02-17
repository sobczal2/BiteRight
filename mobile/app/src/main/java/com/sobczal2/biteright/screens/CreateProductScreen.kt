package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.ScreenLoader
import com.sobczal2.biteright.ui.components.common.forms.FormFieldEvents.OnValueChange
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.components.products.PriceFormFieldOptions
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
    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(MaterialTheme.dimension.xl),
            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
        ) {
            TextFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.nameFieldState,
                onEvent = { event ->
                    when (event) {
                        is OnValueChange -> sendEvent(CreateProductScreenEvent.OnNameChange(event.value))
                    }
                },
                options = TextFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.name)) },
                )
            )
            TextFormField(
                modifier = Modifier.fillMaxWidth(),
                state = state.descriptionFieldState,
                onEvent = { event ->
                    when (event) {
                        is OnValueChange -> sendEvent(
                            CreateProductScreenEvent.OnDescriptionChange(
                                event.value
                            )
                        )
                    }
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
                onEvent ={},
                options = PriceFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.price)) },
                ),
            )
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