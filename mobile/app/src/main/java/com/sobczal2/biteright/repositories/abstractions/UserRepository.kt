package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.data.api.responses.users.MeResponse
import com.sobczal2.biteright.data.api.responses.users.OnboardResponse
import com.sobczal2.biteright.repositories.common.RepositoryError

interface UserRepository {
    suspend fun me(): Either<MeResponse, RepositoryError>
    suspend fun onboard(onboardRequest: OnboardRequest): Either<OnboardResponse, RepositoryError>
}