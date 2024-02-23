package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Card
import androidx.compose.material3.MaterialTheme
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.ui.components.common.forms.FormFieldState
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

data class CategoryFormFieldState(
    override val value: CategoryDto,
    override val error: String? = null,
    val inPreview: Boolean = false
) : FormFieldState<CategoryDto?>

@Composable
fun CategoryFormField(
    state: CategoryFormFieldState,
    onChange: (CategoryDto) -> Unit,
    searchCategories: suspend (String, PaginationParams) -> PaginatedList<CategoryDto>,
    modifier: Modifier = Modifier,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    var dialogOpen by remember { mutableStateOf(false) }

    Column(
        modifier = modifier
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmall,
        ) {
            CategoryItem(
                category = state.value,
                selected = false,
                label = stringResource(id = R.string.category),
                onClick = {
                    dialogOpen = true
                },
                imageRequestBuilder = imageRequestBuilder,
            )
        }

        if (dialogOpen) {
            SearchDialog(
                search = searchCategories,
                keySelector = { it.id },
                onDismissRequest = { dialogOpen = false },
                selectedItem = state.value,
                modifier = Modifier
                    .height(500.dp)
            ) { category, selected ->
                CategoryListItem(
                    category = category,
                    selected = selected,
                    modifier = Modifier
                        .clickable {
                            onChange(category)
                            dialogOpen = false
                        },
                    inPreview = state.inPreview,
                    imageRequestBuilder = imageRequestBuilder
                )
            }
        }
    }
}

@Composable
@BiteRightPreview
fun CategoryFormFieldPreview() {
    val categories = listOf(
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Fruits"
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Vegetables"
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Dairy"
        ),
    )
    val state = CategoryFormFieldState(
        value = categories.first(),
        inPreview = true
    )
    CategoryFormField(
        state = state,
        searchCategories = { query, paginationParams ->
            PaginatedList(
                pageNumber = paginationParams.pageNumber,
                pageSize = paginationParams.pageSize,
                totalCount = 3,
                totalPages = 1,
                items = categories.filter {
                    it.name.contains(query)
                }
            )
        },
        onChange = {}
    )
}