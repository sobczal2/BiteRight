package com.sobczal2.biteright.data.api

import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.data.api.responses.MeResponse
import okhttp3.ResponseBody
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface UserApi {
    @GET("users/me")
    suspend fun me(): Response<MeResponse>

    @POST("users/onboard")
    suspend fun onboard(@Body onboardRequest: OnboardRequest): Response<ResponseBody>
}