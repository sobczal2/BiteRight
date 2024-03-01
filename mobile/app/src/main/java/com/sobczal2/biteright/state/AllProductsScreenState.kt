package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.data.api.requests.products.FilteringParams
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.util.PaginationSource

data class AllProductsScreenState(
    val imageRequestBuilder: ImageRequest.Builder? = null,
    val searchQuery: SearchQuery = SearchQuery.Empty,
    val paginatedProductSource: PaginationSource<SimpleProductDto, SearchQuery> = PaginationSource(
        initialPaginationParams = PaginationParams.Default
    ),
    override val ongoingLoadingActions: Set<String> = setOf(),
) : ScreenState {

    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()

    class SearchQuery(
        val query: String,
        val filteringParams: FilteringParams,
        val sortingStrategy: ProductSortingStrategy,
    ) {
        companion object {
            val Empty = SearchQuery(
                query = "",
                filteringParams = FilteringParams.Empty,
                sortingStrategy = ProductSortingStrategy.Empty
            )
        }
    }
}
