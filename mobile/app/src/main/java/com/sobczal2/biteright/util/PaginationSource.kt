package com.sobczal2.biteright.util

import androidx.compose.runtime.State
import androidx.compose.runtime.mutableStateListOf
import androidx.compose.runtime.mutableStateOf
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams


class PaginationSource<TItem, TQuery>(
    private val initialPaginationParams: PaginationParams,
) {
    private var _items = mutableStateListOf<TItem>()
    private var _paginationParams = mutableStateOf(initialPaginationParams)
    private var _hasMore = mutableStateOf(true)
    private var _initialFetching = mutableStateOf(false)
    private var _moreFetching = mutableStateOf(false)

    val items: List<TItem> get() = _items
    val paginationParams: State<PaginationParams> get() = _paginationParams
    val hasMore: State<Boolean> get() = _hasMore
    val initialFetching: State<Boolean> get() = _initialFetching
    val moreFetching: State<Boolean> get() = _moreFetching

    private class QueryData<TQuery>(
        val query: TQuery,
        val paginationParams: PaginationParams
    )

    private var lastQueryData: QueryData<TQuery>? = null

    fun clearLastQueryData() {
        lastQueryData = null
    }

    fun isFetching() = _initialFetching.value || _moreFetching.value
    suspend fun fetchInitialItems(
        query: TQuery,
        search: suspend (TQuery, PaginationParams) -> PaginatedList<TItem>
    ) {
        val queryData = QueryData(query, _paginationParams.value)
        if (isFetching() || !isQueryDataChanged(queryData)) return
        _initialFetching.value = true
        resetPagination()
        val result = searchInternal(queryData, search)
        updateStateWithResult(result)
        _initialFetching.value = false
    }

    suspend fun fetchMoreItems(query: TQuery,        search: suspend (TQuery, PaginationParams) -> PaginatedList<TItem>) {
        val queryData = QueryData(query, _paginationParams.value.copy(pageNumber = _paginationParams.value.pageNumber + 1))
        if (isFetching() || !_hasMore.value || !isQueryDataChanged(queryData)) return
        _moreFetching.value = true
        val result = searchInternal(queryData, search)
        updateStateWithResult(result, append = true)
        _paginationParams.value = queryData.paginationParams
        _moreFetching.value = false
    }

    private fun resetPagination() {
        _paginationParams.value = initialPaginationParams
        _items.clear()
    }

    private fun updateStateWithResult(result: PaginatedList<TItem>, append: Boolean = false) {
        if (!append) {
            _items.clear()
        }
        _items.addAll(result.items)
        _hasMore.value = result.hasMore()
    }

    private fun isQueryDataChanged(queryData: QueryData<TQuery>): Boolean {
        val lastQueryData = lastQueryData ?: return true
        return lastQueryData.query != queryData.query || lastQueryData.paginationParams != queryData.paginationParams
    }

    private suspend fun searchInternal(
        queryData: QueryData<TQuery>,
        search: suspend (TQuery, PaginationParams) -> PaginatedList<TItem>
    ): PaginatedList<TItem> {
        lastQueryData = queryData
        return search(queryData.query, queryData.paginationParams)
    }

    fun updateItem(item: TItem, predicate: (TItem) -> Boolean) {
        val index = _items.indexOfFirst(predicate)
        if (index != -1) {
            _items[index] = item
        }
    }
}