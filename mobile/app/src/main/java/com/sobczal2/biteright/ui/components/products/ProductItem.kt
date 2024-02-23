package com.sobczal2.biteright.ui.components.products

import androidx.compose.animation.AnimatedVisibility
import androidx.compose.animation.core.tween
import androidx.compose.animation.shrinkVertically
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.size
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Delete
import androidx.compose.material.icons.filled.Restore
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.Icon
import androidx.compose.material3.ListItem
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.lerp
import androidx.compose.ui.res.stringResource
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.ui.components.categories.CategoryImage
import com.sobczal2.biteright.ui.components.common.SwipeableItem
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.util.toExpirationString
import java.time.LocalDate
import java.util.UUID

@Composable
fun ProductListItem(
    simpleProductDto: SimpleProductDto,
    modifier: Modifier = Modifier,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    ListItem(
        headlineContent = {
            Text(
                text = simpleProductDto.name
            )
        },
        supportingContent = {
            ExpirationText(
                simpleProductDto.expirationDateKind,
                simpleProductDto.expirationDate
            )
        },
        trailingContent = {
            if (simpleProductDto.disposed) {
                DisposedIndicator()
            } else {
                AmountIndicator(simpleProductDto.getAmountPercentage())
            }
        },
        leadingContent = {
            CategoryImage(
                categoryId = simpleProductDto.categoryId,
                inPreview = inPreview,
                imageRequestBuilder = imageRequestBuilder
            )
        },
        modifier = modifier
    )
}

@Composable
fun ExpirationText(expirationDateKindDto: ExpirationDateKindDto, expirationDate: LocalDate?) {
    when (expirationDateKindDto) {
        ExpirationDateKindDto.Unknown -> Text(text = stringResource(id = R.string.expiration_unknown))
        ExpirationDateKindDto.Infinite -> Text(text = stringResource(id = R.string.expiration_infinite))
        ExpirationDateKindDto.BestBefore -> Text(text = expirationDate!!.toExpirationString())
        ExpirationDateKindDto.UseBy -> Text(text = expirationDate!!.toExpirationString())
    }
}

@Composable
fun AmountIndicator(amountPercentage: Double) {
    Box(
        modifier = Modifier.size(MaterialTheme.dimension.xxl),
        contentAlignment = Alignment.Center
    ) {
        Text(
            text = "${amountPercentage.toInt()}%",
            style = MaterialTheme.typography.bodySmall
        )

        CircularProgressIndicator(
            progress = {
                (amountPercentage / 100).toFloat()
            },
            trackColor = MaterialTheme.colorScheme.onSurface.copy(alpha = 0.3f),
            color = lerp(
                MaterialTheme.colorScheme.error,
                MaterialTheme.colorScheme.primary,
                amountPercentage.toFloat() / 100
            )
        )
    }
}

@Composable
fun DisposedIndicator() {
    Box(
        modifier = Modifier.size(MaterialTheme.dimension.xxl),
        contentAlignment = Alignment.Center
    ) {
        Icon(
            imageVector = Icons.Default.Delete,
            contentDescription = null
        )
    }
}

@Composable
@BiteRightPreview
fun ProductListItemPreview() {
    ProductListItem(
        simpleProductDto = SimpleProductDto(
            id = UUID.randomUUID(),
            name = "Milk",
            expirationDateKind = ExpirationDateKindDto.BestBefore,
            expirationDate = LocalDate.now().plusDays(7),
            categoryId = UUID.randomUUID(),
            addedDateTime = LocalDate.now().atStartOfDay(),
            currentAmount = 0.1,
            maxAmount = 1.0,
            unitAbbreviation = "L",
            disposed = false
        ),
        inPreview = true
    )
}

@Composable
fun SwipeableProductListItem(
    simpleProductDto: SimpleProductDto,
    modifier: Modifier = Modifier,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
    onDispose: (animationDurationMillis: Int) -> Boolean = { false },
    onRestore: (animationDurationMillis: Int) -> Boolean = { false },
    visible: Boolean = true,
) {
    val animationDurationMillis = 300
    AnimatedVisibility(
        visible = visible,
        exit = shrinkVertically(
            animationSpec = tween(durationMillis = animationDurationMillis),
            shrinkTowards = Alignment.Top
        )
    ) {
        SwipeableItem(
            onSwipeLeft = {
                onDispose(animationDurationMillis)
            },
            onSwipeRight = {
                onRestore(animationDurationMillis)
            },
            swipeLeftBackground = {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .background(MaterialTheme.colorScheme.error),
                    contentAlignment = Alignment.CenterEnd
                ) {
                    Icon(imageVector = Icons.Default.Delete, contentDescription = null)
                }
            },
            swipeRightBackground = {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .background(MaterialTheme.colorScheme.primary),
                    contentAlignment = Alignment.CenterStart
                ) {
                    Icon(imageVector = Icons.Default.Restore, contentDescription = null)
                }
            },
            canSwipeLeft = !simpleProductDto.disposed,
            canSwipeRight = simpleProductDto.disposed,
        ) {
            ProductListItem(
                simpleProductDto = simpleProductDto,
                inPreview = inPreview,
                imageRequestBuilder = imageRequestBuilder,
                modifier = modifier
            )
        }
    }
}
