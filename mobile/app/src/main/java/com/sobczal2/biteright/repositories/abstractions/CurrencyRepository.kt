package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.currencies.GetDefaultRequest
import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest
import com.sobczal2.biteright.data.api.responses.currencies.GetDefaultResponse
import com.sobczal2.biteright.data.api.responses.currencies.SearchResponse
import com.sobczal2.biteright.repositories.common.RepositoryError

interface CurrencyRepository {
    suspend fun search(
        searchRequest: SearchRequest
    ): Either<SearchResponse, RepositoryError>

    suspend fun getDefault(
        getDefaultRequest: GetDefaultRequest
    ): Either<GetDefaultResponse, RepositoryError>
}