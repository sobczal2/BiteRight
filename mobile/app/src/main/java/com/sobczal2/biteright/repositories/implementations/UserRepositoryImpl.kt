package com.sobczal2.biteright.repositories.implementations

import android.util.Log
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.ApiError
import com.sobczal2.biteright.data.api.UserApi
import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryError
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
    private val userApi: UserApi,
    private val gson: Gson
) : UserRepository {
    override suspend fun me(): Either<UserDto, RepositoryError> {
        return try {
            val response = userApi.me()
            if (response.isSuccessful) {
                response.body()!!.user.left()
            } else {
                val errorBody = response.errorBody()?.string()
                val apiError = gson.fromJson(errorBody, ApiError::class.java)
                ApiRepositoryError(apiError, response.code(), apiError.errors).right()
            }
        } catch (e: Exception) {
            Log.e("UserRepositoryImpl", "Error while fetching user", e)
            RepositoryError.fromRetrofitException(e, gson).right()
        }
    }

    override suspend fun onboard(onboardRequest: OnboardRequest): Either<Unit, RepositoryError> {
        return try {
            val response = userApi.onboard(onboardRequest)
            if (response.isSuccessful) {
                Unit.left()
            } else {
                val errorBody = response.errorBody()?.string()
                val apiError = gson.fromJson(errorBody, ApiError::class.java)
                ApiRepositoryError(apiError, response.code(), apiError.errors).right()
            }
        } catch (e: Exception) {
            Log.e("UserRepositoryImpl", "Error while onboarding user", e)
            RepositoryError.fromRetrofitException(e, gson).right()
        }
    }
}