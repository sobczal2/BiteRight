package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.UnitsApi
import com.sobczal2.biteright.data.api.requests.units.SearchRequest
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryImplBase
import com.sobczal2.biteright.util.StringProvider
import javax.inject.Inject

class UnitRepositoryImpl @Inject constructor(
    private val unitsApi: UnitsApi,
    private val stringProvider: StringProvider,
    private val gson: Gson
) : RepositoryImplBase(gson, stringProvider, "UnitRepository"), UnitRepository {
    override suspend fun search(request: SearchRequest): Either<PaginatedList<UnitDto>, RepositoryError> =
        safeApiCall {
            unitsApi.search(request).let { response ->
                response.processResponse { it.units }
            }
        }
}