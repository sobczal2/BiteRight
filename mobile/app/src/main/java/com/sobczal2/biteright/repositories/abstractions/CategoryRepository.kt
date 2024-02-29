package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.categories.GetDefaultRequest
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.data.api.responses.categories.GetDefaultResponse
import com.sobczal2.biteright.data.api.responses.categories.SearchResponse
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.repositories.common.RepositoryError

interface CategoryRepository {
    suspend fun search(searchRequest: SearchRequest) : Either<SearchResponse, RepositoryError>
    suspend fun getDefault(getDefaultRequest: GetDefaultRequest) : Either<GetDefaultResponse, RepositoryError>
}