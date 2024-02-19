package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CurrenciesApi
import com.sobczal2.biteright.data.api.responses.currencies.ListResponse
import com.sobczal2.biteright.dto.currencies.CurrencyDto
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
    override suspend fun list(): Either<ListResponse, RepositoryError> =
        safeApiCall {
            currenciesApi.list().let { response ->
                response.processResponse { it }
            }
        }
}