package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.repositories.common.RepositoryError

interface CurrencyRepository {
    suspend fun list(): Either<List<CurrencyDto>, RepositoryError>
}