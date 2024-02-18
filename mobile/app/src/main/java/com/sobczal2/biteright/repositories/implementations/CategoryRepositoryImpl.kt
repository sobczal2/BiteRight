package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CategoriesApi
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryImplBase
import com.sobczal2.biteright.util.StringProvider
import javax.inject.Inject

class CategoryRepositoryImpl @Inject constructor(
    private val categoriesApi: CategoriesApi,
    stringProvider: StringProvider,
    gson: Gson
) : RepositoryImplBase(gson, stringProvider, "CategoryRepositoryImpl"), CategoryRepository {
    override suspend fun search(request: SearchRequest): Either<PaginatedList<CategoryDto>, RepositoryError> =
        safeApiCall {
            categoriesApi.search(request).let { response ->
                response.processResponse { it.categories }
            }
        }
}