package com.sobczal2.biteright.repositories.common

import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.module.kotlin.readValue
import com.sobczal2.biteright.data.api.ApiError
import com.sobczal2.biteright.util.ResourceIdOrString
import java.io.IOException

interface RepositoryError {
    val message: ResourceIdOrString

    companion object {
        fun fromRetrofitException(e: Exception, objectMapper: ObjectMapper): RepositoryError =
            when (e) {
                is retrofit2.HttpException -> {
                    when (e.code()) {
                        404 -> ApiRepositoryError(ApiError("Not found"), 404)
                        400 -> {
                            e.response()?.let {
                                if (it.errorBody() == null) {
                                    NetworkRepositoryError()
                                } else {
                                    val apiError = objectMapper.readValue<ApiError>(
                                        it.errorBody()!!.string()
                                    )
                                    ApiRepositoryError(apiError, it.code())
                                }
                            }

                            UnknownRepositoryError()
                        }

                        else -> UnknownRepositoryError()
                    }
                }

                is IOException -> NetworkRepositoryError()
                else -> UnknownRepositoryError()
            }
    }
}