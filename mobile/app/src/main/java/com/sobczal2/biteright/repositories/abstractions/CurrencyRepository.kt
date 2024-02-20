package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest
import com.sobczal2.biteright.data.api.responses.currencies.SearchResponse
import com.sobczal2.biteright.repositories.common.RepositoryError

interface CurrencyRepository {
    suspend fun search(
        request: SearchRequest
    ): Either<SearchResponse, RepositoryError>
}