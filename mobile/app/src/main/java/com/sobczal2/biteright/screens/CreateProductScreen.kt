package com.sobczal2.biteright.screens

import android.content.res.Configuration
import android.util.Log
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.ValidatedNumberField
import com.sobczal2.biteright.ui.components.common.ValidatedTextField
import com.sobczal2.biteright.ui.components.currencies.CurrenciesDialog
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.CreateProductViewModel

@Composable
fun CreateProductScreen(
    viewModel: CreateProductViewModel = hiltViewModel(),
    navigateToCurrentProducts: () -> Unit,
) {
    val state = viewModel.state.collectAsState()

    LaunchedEffect(Unit) {
        viewModel.init()
    }

    CreateProductScreenContent(
        state = state.value,
        onCreateProductClick = {
            Log.d("CreateProductScreen", "Create product button clicked with state: ${state.value}")
        },
        onNameChange = viewModel::onNameChange,
        onDescriptionChange = viewModel::onDescriptionChange,
        onPriceChange = viewModel::onPriceChange,
        onSelectCurrencyButtonClick = viewModel::onSelectCurrencyButtonClick,
        availableCurrencies = state.value.availableCurrencyDtos,
        onCurrencySelected = viewModel::onCurrencySelected,
        onCurrencyDialogDismissRequest = viewModel::closeCurrencyDialog
    )
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun CreateProductScreenContent(
    state: CreateProductScreenState = CreateProductScreenState(),
    onCreateProductClick: () -> Unit = {},
    onNameChange: (String) -> Unit = {},
    onDescriptionChange: (String) -> Unit = {},
    onPriceChange: (Double?) -> Unit = {},
    onSelectCurrencyButtonClick: () -> Unit = {},
    availableCurrencies: List<CurrencyDto> = emptyList(),
    onCurrencySelected: (CurrencyDto?) -> Unit = {},
    onCurrencyDialogDismissRequest: () -> Unit = {},
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
            Text(
                text = stringResource(id = R.string.create_product),
                style = MaterialTheme.typography.displaySmall.copy(
                    textAlign = TextAlign.Center
                ),
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
            Row {
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
                        text = state.currencyDto?.symbol
                            ?: stringResource(id = R.string.select_currency)
                    )

                    if (state.currencyDialogOpen) {
                        CurrenciesDialog(
                            availableCurrencies = availableCurrencies,
                            selectedCurrency = state.currencyDto,
                            onSelectionChange = onCurrencySelected,
                            onDismissRequest = onCurrencyDialogDismissRequest
                        )
                    }
                }
            }

            Button(
                onClick = {},
                modifier = Modifier
                    .fillMaxWidth()
                    .height(TextFieldDefaults.MinHeight),
                shape = MaterialTheme.shapes.extraSmall
            ) {
                Text(text = stringResource(id = R.string.select_expiration_date))
            }

            Button(
                onClick = {},
                modifier = Modifier
                    .fillMaxWidth()
                    .height(TextFieldDefaults.MinHeight),
                shape = MaterialTheme.shapes.extraSmall
            ) {
                Text(text = stringResource(id = R.string.select_category))
            }

            Row {
                ValidatedNumberField(
                    onValueChange = { },
                    error = null,
                    label = { Text(text = stringResource(id = R.string.amount)) },
                    modifier = Modifier
                        .weight(1f),
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topEnd = CornerSize(0.dp),
                        bottomEnd = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp),
                    )
                )
                Button(
                    onClick = {},
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topStart = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp)
                    ),
                    modifier = Modifier
                        .height(TextFieldDefaults.MinHeight)
                ) {
                    Text(
                        text = stringResource(id = R.string.select_unit)
                    )
                }
            }

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween
            ) {
                Button(
                    onClick = onCreateProductClick,
                ) {
                    Text(text = stringResource(id = R.string.create_product))
                }
                Button(
                    onClick = {},
                    colors = ButtonDefaults.textButtonColors(
                        containerColor = MaterialTheme.colorScheme.error,
                        contentColor = MaterialTheme.colorScheme.onError
                    ),
                ) {
                    Text(
                        text = stringResource(id = R.string.cancel),
                    )
                }
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