package com.sobczal2.biteright.dto.common

import com.google.gson.annotations.SerializedName

data class PaginatedList<T>(
    @SerializedName("pageNumber") val pageNumber: Int,
    @SerializedName("pageSize") val pageSize: Int,
    @SerializedName("totalCount") val totalCount: Int,
    @SerializedName("totalPages") val totalPages: Int,
    @SerializedName("items") val items: List<T>,
) {
    fun hasMore() = pageNumber < totalPages - 1
}

fun <T> emptyPaginatedList() = PaginatedList<T>(
    pageNumber = 0,
    pageSize = 0,
    totalCount = 0,
    totalPages = 0,
    items = emptyList()
)

data class PaginationParams(
    @SerializedName("pageNumber") val pageNumber: Int,
    @SerializedName("pageSize") val pageSize: Int,
) {
    companion object {
        val Default = PaginationParams(
            pageNumber = 0,
            pageSize = 10
        )
    }
}