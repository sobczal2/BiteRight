package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.data.api.common.ApiError

class ApiRepositoryError(
    private val apiError: ApiError,
    val apiErrorCode: Int,
    val apiErrors: Map<String, List<String>>
) : RepositoryError {
    override val message: String
        get() = apiError.message
}