package com.sobczal2.biteright.data.api.requests.users

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class OnboardRequest(
    @SerializedName("username") val username: String,
    @SerializedName("currencyId") val currencyId: UUID,
    @SerializedName("timeZoneId") val timeZoneId: String
)
