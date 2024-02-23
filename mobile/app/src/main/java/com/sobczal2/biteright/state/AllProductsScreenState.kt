package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.data.api.requests.products.FilteringParams
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.util.PaginationSource

data class AllProductsScreenState(
    val imageRequestBuilder: ImageRequest.Builder? = null,
    val searchQuery: SearchQuery = SearchQuery.Empty,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenState {

    lateinit var paginatedProductSource: PaginationSource<SimpleProductDto, SearchQuery>

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
