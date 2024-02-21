package com.sobczal2.biteright.ui.components.common.forms

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
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Surface
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
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.ui.theme.dimension
import kotlinx.coroutines.FlowPreview
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.debounce
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlin.time.Duration
import kotlin.time.Duration.Companion.milliseconds

@OptIn(FlowPreview::class)
@Composable
fun <T> SearchDialog(
    modifier: Modifier = Modifier,
    initialPaginationParams: PaginationParams = PaginationParams.Default,
    search: suspend (String, PaginationParams) -> PaginatedList<T>,
    inPreview: Boolean = false,
    debounceDuration: Duration = 300.milliseconds,
    keySelector: (T) -> Any,
    onDismissRequest: () -> Unit,
    selectedItem: T,
    listItem: @Composable (item: T, selected: Boolean) -> Unit,
) {
    val coroutineScope = rememberCoroutineScope()

    val items = remember {
        mutableStateListOf<T>()
    }

    val queryFieldStateFlow = remember {
        MutableStateFlow(
            TextFormFieldState()
        )
    }

    val queryFieldState = queryFieldStateFlow.collectAsState()

    var initialLoading by remember {
        mutableStateOf(true)
    }

    var loading by remember {
        mutableStateOf(false)
    }

    var paginationParams by remember {
        mutableStateOf(initialPaginationParams)
    }

    var hasMore by remember {
        mutableStateOf(false)
    }

    fun fetchItemsForNewQuery() {
        if (loading) return
        loading = true
        coroutineScope.launch {
            paginationParams = paginationParams.copy(pageNumber = 0)
            val searchedItems = search(queryFieldState.value.value, paginationParams)
            hasMore = searchedItems.hasMore()
            items.clear()
            items.addAll(searchedItems.items)
            initialLoading = false
            loading = false
        }
    }

    fun loadMore() {
        if (loading || !hasMore) return
        coroutineScope.launch {
            paginationParams = paginationParams.copy(pageNumber = paginationParams.pageNumber + 1)
            val searchedItems = search(queryFieldState.value.value, paginationParams)
            hasMore = searchedItems.hasMore()
            items.addAll(searchedItems.items)
        }
    }

    if (inPreview) {
        LaunchedEffect(Unit) {
            fetchItemsForNewQuery()
        }
    }

    LaunchedEffect(Unit) {
        queryFieldStateFlow
            .debounce(if (initialLoading) Duration.ZERO else debounceDuration)
            .collect {
                fetchItemsForNewQuery()
            }
    }

    Dialog(
        onDismissRequest = onDismissRequest
    ) {
        Surface(
            modifier = modifier,
            shape = MaterialTheme.shapes.medium,
        ) {
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(MaterialTheme.dimension.sm)
            ) {
                OutlinedTextField(
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
                        if (initialLoading || loading) {
                            CircularProgressIndicator()
                        }
                    },
                    modifier = Modifier.fillMaxWidth()
                )

                LazyColumn(
                    modifier = Modifier.fillMaxWidth(),
                    content = {
                        items(items, key = keySelector) { item ->
                            listItem(item, item == selectedItem)

                            if (item != items.last()) {
                                HorizontalDivider(
                                    modifier = Modifier.fillMaxWidth(),
                                    color = MaterialTheme.colorScheme.onSurface
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

                        item {
                            if (!initialLoading && !loading && !hasMore && items.isEmpty()) {
                                Row(
                                    modifier = Modifier.fillMaxWidth(),
                                    horizontalArrangement = Arrangement.Center

                                ) {
                                    Text(
                                        text = stringResource(id = R.string.no_results),
                                        modifier = Modifier.padding(MaterialTheme.dimension.sm)
                                    )
                                }
                            }
                        }
                    }
                )
            }
        }
    }
}