package com.sobczal2.biteright.domain.repository

import arrow.core.Either
import com.auth0.android.result.Credentials
import com.sobczal2.biteright.dto.ApiError
import com.sobczal2.biteright.dto.UserDto

interface UserRepository {
    fun login(credentials: Credentials)
    fun logout()
    fun isLoggedIn(): Boolean
    suspend fun me(): Either<UserDto, ApiError>
}