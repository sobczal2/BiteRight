package com.sobczal2.biteright.data.api.responses.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.products.SimpleProductDto

data class ListCurrentResponse(
    @SerializedName("products") val products: List<SimpleProductDto>
)