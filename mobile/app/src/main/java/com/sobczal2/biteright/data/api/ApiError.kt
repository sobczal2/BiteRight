package com.sobczal2.biteright.data.api

import com.google.gson.annotations.SerializedName

data class ApiError(
    @SerializedName("message") val message: String,
    @SerializedName("errors") val errors: Map<String, List<String>> = mapOf()
)