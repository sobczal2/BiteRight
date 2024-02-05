package com.sobczal2.biteright.data.api.requests

import com.google.gson.annotations.SerializedName

data class OnboardRequest(
    @SerializedName("username") val username: String,
)
