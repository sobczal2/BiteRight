package com.sobczal2.biteright.repositories.common

import com.google.gson.Gson
import com.sobczal2.biteright.data.api.common.ApiError
import com.sobczal2.biteright.util.StringProvider
import java.io.IOException

interface RepositoryError {
    val message: String

    companion object {
        fun fromRetrofitException(
            e: Exception,
            gson: Gson,
            stringProvider: StringProvider
        ): RepositoryError =
            when (e) {
                is retrofit2.HttpException -> {
                    when (e.code()) {
                        404 -> ApiRepositoryError(ApiError("Not found"), 404, emptyMap())
                        400 -> {
                            e.response()?.let {
                                if (it.errorBody() == null) {
                                    NetworkRepositoryError(stringProvider)
                                } else {
                                    val apiError = gson.fromJson(
                                        it.errorBody()!!.string(),
                                        ApiError::class.java
                                    )
                                    ApiRepositoryError(apiError, it.code(), apiError.errors)
                                }
                            }

                            UnknownRepositoryError(stringProvider)
                        }

                        else -> UnknownRepositoryError(stringProvider)
                    }
                }

                is IOException -> NetworkRepositoryError(stringProvider)
                else -> UnknownRepositoryError(stringProvider)
            }
    }
}