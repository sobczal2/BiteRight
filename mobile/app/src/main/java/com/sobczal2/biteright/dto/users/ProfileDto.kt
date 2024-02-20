package com.sobczal2.biteright.dto.users

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import java.util.UUID

data class ProfileDto(
    @SerializedName("currency") val currency: CurrencyDto,
    @SerializedName("timeZoneId") val timeZoneId: String,
)