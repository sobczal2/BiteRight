package com.sobczal2.biteright.dto

import java.util.Dictionary

data class ApiError(
    val message: String?,
    val errors: Dictionary<String, List<String>>?,
    val status: Status
)

enum class Status {
    OK,
    VALIDATION_ERROR,
    UNAUTHORIZED,
    CONNECTION_ERROR,
    UNKNOWN_ERROR
}