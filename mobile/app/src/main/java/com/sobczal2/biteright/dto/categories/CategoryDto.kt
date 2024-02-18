package com.sobczal2.biteright.dto.categories

import androidx.compose.runtime.Composable
import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.util.getCategoryPhotoUrl
import java.util.UUID

data class CategoryDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("name") val name: String,
)

@Composable
fun CategoryDto.imageUri(): String {
    return getCategoryPhotoUrl(categoryId = id)
}