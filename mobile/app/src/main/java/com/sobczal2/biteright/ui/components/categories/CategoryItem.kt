package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.size
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material3.Icon
import androidx.compose.material3.ListItem
import androidx.compose.material3.LocalContentColor
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.categories.imageUri
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

@Composable
fun CategoryItem(
    category: CategoryDto,
    selected: Boolean,
    onClick: () -> Unit,
    modifier: Modifier = Modifier,
    label: String? = null,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    Row(
        modifier = modifier
            .fillMaxWidth()
            .clickable { onClick() },
        horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.sm),
        verticalAlignment = Alignment.CenterVertically,
    ) {
        CategoryImage(
            imageUri = category.imageUri(),
            imageRequestBuilder = imageRequestBuilder,
            inPreview = inPreview
        )
        Row(
            modifier = Modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween,
            verticalAlignment = Alignment.CenterVertically
        ) {

            Column {
                if (label != null) {
                    Text(
                        text = label,
                        style = MaterialTheme.typography.bodySmall.copy(
                            color = LocalContentColor.current
                        )
                    )
                }
                Text(
                    text = category.name,
                    style = MaterialTheme.typography.bodyLarge.copy(
                        color = MaterialTheme.colorScheme.onSurface
                    )
                )
            }
            if (selected) {
                Box(
                    modifier = Modifier.size(64.dp),
                    contentAlignment = Alignment.Center
                ) {
                    Icon(
                        Icons.Default.Done,
                        contentDescription = stringResource(id = R.string.selected),
                        modifier = Modifier.size(MaterialTheme.dimension.xxl),
                    )
                }
            }
        }
    }
}

@Composable
fun CategoryListItem(
    category: CategoryDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    ListItem(
        headlineContent = {
            Text(text = category.name)
        },
        leadingContent = {
            CategoryImage(
                imageUri = category.imageUri(),
                inPreview = inPreview,
                imageRequestBuilder = imageRequestBuilder
            )
        },
        trailingContent = {
            if (selected) {
                Icon(imageVector = Icons.Default.Done, contentDescription = "Selected")
            }
        },
        modifier = modifier
    )
}

@Composable
@BiteRightPreview
fun CategoryItemPreview() {
    CategoryItem(
        category = CategoryDto(
            id = UUID.randomUUID(),
            name = "Fruits",
        ),
        selected = false,
        onClick = {},
        label = "Category",
        inPreview = true
    )
}
