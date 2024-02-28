package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.data.api.common.Exclude
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import java.time.LocalDate
import java.util.UUID

data class EditRequest(
    @Exclude val productId: UUID,
    @SerializedName("name") val name: String,
    @SerializedName("description") val description: String,
    @SerializedName("priceValue") val priceValue: Double?,
    @SerializedName("priceCurrencyId") val priceCurrencyId: UUID?,
    @SerializedName("expirationDateKind") val expirationDateKind: ExpirationDateKindDto,
    @SerializedName("expirationDate") val expirationDate: LocalDate?,
    @SerializedName("categoryId") val categoryId: UUID,
    @SerializedName("amountCurrentValue") val amountCurrentValue: Double,
    @SerializedName("amountMaxValue") val amountMaxValue: Double,
    @SerializedName("amountUnitId") val amountUnitId: UUID
)