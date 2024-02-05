package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.data.api.ApiError
import com.sobczal2.biteright.util.ResourceIdOrString

class ApiRepositoryError(
    private val apiError: ApiError,
    val apiErrorCode: Int,
    val apiErrors: Map<String, List<String>>
) : RepositoryError {
    override val message: ResourceIdOrString
        get() = ResourceIdOrString(apiError.message)
}