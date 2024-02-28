package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.products.ChangeAmountRequest
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.data.api.requests.products.DisposeRequest
import com.sobczal2.biteright.data.api.requests.products.EditRequest
import com.sobczal2.biteright.data.api.requests.products.GetDetailsRequest
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.data.api.requests.products.RestoreRequest
import com.sobczal2.biteright.data.api.requests.products.SearchRequest
import com.sobczal2.biteright.data.api.responses.products.ChangeAmountResponse
import com.sobczal2.biteright.data.api.responses.products.CreateResponse
import com.sobczal2.biteright.data.api.responses.products.DisposeResponse
import com.sobczal2.biteright.data.api.responses.products.EditResponse
import com.sobczal2.biteright.data.api.responses.products.GetDetailsResponse
import com.sobczal2.biteright.data.api.responses.products.ListCurrentResponse
import com.sobczal2.biteright.data.api.responses.products.RestoreResponse
import com.sobczal2.biteright.data.api.responses.products.SearchResponse
import com.sobczal2.biteright.repositories.common.RepositoryError

interface ProductRepository {
    suspend fun listCurrent(listCurrentRequest: ListCurrentRequest): Either<ListCurrentResponse, RepositoryError>
    suspend fun create(createRequest: CreateRequest): Either<CreateResponse, RepositoryError>
    suspend fun search(searchRequest: SearchRequest): Either<SearchResponse, RepositoryError>
    suspend fun dispose(disposeRequest: DisposeRequest): Either<DisposeResponse, RepositoryError>
    suspend fun restore(restoreRequest: RestoreRequest): Either<RestoreResponse, RepositoryError>
    suspend fun changeAmount(changeAmountRequest: ChangeAmountRequest): Either<ChangeAmountResponse, RepositoryError>
    suspend fun getDetails(getDetailsRequest: GetDetailsRequest): Either<GetDetailsResponse, RepositoryError>
    suspend fun edit(editRequest: EditRequest): Either<EditResponse, RepositoryError>
}