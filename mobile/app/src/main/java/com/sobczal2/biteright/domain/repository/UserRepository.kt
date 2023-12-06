package com.sobczal2.biteright.domain.repository

import arrow.core.Either
import com.sobczal2.biteright.dto.common.ApiErrorMessageDto
import com.sobczal2.biteright.dto.user.UserDto
import kotlinx.coroutines.flow.Flow

interface UserRepository {
    suspend fun me(): Either<UserDto, ApiErrorMessageDto>
    suspend fun signIn(email: String, password: String): Either<Unit, ApiErrorMessageDto>
    suspend fun signUp(email: String, name: String, password: String): Either<Unit, ApiErrorMessageDto>
}
