package com.sobczal2.biteright.repositories.common

import android.util.Log
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.common.ApiError
import retrofit2.Response

open class RepositoryImplBase(
    private val gson: Gson,
    @Suppress("PrivatePropertyName") private val TAG: String
) {
    protected suspend fun <T> safeApiCall(apiCall: suspend () -> Either<T, RepositoryError>): Either<T, RepositoryError> {
        return try {
            apiCall()
        } catch (e: Exception) {
            Log.e(TAG, "Error during API call", e)
            RepositoryError.fromRetrofitException(e, gson).right()
        }
    }

    protected fun <T, R> Response<T>.processResponse(
        successHandler: (T) -> R
    ): Either<R, RepositoryError> {
        return if (isSuccessful && body() != null) {
            successHandler(body()!!).left()
        } else {
            parseApiError().right()
        }
    }

    private fun Response<*>.parseApiError(): RepositoryError {
        val errorBody = errorBody()?.string()
        val apiError = gson.fromJson(errorBody, ApiError::class.java) ?: ApiError("Unknown error")
        return ApiRepositoryError(apiError, code(), apiError.errors)
    }
}