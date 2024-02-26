package com.sobczal2.biteright.data.api.responses.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.products.DetailedProductDto

data class GetDetailsResponse(
    @SerializedName("product") val product: DetailedProductDto
)