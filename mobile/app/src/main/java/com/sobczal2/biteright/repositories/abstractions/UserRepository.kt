package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.repositories.common.RepositoryError

interface UserRepository {
    suspend fun me(): Either<UserDto, RepositoryError>
    suspend fun onboard(onboardRequest: OnboardRequest): Either<Unit, RepositoryError>
}