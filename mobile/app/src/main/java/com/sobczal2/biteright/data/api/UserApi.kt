package com.sobczal2.biteright.data.api

import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.data.api.responses.MeResponse
import com.sobczal2.biteright.dto.users.UserDto
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.POST

interface UserApi {
    @GET("users/me")
    suspend fun me(): Call<MeResponse>

    @POST("users/onboard")
    suspend fun onboard(onboardRequest: OnboardRequest): Call<Unit>
}