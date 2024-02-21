package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
import androidx.compose.animation.AnimatedVisibility
import androidx.compose.animation.core.tween
import androidx.compose.animation.fadeOut
import androidx.compose.animation.shrinkVertically
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
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
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.components.categories.CategoryImage
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.between
import com.sobczal2.biteright.util.humanize
import com.sobczal2.biteright.util.truncateString
import java.time.LocalDate
import kotlin.math.roundToInt
import kotlin.time.Duration
import kotlin.time.Duration.Companion.milliseconds
import kotlin.time.DurationUnit

data class ProductSummaryItemState(
    val name: String,
    val expirationDate: LocalDate,
    val categoryImageUri: String?,
    val amountPercentage: Double,
    val disposed: Boolean = false
)

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ProductItem(
    productSummaryItemState: ProductSummaryItemState,
    modifier: Modifier = Modifier,
    onClick: () -> Unit = {},
    inPreview: Boolean = false,
    animationDuration: Duration = 300.milliseconds,
    onRestored: () -> /* should disappear */ Boolean = { true },
    onDisposed: () -> /* should disappear */ Boolean = { false },
    imageRequestBuilder: ImageRequest.Builder? = null
) {
    var visible by remember { mutableStateOf(true) }

    val swipeState = rememberSwipeToDismissBoxState(
        initialValue = SwipeToDismissBoxValue.Settled,
        confirmValueChange = {
            when (it) {
                SwipeToDismissBoxValue.EndToStart -> {
                    val shouldDisappear = onDisposed()
                    if (shouldDisappear) {
                        visible = false
                    }
                    true
                }

                SwipeToDismissBoxValue.StartToEnd -> {
                    val shouldDisappear = onRestored()
                    if (shouldDisappear) {
                        visible = false
                    }
                    true
                }

                else -> false
            }
        }
    )

    AnimatedVisibility(
        visible = visible,
        exit = shrinkVertically(
            animationSpec = tween(durationMillis = animationDuration.toInt(DurationUnit.MILLISECONDS)),
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
                Row(
                    modifier = Modifier
                        .fillMaxWidth(),
                    horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.sm),
                    verticalAlignment = Alignment.CenterVertically
                ) {
                    CategoryImage(
                        imageUri = productSummaryItemState.categoryImageUri,
                        imageRequestBuilder = imageRequestBuilder,
                        inPreview = inPreview
                    )
                    ProductDetails(
                        productSummaryItemState,
                        modifier = Modifier
                            .weight(1f)
                    )
                    ProductConsumptionIndicator(
                        amountPercentage = productSummaryItemState.amountPercentage,
                        disposed = productSummaryItemState.disposed
                    )
                }
            }
        }
    }
}

@Composable
fun ProductConsumptionIndicator(
    amountPercentage: Double,
    disposed: Boolean
) {
    if (disposed) {
        Box(
            modifier = Modifier
                .padding(MaterialTheme.dimension.sm)
                .size(MaterialTheme.dimension.xxl),
            contentAlignment = Alignment.Center
        ) {
            Icon(
                imageVector = Icons.Default.Delete,
                contentDescription = stringResource(id = R.string.deleted),
                modifier = Modifier.size(MaterialTheme.dimension.xl),
                tint = MaterialTheme.colorScheme.error
            )
        }
    } else {
        Box(
            modifier = Modifier
                .padding(8.dp)
                .width(MaterialTheme.dimension.xxl),
        ) {
            CircularProgressIndicator(
                progress = { amountPercentage.toFloat() / 100 },
                modifier = Modifier.size(MaterialTheme.dimension.xxl),
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
}

private fun getColorForAmountPercentage(amountPercentage: Double): Color {
    return when (amountPercentage) {
        in 0.0..25.0 -> Color.Red
        in 25.0..50.0 -> Color(0xFFFFA500)
        in 50.0..75.0 -> Color.Yellow
        in 75.0..100.0 -> Color.Green
        else -> Color.Black
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
    if (expirationDate.isEqual(LocalDate.now())) {
        Text(
            text = stringResource(id = R.string.expires_today),
            style = MaterialTheme.typography.labelMedium.copy(
                fontWeight = FontWeight.Bold
            )
        )
    } else if (expirationDate.isBefore(LocalDate.now())) {
        Text(
            text = "${stringResource(id = R.string.expired_for)} ${
                between(
                    expirationDate,
                    LocalDate.now()
                ).humanize()
            }",
            style = MaterialTheme.typography.labelMedium,
            color = MaterialTheme.colorScheme.error
        )
    } else {
        Text(
            text = "${stringResource(id = R.string.expires_in)} ${
                between(
                    LocalDate.now(),
                    expirationDate
                ).humanize()
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
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
fun ProductSummaryItemPreview() {
    BiteRightTheme {
        ProductItem(
            productSummaryItemState = ProductSummaryItemState(
                name = "Product name Product name Product name",
                expirationDate = LocalDate.now().plusDays(5),
                categoryImageUri = null,
                amountPercentage = 50.0,
                disposed = false
            ),
            inPreview = true
        )
    }
}