package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.data.api.responses.products.ListCurrentResponse
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.dto.products.SimpleProductDto
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Query

interface ProductsApi {
    @GET("products/current")
    suspend fun listCurrent(@Query("sortingStrategy") sortingStrategy: ProductSortingStrategy): Response<ListCurrentResponse>
}