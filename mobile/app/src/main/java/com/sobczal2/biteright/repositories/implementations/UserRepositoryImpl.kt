package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.UsersApi
import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.data.api.responses.users.MeResponse
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryImplBase
import com.sobczal2.biteright.util.StringProvider
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
    private val userApi: UsersApi,
    private val stringProvider: StringProvider,
    private val gson: Gson
) : RepositoryImplBase(gson, stringProvider, "UserRepositoryImpl"), UserRepository {
    override suspend fun me(): Either<MeResponse, RepositoryError> = safeApiCall {
        userApi.me().let { response ->
            response.processResponse { it }
        }
    }

    override suspend fun onboard(onboardRequest: OnboardRequest): Either<Unit, RepositoryError> =
        safeApiCall {
            userApi.onboard(onboardRequest).let { response ->
                response.processResponse { }
            }
        }
}