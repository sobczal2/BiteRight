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
    @SerializedName("currentAmount") val currentAmount: Double,
    @SerializedName("maxAmount") val maxAmount: Double,
    @SerializedName("unitAbbreviation") val unitAbbreviation: String,
    @SerializedName("disposed") val disposed: Boolean
) {
    fun getAmountPercentage(): Double {
        return currentAmount / maxAmount * 100
    }
}