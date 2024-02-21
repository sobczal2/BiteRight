package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import java.util.UUID

data class SearchRequest(
    @SerializedName("query") val query: String,
    @SerializedName("filteringParams") val filteringParams: FilteringParams,
    @SerializedName("sortingStrategy") val sortingStrategy: ProductSortingStrategy,
    @SerializedName("paginationParams") val paginationParams: PaginationParams
)


data class FilteringParams(
    @SerializedName("categoryIds") val categoryIds: List<UUID>
)