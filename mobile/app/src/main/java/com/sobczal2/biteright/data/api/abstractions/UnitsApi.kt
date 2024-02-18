package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.units.SearchRequest
import com.sobczal2.biteright.data.api.responses.units.SearchResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.POST

interface UnitsApi {
    @POST("units/search")
    suspend fun search(
        @Body searchRequest: SearchRequest,
    ): Response<SearchResponse>
}