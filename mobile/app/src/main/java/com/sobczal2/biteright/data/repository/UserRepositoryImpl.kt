package com.sobczal2.biteright.data.repository

import com.sobczal2.biteright.data.local.UserSPDataSource
import com.sobczal2.biteright.data.remote.UserApiDataSource
import com.sobczal2.biteright.domain.repository.UserRepository
import com.sobczal2.biteright.dto.user.SignInRequest
import com.sobczal2.biteright.dto.user.UserDto
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
    private val userApiDataSource: UserApiDataSource, private val userSPDataSource: UserSPDataSource
) : UserRepository {

    override suspend fun me(): Result<UserDto> {
        return try {
            val response = userApiDataSource.me()

            if (response.isSuccessful) {
                val meResponse = response.body()!!
                Result.success(meResponse.user)
            } else {
                Result.failure(Exception(response.errorBody()?.string()))
            }
        }
        catch (e: Exception) {
            Result.failure(e)
        }
    }

    override suspend fun signIn(email: String, password: String): Result<Unit> {
        return try {
            val signInRequest = SignInRequest(email, password)
            val response = userApiDataSource.signIn(signInRequest)

            if (response.isSuccessful) {
                val signInResponse = response.body()!!
                userSPDataSource.save(signInResponse.userId, signInResponse.jwt, signInResponse.refreshToken)
                Result.success(Unit)
            } else {
                Result.failure(Exception(response.errorBody()?.string()))
            }
        }
        catch (e: Exception) {
            Result.failure(e)
        }
    }

    override suspend fun signUp(email: String, password: String, name: String): Result<Unit> {
        TODO("Not yet implemented")
    }
}