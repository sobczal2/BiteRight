package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.units.SearchRequest
import com.sobczal2.biteright.data.api.responses.units.SearchResponse
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.repositories.common.RepositoryError

interface UnitRepository {
    suspend fun search(request: SearchRequest) : Either<SearchResponse, RepositoryError>
}