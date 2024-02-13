package com.sobczal2.biteright.dto.categories

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class CategoryDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("name") val name: String,
)
