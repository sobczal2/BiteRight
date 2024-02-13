package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.responses.categories.SearchResponse
import retrofit2.Response
import retrofit2.http.GET
import retrofit2.http.Query

interface CategoriesApi {
    @GET("categories/search")
    suspend fun search(
        @Query("query") query: String,
        @Query("pageNumber") pageNumber: Int,
        @Query("pageSize") pageSize: Int
    ): Response<SearchResponse>
}