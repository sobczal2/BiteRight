package com.sobczal2.biteright.data.api

import java.util.Dictionary

data class ApiError(
    val message: String,
    val errors: Map<String, List<String>> = mapOf()
)