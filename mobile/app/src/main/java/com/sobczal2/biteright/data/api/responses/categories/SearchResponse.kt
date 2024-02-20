package com.sobczal2.biteright.data.api.responses.categories

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList

data class SearchResponse(
    @SerializedName("categories") val categories: PaginatedList<CategoryDto>
)
