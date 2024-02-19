package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Card
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateListOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.window.Dialog
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import kotlinx.coroutines.FlowPreview
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.coroutines.time.debounce
import java.time.Duration
import java.util.UUID

@OptIn(FlowPreview::class)
@Composable
fun CategorySearchDialog(
    selectedCategory: CategoryDto,
    modifier: Modifier = Modifier,
    searchCategories: suspend (String, PaginationParams) -> PaginatedList<CategoryDto>,
    onCategorySelected: (CategoryDto) -> Unit,
    onDismissRequest: () -> Unit,
    debounceDuration: Duration = Duration.ofMillis(300),
    inPreview: Boolean = false,
    imageRequestBuilder: ImageRequest.Builder? = null,
) {
    val coroutineScope = rememberCoroutineScope()

    val categories = remember {
        mutableStateListOf<CategoryDto>()
    }

    val queryFieldStateFlow = remember {
        MutableStateFlow(
            TextFormFieldState()
        )
    }

    val queryFieldState = queryFieldStateFlow.collectAsState()

    var loading by remember {
        mutableStateOf(true)
    }

    var paginationParams by remember {
        mutableStateOf(
            PaginationParams(
                pageNumber = 0,
                pageSize = 10
            )
        )
    }

    var hasMore by remember {
        mutableStateOf(true)
    }

    fun fetchNewQueryCategories() {
        loading = true
        coroutineScope.launch {
            paginationParams = paginationParams.copy(pageNumber = 0)
            val searchedCategories = searchCategories(queryFieldState.value.value, paginationParams)
            hasMore = searchedCategories.hasMore()
            categories.clear()
            categories.addAll(searchedCategories.items)
            loading = false
        }
    }

    fun loadMore() {
        if (loading || !hasMore) return
        coroutineScope.launch {
            paginationParams = paginationParams.copy(pageNumber = paginationParams.pageNumber + 1)
            val searchedCategories = searchCategories(queryFieldState.value.value, paginationParams)
            hasMore = searchedCategories.hasMore()
            categories.addAll(searchedCategories.items)
        }
    }

    LaunchedEffect(Unit) {
        queryFieldStateFlow
            .debounce(debounceDuration)
            .collect {
                fetchNewQueryCategories()
            }
    }

    Dialog(
        onDismissRequest = onDismissRequest
    ) {
        Card(
            modifier = modifier
        ) {
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(MaterialTheme.dimension.sm)
            ) {
                TextField(
                    value = queryFieldState.value.value,
                    onValueChange = { value ->
                        queryFieldStateFlow.update {
                            it.copy(value = value)
                        }
                    },
                    label = {
                        Text(
                            text = stringResource(id = R.string.search),
                        )
                    },
                    trailingIcon = {
                        if (loading) {
                            CircularProgressIndicator()
                        }
                    },
                    modifier = Modifier.fillMaxWidth()
                )

                LazyColumn(
                    modifier = Modifier.fillMaxWidth(),
                    content = {
                        items(categories, key = { it.id }) { category ->
                            CategoryItem(
                                category = category,
                                selected = category == selectedCategory,
                                onClick = {
                                    onCategorySelected(category)
                                    onDismissRequest()
                                },
                                inPreview = inPreview,
                                imageRequestBuilder = imageRequestBuilder
                            )

                            if (category != categories.last()) {
                                HorizontalDivider(
                                    modifier = Modifier.fillMaxWidth()
                                )
                            }
                        }

                        item {
                            if (!loading && hasMore) {
                                Row(
                                    modifier = Modifier.fillMaxWidth(),
                                    horizontalArrangement = Arrangement.Center
                                ) {
                                    CircularProgressIndicator()
                                }
                                LaunchedEffect(Unit) {
                                    loadMore()
                                }
                            }
                        }
                    }
                )
            }
        }
    }
}

@Composable
@BiteRightPreview
fun CategorySearchDialogPreview() {

    val categories = listOf(
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Fruits",
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Vegetables",
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Meat",
        ),
    )
    BiteRightTheme {
        CategorySearchDialog(
            selectedCategory = categories.first(),
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
            onCategorySelected = {},
            onDismissRequest = {},
            inPreview = true
        )
    }
}