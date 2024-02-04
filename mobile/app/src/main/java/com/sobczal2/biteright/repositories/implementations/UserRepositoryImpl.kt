package com.sobczal2.biteright.repositories.implementations

import android.util.Log
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.module.kotlin.readValue
import com.sobczal2.biteright.data.api.ApiError
import com.sobczal2.biteright.data.api.UserApi
import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.repositories.common.NetworkRepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.UnknownRepositoryError
import retrofit2.HttpException
import retrofit2.awaitResponse
import java.io.IOException
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
    private val userApi: UserApi,
    private val objectMapper: ObjectMapper
) : UserRepository {
    override suspend fun me(): Either<UserDto, RepositoryError> {
        try {
            val response = userApi.me().awaitResponse()
            if (response.isSuccessful) {
                return response.body()!!.user.left()
            }
        } catch (e: Exception) {
            Log.e("UserRepositoryImpl", "Error while fetching user", e)
            return RepositoryError.fromRetrofitException(e, objectMapper).right()
        }

        return UnknownRepositoryError().right()
    }

    override suspend fun onboard(onboardRequest: OnboardRequest): Either<Unit, RepositoryError> {
        try {
            val response = userApi.onboard(onboardRequest).awaitResponse()
            if (response.isSuccessful) {
                return Unit.left()
            }
        } catch (e: Exception) {
            Log.e("UserRepositoryImpl", "Error while onboarding user", e)
            return RepositoryError.fromRetrofitException(e, objectMapper).right()
        }

        return UnknownRepositoryError().right()
    }
}