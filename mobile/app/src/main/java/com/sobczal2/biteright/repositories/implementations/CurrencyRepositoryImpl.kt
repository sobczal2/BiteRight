package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CurrenciesApi
import com.sobczal2.biteright.data.api.requests.currencies.GetDefaultRequest
import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest
import com.sobczal2.biteright.data.api.responses.currencies.GetDefaultResponse
import com.sobczal2.biteright.data.api.responses.currencies.SearchResponse
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryImplBase
import com.sobczal2.biteright.util.StringProvider
import javax.inject.Inject

class CurrencyRepositoryImpl @Inject constructor(
    private val currenciesApi: CurrenciesApi,
    stringProvider: StringProvider,
    gson: Gson
) : RepositoryImplBase(gson, stringProvider, "ProductRepositoryImpl"), CurrencyRepository {
    override suspend fun search(searchRequest: SearchRequest): Either<SearchResponse, RepositoryError> =
        safeApiCall {
            currenciesApi.search(searchRequest).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun getDefault(getDefaultRequest: GetDefaultRequest): Either<GetDefaultResponse, RepositoryError> =
        safeApiCall {
            currenciesApi.getDefault().let { response ->
                response.processResponse { it }
            }
        }
}