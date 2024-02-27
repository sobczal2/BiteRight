package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Image
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.unit.dp
import coil.compose.AsyncImage
import coil.request.ImageRequest
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.util.getCategoryPhotoUrl
import java.util.UUID

@Composable
fun CategoryImage(
    categoryId: UUID,
    modifier: Modifier = Modifier,
    shape: Shape = CircleShape,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    val imageUri = getCategoryPhotoUrl(categoryId = categoryId)
    if (inPreview) {
        Box(
            modifier = modifier
                .background(color = MaterialTheme.colorScheme.inverseSurface, shape = shape),
            contentAlignment = Alignment.Center,
        ) {
            Icon(
                Icons.Default.Image,
                contentDescription = "Preview Image",
                modifier = Modifier
                    .fillMaxSize()
            )
        }
    } else {
        Box(
            modifier = modifier
                .background(color = MaterialTheme.colorScheme.inverseSurface, shape = shape)
        ) {
            AsyncImage(
                model = imageRequestBuilder?.data(imageUri)?.build() ?: imageUri,
                contentDescription = "Product Image",
                modifier = Modifier
                    .fillMaxSize(),
            )
        }
    }
}

@Composable
@BiteRightPreview
fun CategoryImagePreview() {
    BiteRightTheme {
        CategoryImage(
            categoryId = UUID.randomUUID(),
            inPreview = true,
            modifier = Modifier
                .size(64.dp)
        )
    }
}