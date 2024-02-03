package com.sobczal2.biteright.data.repository

import arrow.core.Either
import com.auth0.android.authentication.AuthenticationException
import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.authentication.storage.CredentialsManagerException
import com.auth0.android.callback.Callback
import com.auth0.android.result.Credentials
import com.fasterxml.jackson.databind.ObjectMapper
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.remote.UserApiDataSource
import com.sobczal2.biteright.domain.repository.UserRepository
import com.sobczal2.biteright.dto.ApiError
import com.sobczal2.biteright.dto.Status
import com.sobczal2.biteright.dto.UserDto
import kotlinx.coroutines.flow.Flow
import retrofit2.awaitResponse
import java.net.ConnectException
import javax.inject.Inject
import kotlin.coroutines.suspendCoroutine

class UserRepositoryImpl @Inject constructor(
    private val credentialsManager: CredentialsManager,
    private val userApiDataSource: UserApiDataSource,
    private val objectMapper: ObjectMapper
) : UserRepository {
    override fun login(credentials: Credentials) {
        credentialsManager.saveCredentials(credentials)
    }

    override fun logout() {
        credentialsManager.clearCredentials()
    }

    override fun isLoggedIn(): Boolean {
        return credentialsManager.hasValidCredentials()
    }

    override suspend fun me(): Either<UserDto, ApiError> {
        try {
            val response = userApiDataSource.me().awaitResponse()
            if (response.isSuccessful) {
                return Either.Left(response.body()!!)
            } else if (response.code() == 400) {
                val apiError = objectMapper.readValue(
                    response.errorBody()!!.string(),
                    ApiError::class.java
                )

                return Either.Right(
                    ApiError(
                        apiError.message,
                        apiError.errors,
                        Status.VALIDATION_ERROR
                    )
                )
            } else {
                val apiError = ApiError(
                    null,
                    null,
                    Status.UNKNOWN_ERROR
                )
                return Either.Right(apiError)
            }
        } catch (e: ConnectException) {
            val apiError = ApiError(
                null,
                null,
                Status.CONNECTION_ERROR
            )
            return Either.Right(apiError)
        } catch (e: Exception) {
            val apiError = ApiError(
                null,
                null,
                Status.UNKNOWN_ERROR
            )
            return Either.Right(apiError)
        }
    }
}