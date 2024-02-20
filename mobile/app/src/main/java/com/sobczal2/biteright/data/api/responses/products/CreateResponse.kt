package com.sobczal2.biteright.data.api.responses.products

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class CreateResponse(
    @SerializedName("productId") val productId: UUID,
)