package com.sobczal2.biteright.dto.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.units.UnitDto
import java.time.Instant
import java.time.LocalDate
import java.time.LocalDateTime
import java.util.UUID

data class DetailedProductDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("name") val name: String,
    @SerializedName("description") val description: String,
    @SerializedName("priceValue") val priceValue: Double?,
    @SerializedName("priceCurrency") val priceCurrency: CurrencyDto?,
    @SerializedName("expirationDateKind") val expirationDateKind: ExpirationDateKindDto,
    @SerializedName("expirationDateValue") val expirationDateValue: LocalDate?,
    @SerializedName("category") val category: CategoryDto,
    @SerializedName("addedDateTime") val addedDateTime: Instant,
    @SerializedName("amountCurrentValue") val amountCurrentValue: Double,
    @SerializedName("amountMaxValue") val amountMaxValue: Double,
    @SerializedName("amountUnit") val amountUnit: UnitDto,
    @SerializedName("disposedStateValue") val disposedStateValue: Boolean,
    @SerializedName("disposedStateDateTime") val disposedStateDateTime: Instant?
)