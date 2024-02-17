//package com.sobczal2.biteright.ui.components.products
//
//import androidx.compose.foundation.layout.Row
//import androidx.compose.foundation.layout.height
//import androidx.compose.foundation.shape.CornerSize
//import androidx.compose.material3.Button
//import androidx.compose.material3.MaterialTheme
//import androidx.compose.material3.Text
//import androidx.compose.material3.TextFieldDefaults
//import androidx.compose.runtime.Composable
//import androidx.compose.runtime.getValue
//import androidx.compose.runtime.mutableStateOf
//import androidx.compose.runtime.remember
//import androidx.compose.runtime.setValue
//import androidx.compose.ui.Modifier
//import androidx.compose.ui.res.stringResource
//import androidx.compose.ui.unit.dp
//import com.sobczal2.biteright.R
//import com.sobczal2.biteright.dto.currencies.CurrencyDto
//import com.sobczal2.biteright.ui.components.common.ValidatedNumberField
//import com.sobczal2.biteright.ui.components.currencies.CurrenciesDialog
//import com.sobczal2.biteright.util.ResourceIdOrString
//
//data class PriceFieldActions(
//    val onPriceChange: (Double?) -> Unit,
//    val onCurrencySelected: (CurrencyDto?) -> Unit,
//)
//
//@Composable
//fun PriceField(
//    error: ResourceIdOrString?,
//    actions: PriceFieldActions,
//    availableCurrencies: List<CurrencyDto>,
//) {
//    var currencyDialogOpen by remember {
//        mutableStateOf(false)
//    }
//
//    var currencyDto by remember {
//        mutableStateOf<CurrencyDto?>(null)
//    }
//
//    Row {
//        ValidatedNumberField(
//            onValueChange = actions.onPriceChange,
//            error = error,
//            label = { Text(text = stringResource(id = R.string.price)) },
//            modifier = Modifier
//                .weight(1f),
//            shape = MaterialTheme.shapes.extraSmall.copy(
//                topEnd = CornerSize(0.dp),
//                bottomEnd = CornerSize(0.dp),
//                bottomStart = CornerSize(0.dp),
//            )
//        )
//        Button(
//            onClick = { currencyDialogOpen = true },
//            shape = MaterialTheme.shapes.extraSmall.copy(
//                topStart = CornerSize(0.dp),
//                bottomStart = CornerSize(0.dp)
//            ),
//            modifier = Modifier
//                .height(TextFieldDefaults.MinHeight)
//        ) {
//            Text(
//                text = currencyDto?.symbol
//                    ?: stringResource(id = R.string.select_currency)
//            )
//
//            if (currencyDialogOpen) {
//                CurrenciesDialog(
//                    availableCurrencies = availableCurrencies,
//                    selectedCurrency = currencyDto,
//                    onSelectionChange = {
//                        actions.onCurrencySelected(it)
//                        currencyDialogOpen = false
//                        currencyDto = it
//                    },
//                    onDismissRequest = { currencyDialogOpen = false }
//                )
//            }
//        }
//    }
//}