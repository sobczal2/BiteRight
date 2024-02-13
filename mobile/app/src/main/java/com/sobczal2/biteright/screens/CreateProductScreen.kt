package com.sobczal2.biteright.screens

import android.content.res.Configuration
import android.util.Log
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Button
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.ValidatedNumberField
import com.sobczal2.biteright.ui.components.common.ValidatedTextField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.viewmodels.CreateProductViewModel

@Composable
fun CreateProductScreen(
    viewModel: CreateProductViewModel = hiltViewModel(),
    navigateToCurrentProducts: () -> Unit,
) {
    val state = viewModel.state.collectAsState()

    CreateProductScreenContent(
        state = state.value,
        onCreateProductClick = {
            Log.d("CreateProductScreen", "Create product button clicked with state: ${state.value}")
        },
        onNameChange = viewModel::onNameChange,
        onDescriptionChange = viewModel::onDescriptionChange,
        onPriceChange = viewModel::onPriceChange,
    )
}

@Composable
fun CreateProductScreenContent(
    state: CreateProductScreenState = CreateProductScreenState(),
    onCreateProductClick: () -> Unit = {},
    onNameChange: (String) -> Unit = {},
    onDescriptionChange: (String) -> Unit = {},
    onPriceChange: (Double?) -> Unit = {},
) {
    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column {
            ValidatedTextField(
                value = state.name,
                onValueChange = onNameChange,
                error = state.nameError,
                label = { Text(text = stringResource(id = R.string.name)) },
            )
            ValidatedTextField(
                value = state.description,
                onValueChange = onDescriptionChange,
                error = state.descriptionError,
                label = { Text(text = stringResource(id = R.string.description)) },
                singleLine = false,
            )
            ValidatedNumberField(
                onValueChange = onPriceChange,
                error = state.priceError,
                label = { Text(text = stringResource(id = R.string.price)) },
            )

            Button(onClick = onCreateProductClick) {
                Text(text = stringResource(id = R.string.create_product))
            }
        }
    }
}

@Composable
@Preview
@Preview(uiMode = Configuration.UI_MODE_NIGHT_YES)
fun CreateProductScreenPreview() {
    BiteRightTheme {
        CreateProductScreenContent()
    }
}