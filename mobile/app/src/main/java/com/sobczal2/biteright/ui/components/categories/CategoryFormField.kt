package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.clickable
import androidx.compose.foundation.focusable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Card
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.focus.FocusDirection
import androidx.compose.ui.focus.FocusRequester
import androidx.compose.ui.focus.focusRequester
import androidx.compose.ui.focus.onFocusChanged
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.ui.components.common.forms.FormFieldState
import com.sobczal2.biteright.ui.components.common.forms.SearchDialog
import com.sobczal2.biteright.ui.theme.extraSmallTop
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
    val focusManager = LocalFocusManager.current
    var dialogOpen by remember { mutableStateOf(false) }
    val focusRequester = remember {
        FocusRequester()
    }
    var focused by remember { mutableStateOf(false) }
    val color =
        if (focused) TextFieldDefaults.colors().focusedLabelColor else TextFieldDefaults.colors().unfocusedLabelColor

    Column(
        modifier = modifier
            .clickable {
                focusRequester.requestFocus()
                dialogOpen = true
            }
            .focusRequester(focusRequester)
            .onFocusChanged {
                focused = it.isFocused
            }
            .focusable()
    ) {
        Card(
            shape = MaterialTheme.shapes.extraSmallTop,
        ) {
            CategoryItem(
                category = state.value,
                selected = false,
                label = stringResource(id = R.string.category),
                labelColor = color,
                imageRequestBuilder = imageRequestBuilder,
            )
        }
        HorizontalDivider(
            thickness = if (focused) 2.dp else 1.dp,
            color = color
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
                        focusManager.moveFocus(FocusDirection.Next)
                        onChange(category)
                        dialogOpen = false
                    },
                inPreview = state.inPreview,
                imageRequestBuilder = imageRequestBuilder
            )
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