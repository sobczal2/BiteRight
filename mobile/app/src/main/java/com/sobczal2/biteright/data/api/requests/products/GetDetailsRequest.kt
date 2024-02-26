package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class GetDetailsRequest(
    @SerializedName("productId") val productId: UUID
)