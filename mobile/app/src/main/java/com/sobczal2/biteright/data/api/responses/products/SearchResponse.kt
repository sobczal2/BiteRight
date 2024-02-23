package com.sobczal2.biteright.data.api.responses.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.products.SimpleProductDto

data class SearchResponse(
    @SerializedName("products") val products: PaginatedList<SimpleProductDto>
)