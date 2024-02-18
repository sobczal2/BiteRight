package com.sobczal2.biteright.dto.common

import com.google.gson.annotations.SerializedName

data class PaginatedList<T>(
    @SerializedName("pageNumber") val pageNumber: Int,
    @SerializedName("pageSize") val pageSize: Int,
    @SerializedName("totalCount") val totalCount: Int,
    @SerializedName("totalPages") val totalPages: Int,
    @SerializedName("items") val items: List<T>,
)

data class PaginationParams(
    @SerializedName("pageNumber") val pageNumber: Int,
    @SerializedName("pageSize") val pageSize: Int,
)