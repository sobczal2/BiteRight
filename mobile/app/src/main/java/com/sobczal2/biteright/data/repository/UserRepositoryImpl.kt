package com.sobczal2.biteright.data.repository

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.local.UserSPDataSource
import com.sobczal2.biteright.data.remote.UserApiDataSource
import com.sobczal2.biteright.domain.repository.UserRepository
import com.sobczal2.biteright.dto.common.ApiErrorMessageDto
import com.sobczal2.biteright.dto.user.SignInRequest
import com.sobczal2.biteright.dto.user.SignUpRequest
import com.sobczal2.biteright.dto.user.UserDto
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
    private val userApiDataSource: UserApiDataSource, private val userSPDataSource: UserSPDataSource
) : UserRepository {

    override suspend fun me(): Either<UserDto, ApiErrorMessageDto> {
        return try {
            val response = userApiDataSource.me()

            if (response.isSuccessful) {
                val meResponse = response.body()!!
                Either.Left(meResponse.user)
            } else {
                val errorBody = response.errorBody()?.string()
                val apiError = if (errorBody != null) {
                    Gson().fromJson(errorBody, ApiErrorMessageDto::class.java)
                } else {
                    ApiErrorMessageDto.createForUnknownError()
                }
                Either.Right(apiError)
            }
        } catch (e: Exception) {
            Either.Right(ApiErrorMessageDto.createForUnknownError())
        }
    }

    override suspend fun signIn(email: String, password: String): Either<Unit, ApiErrorMessageDto> {
        return try {
            val signInRequest = SignInRequest(email, password)
            val response = userApiDataSource.signIn(signInRequest)

            if (response.isSuccessful) {
                val signInResponse = response.body()!!
                userSPDataSource.save(
                    signInResponse.userId,
                    signInResponse.jwt,
                    signInResponse.refreshToken
                )
                Either.Left(Unit)
            } else {
                val errorBody = response.errorBody()?.string()
                val apiError = if (errorBody != null) {
                    Gson().fromJson(errorBody, ApiErrorMessageDto::class.java)
                } else {
                    ApiErrorMessageDto.createForUnknownError()
                }
                Either.Right(apiError)
            }
        } catch (e: Exception) {
            Either.Right(ApiErrorMessageDto.createForUnknownError())
        }
    }

    override suspend fun signUp(email: String, name: String, password: String): Either<Unit, ApiErrorMessageDto> {
        return try {
            val signUpRequest = SignUpRequest(email, name, password)
            val response = userApiDataSource.signUp(signUpRequest)

            if (response.isSuccessful) {
                val signUpResponse = response.body()!!
                userSPDataSource.save(
                    signUpResponse.userId,
                    signUpResponse.jwt,
                    signUpResponse.refreshToken
                )
                Either.Left(Unit)
            } else {
                val errorBody = response.errorBody()?.string()
                val apiError = if (errorBody != null) {
                    Gson().fromJson(errorBody, ApiErrorMessageDto::class.java)
                } else {
                    ApiErrorMessageDto.createForUnknownError()
                }
                Either.Right(apiError)
            }
        } catch (e: Exception) {
            Either.Right(ApiErrorMessageDto.createForUnknownError())
        }
    }

}