package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.shape.CornerSize
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
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.extraSmallStart
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

@Composable
fun CategoryItem(
    category: CategoryDto,
    selected: Boolean,
    modifier: Modifier = Modifier,
    label: String? = null,
    labelColor: Color = LocalContentColor.current,
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    Row(
        modifier = modifier
            .fillMaxWidth(),
        horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.sm),
        verticalAlignment = Alignment.CenterVertically,
    ) {
        CategoryImage(
            categoryId = category.id,
            imageRequestBuilder = imageRequestBuilder,
            inPreview = inPreview,
            shape = MaterialTheme.shapes.extraSmallStart.copy(
                bottomStart = CornerSize(0.dp),
            ),
            modifier = Modifier.size(MaterialTheme.dimension.xxl)
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
                            color = labelColor
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
                        contentDescription = stringResource(id = R.string.selected_str),
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
                categoryId = category.id,
                inPreview = inPreview,
                imageRequestBuilder = imageRequestBuilder,
                modifier = Modifier.size(MaterialTheme.dimension.xl)
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
        label = "Category",
        inPreview = true
    )
}
