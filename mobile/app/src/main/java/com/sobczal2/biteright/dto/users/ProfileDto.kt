package com.sobczal2.biteright.dto.users

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class ProfileDto(
    @SerializedName("currencyId") val currencyId: UUID,
    @SerializedName("currencyName") val currencyName: String
)