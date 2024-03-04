package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Slider
import androidx.compose.material3.SliderDefaults
import androidx.compose.material3.SliderState
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Dialog
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.Locale

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
            shape = MaterialTheme.shapes.extraSmall,
        ) {
            Column(
                modifier = Modifier,
                horizontalAlignment = Alignment.CenterHorizontally
            ) {
                Text(
                    text = "${stringResource(id = R.string.change_amount_for)} $productName",
                    style = MaterialTheme.typography.headlineSmall.copy(
                        textAlign = TextAlign.Center
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(
                            top = MaterialTheme.dimension.md,
                            start = MaterialTheme.dimension.md,
                            end = MaterialTheme.dimension.md,
                        ),
                )
                val sliderInteractionSource = remember { MutableInteractionSource() }
                Slider(
                    state = sliderState.value,
                    interactionSource = sliderInteractionSource,
                    thumb = {
                        Column(
                            horizontalAlignment = Alignment.CenterHorizontally,
                            modifier = Modifier
                                .padding(top = MaterialTheme.dimension.sm)
                        ) {
                            SliderDefaults.Thumb(
                                interactionSource = sliderInteractionSource
                            )
                            Text(
                                text = "${"%.2f".format(Locale.US, sliderState.value.value)} $unitName",
                                style = MaterialTheme.typography.bodySmall.copy(
                                    textAlign = TextAlign.Center
                                ),
                                modifier = Modifier
                                    .width(100.dp)
                            )
                        }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(MaterialTheme.dimension.sm)
                )

                Row(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(
                            start = MaterialTheme.dimension.md,
                            end = MaterialTheme.dimension.md,
                            bottom = MaterialTheme.dimension.md,
                        ),
                    horizontalArrangement = Arrangement.SpaceBetween,
                ) {
                    OutlinedButton(
                        onClick = onDismiss,
                        shape = MaterialTheme.shapes.extraSmall,
                        colors = ButtonDefaults.outlinedButtonColors().copy(
                            contentColor = MaterialTheme.colorScheme.error,
                        )
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