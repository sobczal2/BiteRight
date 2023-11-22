package com.sobczal2.biteright.domain.repository

import com.sobczal2.biteright.dto.user.UserDto
import kotlinx.coroutines.flow.Flow

interface UserRepository {
    suspend fun me(): Result<UserDto>
    suspend fun signIn(email: String, password: String): Result<Unit>
    suspend fun signUp(email: String, password: String, name: String): Result<Unit>
}
