package com.sobczal2.biteright.repositories.implementations

import arrow.core.Either
import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.ProductsApi
import com.sobczal2.biteright.data.api.requests.products.ChangeAmountRequest
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.data.api.requests.products.DeleteRequest
import com.sobczal2.biteright.data.api.requests.products.DisposeRequest
import com.sobczal2.biteright.data.api.requests.products.EditRequest
import com.sobczal2.biteright.data.api.requests.products.GetDetailsRequest
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.data.api.requests.products.RestoreRequest
import com.sobczal2.biteright.data.api.requests.products.SearchRequest
import com.sobczal2.biteright.data.api.responses.products.ChangeAmountResponse
import com.sobczal2.biteright.data.api.responses.products.CreateResponse
import com.sobczal2.biteright.data.api.responses.products.DeleteResponse
import com.sobczal2.biteright.data.api.responses.products.DisposeResponse
import com.sobczal2.biteright.data.api.responses.products.EditResponse
import com.sobczal2.biteright.data.api.responses.products.GetDetailsResponse
import com.sobczal2.biteright.data.api.responses.products.ListCurrentResponse
import com.sobczal2.biteright.data.api.responses.products.RestoreResponse
import com.sobczal2.biteright.data.api.responses.products.SearchResponse
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
    ): Either<ListCurrentResponse, RepositoryError> =
        safeApiCall {
            productApi.listCurrent(listCurrentRequest.sortingStrategy).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun create(createRequest: CreateRequest): Either<CreateResponse, RepositoryError> =
        safeApiCall {
            productApi.createProduct(createRequest).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun search(searchRequest: SearchRequest): Either<SearchResponse, RepositoryError> =
        safeApiCall {
            productApi.searchProducts(searchRequest).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun dispose(disposeRequest: DisposeRequest): Either<DisposeResponse, RepositoryError> =
        safeApiCall {
            productApi.disposeProduct(disposeRequest.productId).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun restore(restoreRequest: RestoreRequest): Either<RestoreResponse, RepositoryError> =
        safeApiCall {
            productApi.restoreProduct(restoreRequest.productId).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun changeAmount(changeAmountRequest: ChangeAmountRequest): Either<ChangeAmountResponse, RepositoryError> =
        safeApiCall {
            productApi.changeAmount(changeAmountRequest.productId, changeAmountRequest)
                .let { response ->
                    response.processResponse { it }
                }
        }

    override suspend fun getDetails(getDetailsRequest: GetDetailsRequest): Either<GetDetailsResponse, RepositoryError> =
        safeApiCall {
            productApi.getDetails(getDetailsRequest.productId).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun edit(editRequest: EditRequest): Either<EditResponse, RepositoryError> =
        safeApiCall {
            productApi.edit(editRequest.productId, editRequest).let { response ->
                response.processResponse { it }
            }
        }

    override suspend fun delete(deleteRequest: DeleteRequest): Either<DeleteResponse, RepositoryError> =
        safeApiCall {
            productApi.delete(deleteRequest.productId).let { response ->
                response.processResponse { it }
            }
    }
}