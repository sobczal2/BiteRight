package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.responses.currencies.ListResponse
import retrofit2.Response
import retrofit2.http.GET

interface CurrenciesApi {
    @GET("currencies")
    suspend fun list(): Response<ListResponse>
}