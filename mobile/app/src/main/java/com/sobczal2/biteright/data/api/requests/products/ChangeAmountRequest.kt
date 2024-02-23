package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.data.api.common.Exclude
import java.util.UUID

data class ChangeAmountRequest(
    @Exclude val productId: UUID,
    @SerializedName("amount") val amount: Double
)