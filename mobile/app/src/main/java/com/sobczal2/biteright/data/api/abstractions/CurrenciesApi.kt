package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest
import com.sobczal2.biteright.data.api.responses.currencies.SearchResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface CurrenciesApi {
    @POST("currencies/search")
    suspend fun search(@Body request: SearchRequest): Response<SearchResponse>
}