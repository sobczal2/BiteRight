//package com.sobczal2.biteright.screens
//
//import android.content.res.Configuration
//import android.util.Log
//import androidx.compose.foundation.layout.Arrangement
//import androidx.compose.foundation.layout.Column
//import androidx.compose.foundation.layout.Row
//import androidx.compose.foundation.layout.fillMaxSize
//import androidx.compose.foundation.layout.fillMaxWidth
//import androidx.compose.foundation.layout.height
//import androidx.compose.foundation.layout.padding
//import androidx.compose.foundation.shape.CornerSize
//import androidx.compose.material3.Button
//import androidx.compose.material3.ButtonDefaults
//import androidx.compose.material3.MaterialTheme
//import androidx.compose.material3.Surface
//import androidx.compose.material3.Text
//import androidx.compose.material3.TextFieldDefaults
//import androidx.compose.runtime.Composable
//import androidx.compose.runtime.LaunchedEffect
//import androidx.compose.runtime.collectAsState
//import androidx.compose.ui.Modifier
//import androidx.compose.ui.res.stringResource
//import androidx.compose.ui.text.style.TextAlign
//import androidx.compose.ui.tooling.preview.Preview
//import androidx.compose.ui.unit.dp
//import androidx.hilt.navigation.compose.hiltViewModel
//import com.sobczal2.biteright.R
//import com.sobczal2.biteright.dto.currencies.CurrencyDto
//import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
//import com.sobczal2.biteright.events.NavigationEvent
//import com.sobczal2.biteright.state.CreateProductScreenState
//import com.sobczal2.biteright.ui.components.common.ValidatedNumberField
//import com.sobczal2.biteright.ui.components.common.ValidatedTextField
//import com.sobczal2.biteright.ui.components.products.ExpirationDateDialog
//import com.sobczal2.biteright.ui.components.products.PriceField
//import com.sobczal2.biteright.ui.components.products.PriceFieldActions
//import com.sobczal2.biteright.ui.theme.BiteRightTheme
//import com.sobczal2.biteright.ui.theme.dimension
//import com.sobczal2.biteright.viewmodels.CreateProductViewModel
//import java.time.LocalDate
//
//@Composable
//fun CreateProductScreen(
//    viewModel: CreateProductViewModel = hiltViewModel(),
//    handleNavigationEvent: (NavigationEvent) -> Unit,
//) {
//    val state = viewModel.state.collectAsState()
//
//    LaunchedEffect(Unit) {
//        viewModel.init()
//    }
//
//    CreateProductScreenContent(
//        state = state.value,
//        onCreateProductClick = {
//            Log.d("CreateProductScreen", "Create product button clicked with state: ${state.value}")
//        },
//        onNameChange = viewModel::onNameChange,
//        onDescriptionChange = viewModel::onDescriptionChange,
//        onPriceChange = viewModel::onPriceChange,
//        availableCurrencies = state.value.availableCurrencyDtos,
//        onCurrencySelected = viewModel::onCurrencySelected,
//        onExpirationDateKindSelected = viewModel::onExpirationDateKindSelected,
//        onExpirationDateValueSelected = viewModel::onExpirationDateValueSelected,
//        onExpirationDateDialogOpen = viewModel::onExpirationDateDialogOpen,
//        onExpirationDateDialogClose = viewModel::onExpirationDateDialogClose
//    )
//}
//
//@Composable
//fun CreateProductScreenContent(
//    state: CreateProductScreenState = CreateProductScreenState(),
//    onCreateProductClick: () -> Unit = {},
//    onNameChange: (String) -> Unit = {},
//    onDescriptionChange: (String) -> Unit = {},
//    onPriceChange: (Double?) -> Unit = {},
//    availableCurrencies: List<CurrencyDto> = emptyList(),
//    onCurrencySelected: (CurrencyDto?) -> Unit = {},
//    onExpirationDateKindSelected: (ExpirationDateKindDto?) -> Unit = {},
//    onExpirationDateValueSelected: (LocalDate?) -> Unit = {},
//    onExpirationDateDialogOpen: () -> Unit = {},
//    onExpirationDateDialogClose: () -> Unit = {},
//) {
//    Surface(
//        modifier = Modifier
//            .fillMaxSize(),
//    ) {
//        Column(
//            modifier = Modifier
//                .fillMaxSize()
//                .padding(MaterialTheme.dimension.xl),
//            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
//        ) {
//            Text(
//                text = stringResource(id = R.string.create_product),
//                style = MaterialTheme.typography.displaySmall.copy(
//                    textAlign = TextAlign.Center
//                ),
//                modifier = Modifier
//                    .fillMaxWidth()
//                    .padding(bottom = MaterialTheme.dimension.lg)
//            )
//            ValidatedTextField(
//                onValueChange = onNameChange,
//                error = state.nameError,
//                label = { Text(text = stringResource(id = R.string.name)) },
//                modifier = Modifier.fillMaxWidth()
//            )
//            ValidatedTextField(
//                onValueChange = onDescriptionChange,
//                error = state.descriptionError,
//                label = { Text(text = stringResource(id = R.string.description)) },
//                singleLine = false,
//                modifier = Modifier.fillMaxWidth()
//            )
//            PriceField(
//                error = state.priceError,
//                actions = PriceFieldActions(
//                    onPriceChange = onPriceChange,
//                    onCurrencySelected = onCurrencySelected
//                ),
//                availableCurrencies = availableCurrencies,
//            )
//            Button(
//                onClick = onExpirationDateDialogOpen,
//                modifier = Modifier
//                    .fillMaxWidth()
//                    .height(TextFieldDefaults.MinHeight),
//                shape = MaterialTheme.shapes.extraSmall
//            ) {
//                Text(
//                    text = expirationDateButtonString(
//                        state.expirationDateKind,
//                        state.expirationDateValue
//                    )
//                )
//            }
//
//            if (state.expirationDateDialogOpen) {
//                ExpirationDateDialog(
//                    onSelected = { date, kind ->
//                        onExpirationDateKindSelected(kind)
//                        onExpirationDateValueSelected(date)
//                    },
//                    onDismissRequest = onExpirationDateDialogClose
//                )
//            }
//
//            Button(
//                onClick = {},
//                modifier = Modifier
//                    .fillMaxWidth()
//                    .height(TextFieldDefaults.MinHeight),
//                shape = MaterialTheme.shapes.extraSmall
//            ) {
//                Text(text = stringResource(id = R.string.select_category))
//            }
//
//            Row {
//                ValidatedNumberField(
//                    onValueChange = { },
//                    error = null,
//                    label = { Text(text = stringResource(id = R.string.amount)) },
//                    modifier = Modifier
//                        .weight(1f),
//                    shape = MaterialTheme.shapes.extraSmall.copy(
//                        topEnd = CornerSize(0.dp),
//                        bottomEnd = CornerSize(0.dp),
//                        bottomStart = CornerSize(0.dp),
//                    )
//                )
//                Button(
//                    onClick = {},
//                    shape = MaterialTheme.shapes.extraSmall.copy(
//                        topStart = CornerSize(0.dp),
//                        bottomStart = CornerSize(0.dp)
//                    ),
//                    modifier = Modifier
//                        .height(TextFieldDefaults.MinHeight)
//                ) {
//                    Text(
//                        text = stringResource(id = R.string.select_unit)
//                    )
//                }
//            }
//
//            Row(
//                modifier = Modifier.fillMaxWidth(),
//                horizontalArrangement = Arrangement.SpaceBetween
//            ) {
//                Button(
//                    onClick = onCreateProductClick,
//                ) {
//                    Text(text = stringResource(id = R.string.create_product))
//                }
//                Button(
//                    onClick = {},
//                    colors = ButtonDefaults.textButtonColors(
//                        containerColor = MaterialTheme.colorScheme.error,
//                        contentColor = MaterialTheme.colorScheme.onError
//                    ),
//                ) {
//                    Text(
//                        text = stringResource(id = R.string.cancel),
//                    )
//                }
//            }
//        }
//    }
//}
//
//@Composable
//private fun expirationDateButtonString(
//    expirationDateKind: ExpirationDateKindDto?,
//    expirationDateValue: LocalDate?,
//): String {
//    if (expirationDateKind == null) {
//        return stringResource(id = R.string.select_expiration_date)
//    } else if (expirationDateValue == null) {
//        return stringResource(
//            id = ExpirationDateKindDto.toLocalizedResourceID(
//                expirationDateKind
//            )
//        )
//    } else {
//        return "$expirationDateKind: $expirationDateValue"
//    }
//}
//
//@Composable
//@Preview(apiLevel = 33)
//@Preview(apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
//fun CreateProductScreenPreview() {
//    BiteRightTheme {
//        CreateProductScreenContent()
//    }
//}