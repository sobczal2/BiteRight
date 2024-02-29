package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CategoriesApi
import com.sobczal2.biteright.data.api.requests.categories.GetDefaultRequest
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.data.api.responses.categories.GetDefaultResponse
import com.sobczal2.biteright.data.api.responses.categories.SearchResponse
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
    override suspend fun search(searchRequest: SearchRequest): Either<SearchResponse, RepositoryError> =
        safeApiCall {
            categoriesApi.search(searchRequest).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun getDefault(getDefaultRequest: GetDefaultRequest): Either<GetDefaultResponse, RepositoryError> =
        safeApiCall {
            categoriesApi.getDefault().let { response ->
                response.processResponse { it }
            }
        }
}