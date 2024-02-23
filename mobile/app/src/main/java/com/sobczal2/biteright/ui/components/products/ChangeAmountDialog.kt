package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Slider
import androidx.compose.material3.SliderState
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.window.Dialog
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ChangeAmountDialog(
    onDismiss: () -> Unit,
    onConfirm: (Double) -> Unit,
    loading: Boolean,
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
                    text = "${"%.2f".format(sliderState.value.value)} / ${"%.2f".format(maxAmount)} $unitName",
                    style = MaterialTheme.typography.bodySmall,
                    modifier = Modifier
                )
                Slider(
                    state = sliderState.value,
                )

                Row(
                    modifier = Modifier
                        .fillMaxWidth(),
                    horizontalArrangement = Arrangement.SpaceBetween,
                ) {
                    OutlinedButton(
                        onClick = onDismiss,
                        shape = MaterialTheme.shapes.extraSmall
                    ) {
                        Text(text = stringResource(id = R.string.cancel))
                    }
                    ButtonWithLoader(
                        onClick = {
                            onConfirm(sliderState.value.value.toDouble())
                        },
                        loading = loading
                    ) {
                        Text(text = stringResource(id = R.string.confirm))

                    }
                }
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
        loading = false,
        productName = "Banana"
    )
}