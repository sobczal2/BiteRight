package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.animation.core.tween
import androidx.compose.animation.fadeOut
import androidx.compose.animation.shrinkVertically
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Delete
import androidx.compose.material.icons.filled.Image
import androidx.compose.material.icons.filled.Restore
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.SwipeToDismissBox
import androidx.compose.material3.SwipeToDismissBoxState
import androidx.compose.material3.SwipeToDismissBoxValue
import androidx.compose.material3.Text
import androidx.compose.material3.rememberSwipeToDismissBoxState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import coil.compose.AsyncImage
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.humanizePeriod
import com.sobczal2.biteright.util.truncateString
import kotlinx.coroutines.delay
import java.time.LocalDate
import java.time.Duration
import java.time.Period
import kotlin.math.roundToInt

data class ProductSummaryItemState(
    val name: String,
    val expirationDate: LocalDate,
    val categoryImageUri: String?,
    val amountPercentage: Double,
    val disposed: Boolean = false
)

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductSummaryItem(
    productSummaryItemState: ProductSummaryItemState,
    modifier: Modifier = Modifier,
    onClick: () -> Unit = {},
    inPreview: Boolean = false,
    animationDuration: Duration = Duration.ofMillis(300),
    onRestored: () -> Unit = {},
    onDeleted: () -> Unit = {}
) {
    var isDeleted by remember { mutableStateOf(false) }
    var isRestored by remember { mutableStateOf(false) }
    val swipeState = rememberSwipeToDismissBoxState(
        initialValue = SwipeToDismissBoxValue.Settled,
        confirmValueChange = {
            when (it) {
                SwipeToDismissBoxValue.EndToStart -> {
                    isDeleted = true; true
                }

                SwipeToDismissBoxValue.StartToEnd -> {
                    isRestored = true; true
                }

                else -> false
            }
        }
    )

    LaunchedEffect(isRestored) {
        if (isRestored) {
            swipeState.reset()
            onRestored()
            isRestored = false
        }
    }

    LaunchedEffect(isDeleted) {
        if (isDeleted) {
            delay(animationDuration.toMillis())
            onDeleted()
        }
    }

    AnimatedVisibility(
        visible = !isDeleted,
        exit = shrinkVertically(
            animationSpec = tween(durationMillis = animationDuration.toMillis().toInt()),
            shrinkTowards = Alignment.Top
        ) + fadeOut()
    ) {
        SwipeToDismissBox(
            state = swipeState,
            backgroundContent = { SwipeToDismissBackground(swipeState) },
            enableDismissFromStartToEnd = productSummaryItemState.disposed,
            enableDismissFromEndToStart = !productSummaryItemState.disposed,
            modifier = modifier
                .fillMaxWidth()
        ) {
            Surface(
                modifier = Modifier
                    .clickable(onClick = onClick)
                    .fillMaxWidth()
            ) {
                Row {
                    ProductImage(
                        imageUri = productSummaryItemState.categoryImageUri,
                        inPreview = inPreview
                    )
                    ProductDetails(
                        productSummaryItemState,
                        modifier = Modifier
                            .weight(1f)
                    )
                    ProductConsumptionIndicator(amountPercentage = productSummaryItemState.amountPercentage)
                }
            }
        }
    }
}

@Composable
fun ProductConsumptionIndicator(
    amountPercentage: Double
) {
    Box(
        modifier = Modifier
            .padding(8.dp)
            .width(48.dp)
    ) {
        CircularProgressIndicator(
            progress = { amountPercentage.toFloat() / 100 },
            modifier = Modifier.size(48.dp),
            color = getColorForAmountPercentage(amountPercentage)
        )
        Text(
            text = "${amountPercentage.roundToInt()}%",
            modifier = Modifier.align(Alignment.Center),
            style = MaterialTheme.typography.labelMedium
                .copy(textAlign = TextAlign.Center)
        )
    }
}

private fun getColorForAmountPercentage(amountPercentage: Double): Color {
    return when (amountPercentage) {
        in 0.0..25.0 -> Color.Green
        in 25.0..50.0 -> Color.Yellow
        in 50.0..75.0 -> Color(0xFFFFA500)
        in 75.0..100.0 -> Color.Red
        else -> Color.Black
    }
}

@Composable
fun ProductImage(imageUri: String?, inPreview: Boolean) {
    if (inPreview) {
        Box(modifier = Modifier.size(64.dp), contentAlignment = Alignment.Center) {
            Icon(
                Icons.Default.Image,
                contentDescription = "Preview Image",
                modifier = Modifier.size(50.dp)
            )
        }
    } else {
        AsyncImage(
            model = imageUri,
            contentDescription = "Product Image",
            modifier = Modifier.size(64.dp)
        )
    }
}

@Composable
fun ProductDetails(
    productSummaryItemState: ProductSummaryItemState,
    modifier: Modifier = Modifier
) {
    Box(
        modifier = modifier
            .padding(MaterialTheme.dimension.sm)
    ) {
        Column {
            Text(
                text = truncateString(productSummaryItemState.name, 24),
                style = MaterialTheme.typography.bodyLarge,
                maxLines = 1
            )
            ProductExpirationIndicator(expirationDate = productSummaryItemState.expirationDate)
        }
    }
}

@Composable
fun ProductExpirationIndicator(
    expirationDate: LocalDate
) {

    if (expirationDate.isBefore(LocalDate.now())) {
        Text(
            text = "${stringResource(id = R.string.expired_for)} ${
                humanizePeriod(
                    period = Period.between(
                        expirationDate,
                        LocalDate.now()
                    )
                )
            }",
            style = MaterialTheme.typography.labelMedium,
            color = MaterialTheme.colorScheme.error
        )
    } else {
        Text(
            text = "${stringResource(id = R.string.expires_in)} ${
                humanizePeriod(
                    period = Period.between(
                        LocalDate.now(),
                        expirationDate
                    )
                )
            }",
            style = MaterialTheme.typography.labelMedium,
        )

    }
}

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun SwipeToDismissBackground(state: SwipeToDismissBoxState) {
    Box(
        modifier = Modifier
            .fillMaxSize()
            .background(
                when (state.targetValue) {
                    SwipeToDismissBoxValue.StartToEnd -> Color.Green
                    SwipeToDismissBoxValue.EndToStart -> Color.Red
                    else -> Color.Transparent
                }
            )
            .padding(16.dp),
        contentAlignment = when (state.targetValue) {
            SwipeToDismissBoxValue.StartToEnd -> Alignment.CenterStart
            SwipeToDismissBoxValue.EndToStart -> Alignment.CenterEnd
            else -> Alignment.Center
        }
    ) {
        Icon(
            imageVector = when (state.targetValue) {
                SwipeToDismissBoxValue.StartToEnd -> Icons.Default.Restore
                SwipeToDismissBoxValue.EndToStart -> Icons.Default.Delete
                else -> return@Box
            },
            contentDescription = "Restore or Delete",
            modifier = Modifier.size(48.dp),
            tint = Color.White
        )
    }
}


@Composable
@Preview
@Preview("Dark Theme", uiMode = Configuration.UI_MODE_NIGHT_YES)
fun ProductSummaryItemPreview() {
    BiteRightTheme {
        ProductSummaryItem(
            productSummaryItemState = ProductSummaryItemState(
                name = "Product name Product name Product name",
                expirationDate = LocalDate.now().plusDays(5),
                categoryImageUri = null,
                amountPercentage = 50.0
            ),
            inPreview = true
        )
    }
}