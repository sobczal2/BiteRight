package com.sobczal2.biteright.data.api.responses.currencies

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.currencies.CurrencyDto

data class GetDefaultResponse(
    @SerializedName("currency") val currency: CurrencyDto
)