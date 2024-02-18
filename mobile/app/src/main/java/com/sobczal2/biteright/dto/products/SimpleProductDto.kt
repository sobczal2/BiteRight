package com.sobczal2.biteright.dto.products

import com.google.gson.annotations.SerializedName
import java.time.LocalDate
import java.time.LocalDateTime
import java.util.UUID

data class SimpleProductDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("name") val name: String,
    @SerializedName("expirationDate") val expirationDate: LocalDate?,
    @SerializedName("categoryId") val categoryId: UUID,
    @SerializedName("addedDateTime") val addedDateTime: LocalDateTime,
    @SerializedName("amountPercentage") val amountPercentage: Double,
    @SerializedName("disposed") val disposed: Boolean
)