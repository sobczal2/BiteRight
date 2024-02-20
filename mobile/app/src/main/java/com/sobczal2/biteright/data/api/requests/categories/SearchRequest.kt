package com.sobczal2.biteright.data.api.requests.categories

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.common.PaginationParams

data class SearchRequest(
    @SerializedName("query") val query: String,
    @SerializedName("paginationParams") val paginationParams: PaginationParams,
)