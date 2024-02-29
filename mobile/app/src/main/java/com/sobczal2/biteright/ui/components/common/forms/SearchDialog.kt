package com.sobczal2.biteright.ui.components.common.forms

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.HorizontalDivider
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.window.Dialog
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.util.PaginationSource
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
    debounceDuration: Duration = 300.milliseconds,
    keySelector: (T) -> Any,
    onDismissRequest: () -> Unit,
    selectedItem: T,
    listItem: @Composable (item: T, selected: Boolean) -> Unit,
) {
    val coroutineScope = rememberCoroutineScope()

    val paginationSource = remember {
        PaginationSource<T, String>(initialPaginationParams = initialPaginationParams)
    }

    val queryFieldStateFlow = remember {
        MutableStateFlow(
            TextFormFieldState()
        )
    }

    val queryFieldState = queryFieldStateFlow.collectAsStateWithLifecycle()

    var initialized by remember { mutableStateOf(false) }

    LaunchedEffect(Unit) {
        queryFieldStateFlow
            .debounce(if (initialized) debounceDuration else Duration.ZERO)
            .collect {
                initialized = true
                paginationSource.fetchInitialItems(queryFieldState.value.value, search)
            }
    }

    Dialog(
        onDismissRequest = onDismissRequest
    ) {
        Surface(
            modifier = modifier,
            shape = MaterialTheme.shapes.extraSmall,
        ) {
            Column(
                modifier = Modifier
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
                        if (paginationSource.initialFetching.value) {
                            CircularProgressIndicator()
                        }
                    },
                    modifier = Modifier.fillMaxWidth()
                )

                LazyColumn(
                    modifier = Modifier.fillMaxWidth(),
                    content = {
                        items(paginationSource.items, key = keySelector) { item ->
                            listItem(item, item == selectedItem)

                            if (item != paginationSource.items.last()) {
                                HorizontalDivider(
                                    modifier = Modifier.fillMaxWidth(),
                                    color = MaterialTheme.colorScheme.onSurface
                                )
                            }
                        }

                        item {
                            if (paginationSource.hasMore.value) {
                                Row(
                                    modifier = Modifier.fillMaxWidth(),
                                    horizontalArrangement = Arrangement.Center
                                ) {
                                    CircularProgressIndicator()
                                }
                                LaunchedEffect(Unit) {
                                    coroutineScope.launch {
                                        paginationSource.fetchMoreItems(queryFieldState.value.value, search)
                                    }
                                }
                            }
                        }

                        item {
                            if (
                                !paginationSource.isFetching()
                                && !paginationSource.hasMore.value
                                && paginationSource.items.isEmpty()
                            ) {
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