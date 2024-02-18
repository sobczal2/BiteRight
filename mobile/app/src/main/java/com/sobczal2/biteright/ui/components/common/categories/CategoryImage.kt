package com.sobczal2.biteright.ui.components.common.categories

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Image
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import coil.compose.AsyncImage
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview

@Composable
fun CategoryImage(imageUri: String?, inPreview: Boolean) {
    if (inPreview) {
        Box(modifier = Modifier.size(64.dp), contentAlignment = Alignment.Center) {
            Icon(
                Icons.Default.Image,
                contentDescription = "Preview Image",
                modifier = Modifier.size(50.dp)
            )
        }
    } else {
        Box(
            modifier = Modifier
                .size(64.dp)
                .padding(MaterialTheme.dimension.sm)
                .background(MaterialTheme.colorScheme.inverseSurface, CircleShape)
        ) {
            AsyncImage(
                model = imageUri,
                contentDescription = "Product Image",
                modifier = Modifier.size(64.dp)
            )
        }
    }
}

@Composable
@BiteRightPreview
fun CategoryImagePreview() {
    BiteRightTheme {
        CategoryImage(imageUri = null, inPreview = true)
    }
}