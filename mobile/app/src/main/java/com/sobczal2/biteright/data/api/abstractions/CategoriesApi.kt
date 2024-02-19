package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.data.api.responses.categories.GetDefaultResponse
import com.sobczal2.biteright.data.api.responses.categories.SearchResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface CategoriesApi {
    @POST("categories/search")
    suspend fun search(
        @Body searchRequest: SearchRequest,
    ): Response<SearchResponse>

    @GET("categories/default")
    suspend fun getDefault(): Response<GetDefaultResponse>
}