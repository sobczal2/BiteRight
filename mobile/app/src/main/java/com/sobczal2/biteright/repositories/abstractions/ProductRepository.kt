package com.sobczal2.biteright.repositories.abstractions

import arrow.core.Either
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.data.api.responses.products.CreateResponse
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.repositories.common.RepositoryError

interface ProductRepository {
    suspend fun listCurrent(listCurrentRequest: ListCurrentRequest): Either<List<SimpleProductDto>, RepositoryError>

    suspend fun createProduct(createRequest: CreateRequest): Either<CreateResponse, RepositoryError>
}