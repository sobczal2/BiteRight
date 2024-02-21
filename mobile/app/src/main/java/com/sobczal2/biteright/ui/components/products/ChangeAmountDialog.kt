package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Slider
import androidx.compose.material3.SliderState
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableDoubleStateOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.window.Dialog
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ChangeAmountDialog(
    onDismiss: () -> Unit,
    onConfirm: (Double) -> Unit,
    currentAmount: Double,
    maxAmount: Double,
    unitName: String,
    productName: String,
    modifier: Modifier = Modifier
) {
    val sliderState = remember {
        mutableStateOf(
            SliderState(
                value = currentAmount.toFloat(),
                valueRange = 0.0.toFloat()..maxAmount.toFloat(),
            )
        )
    }
    Dialog(
        onDismissRequest = onDismiss
    ) {
        Surface(
            modifier = modifier,
            shape = MaterialTheme.shapes.medium,
        ) {
            Column(
                modifier = Modifier
                    .padding(MaterialTheme.dimension.md)
            ) {
                Text(
                    text = "${stringResource(id = R.string.change_amount_for)} $productName",
                    style = MaterialTheme.typography.bodyMedium
                )
                Text(
                    text = "${sliderState.value.value} / $maxAmount $unitName",
                    style = MaterialTheme.typography.bodySmall,
                    modifier = Modifier
                )
                Slider(
                    state = sliderState.value,
                )
            }
        }
    }
}


@Composable
@BiteRightPreview
fun ChangeAmountDialogPreview() {
    ChangeAmountDialog(
        onDismiss = {},
        onConfirm = {},
        currentAmount = 50.0,
        maxAmount = 100.0,
        unitName = "g",
        productName = "Banana"
    )
}