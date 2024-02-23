package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.products.ChangeAmountRequest
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.data.api.requests.products.SearchRequest
import com.sobczal2.biteright.data.api.responses.products.ChangeAmountResponse
import com.sobczal2.biteright.data.api.responses.products.CreateResponse
import com.sobczal2.biteright.data.api.responses.products.DisposeResponse
import com.sobczal2.biteright.data.api.responses.products.ListCurrentResponse
import com.sobczal2.biteright.data.api.responses.products.RestoreResponse
import com.sobczal2.biteright.data.api.responses.products.SearchResponse
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path
import retrofit2.http.Query
import java.util.UUID

interface ProductsApi {
    @GET("products/current")
    suspend fun listCurrent(@Query("sortingStrategy") sortingStrategy: ProductSortingStrategy): Response<ListCurrentResponse>

    @POST("products")
    suspend fun createProduct(@Body request: CreateRequest): Response<CreateResponse>

    @POST("products/search")
    suspend fun searchProducts(@Body request: SearchRequest): Response<SearchResponse>

    @PUT("products/{productId}/dispose")
    suspend fun disposeProduct(@Path("productId") productId: UUID): Response<DisposeResponse>

    @PUT("products/{productId}/restore")
    suspend fun restoreProduct(@Path("productId") productId: UUID): Response<RestoreResponse>

    @PUT("products/{productId}/amount")
    suspend fun changeAmount(@Path("productId") productId: UUID, @Body request: ChangeAmountRequest): Response<ChangeAmountResponse>
}