package com.sobczal2.biteright.screens

import android.content.res.Configuration
import android.util.Log
import androidx.compose.animation.animateContentSize
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.ValidatedNumberField
import com.sobczal2.biteright.ui.components.common.ValidatedTextField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
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
        onSelectCurrencyButtonClick = viewModel::onSelectCurrencyButtonClick
    )
}

@Composable
fun CreateProductScreenContent(
    state: CreateProductScreenState = CreateProductScreenState(),
    onCreateProductClick: () -> Unit = {},
    onNameChange: (String) -> Unit = {},
    onDescriptionChange: (String) -> Unit = {},
    onPriceChange: (Double?) -> Unit = {},
    onSelectCurrencyButtonClick: () -> Unit = {},
) {
    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(MaterialTheme.dimension.xl),

            ) {
            Text(
                text = stringResource(id = R.string.create_product),
                style = MaterialTheme.typography.displayMedium,
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = MaterialTheme.dimension.lg)
            )
            ValidatedTextField(
                onValueChange = onNameChange,
                error = state.nameError,
                label = { Text(text = stringResource(id = R.string.name)) },
                modifier = Modifier.fillMaxWidth()
            )
            ValidatedTextField(
                onValueChange = onDescriptionChange,
                error = state.descriptionError,
                label = { Text(text = stringResource(id = R.string.description)) },
                singleLine = false,
                modifier = Modifier.fillMaxWidth()
            )
            Row(
                modifier = Modifier
                    .fillMaxWidth()
            ) {
                ValidatedNumberField(
                    onValueChange = onPriceChange,
                    error = state.priceError,
                    label = { Text(text = stringResource(id = R.string.price)) },
                    modifier = Modifier
                        .weight(1f),
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topEnd = CornerSize(0.dp),
                        bottomEnd = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp),
                    )
                )
                Button(
                    onClick = onSelectCurrencyButtonClick,
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topStart = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp)
                    ),
                    modifier = Modifier
                        .height(TextFieldDefaults.MinHeight)
                ) {
                    Text(
                        text = state.currencyString ?: stringResource(id = R.string.select_currency)
                    )
                }
            }

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