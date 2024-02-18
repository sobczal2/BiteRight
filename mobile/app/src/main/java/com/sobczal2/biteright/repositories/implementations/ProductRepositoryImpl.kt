package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.ProductsApi
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.data.api.responses.products.CreateResponse
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.common.RepositoryError
import com.sobczal2.biteright.repositories.common.RepositoryImplBase
import com.sobczal2.biteright.util.StringProvider
import javax.inject.Inject

class ProductRepositoryImpl @Inject constructor(
    private val productApi: ProductsApi,
    stringProvider: StringProvider,
    gson: Gson
) : RepositoryImplBase(gson, stringProvider, "ProductRepositoryImpl"), ProductRepository {
    override suspend fun listCurrent(
        listCurrentRequest: ListCurrentRequest
    ): Either<List<SimpleProductDto>, RepositoryError> =
        safeApiCall {
            productApi.listCurrent(listCurrentRequest.sortingStrategy).let { response ->
                response.processResponse { it.products }
            }
        }

    override suspend fun createProduct(createRequest: CreateRequest): Either<CreateResponse, RepositoryError> =
        safeApiCall {
            productApi.createProduct(createRequest).let { response ->
                response.processResponse { it }
            }
        }
}