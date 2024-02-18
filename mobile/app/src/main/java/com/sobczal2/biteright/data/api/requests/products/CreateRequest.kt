package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import java.time.LocalDate
import java.util.UUID

data class CreateRequest(
    @SerializedName("name") val name: String,
    @SerializedName("description") val description: String,
    @SerializedName("price") val price: Double?,
    @SerializedName("currencyId") val currencyId: UUID?,
    @SerializedName("expirationDateKind") val expirationDateKind: ExpirationDateKindDto,
    @SerializedName("expirationDate") val expirationDate: LocalDate?,
    @SerializedName("categoryId") val categoryId: UUID,
    @SerializedName("maximumAmountValue") val maximumAmountValue: Double,
    @SerializedName("amountUnitId") val amountUnitId: UUID,
)