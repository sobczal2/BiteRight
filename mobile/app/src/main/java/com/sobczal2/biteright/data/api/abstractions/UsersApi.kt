package com.sobczal2.biteright.data.api.abstractions

import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.data.api.requests.users.UpdateProfileRequest
import com.sobczal2.biteright.data.api.responses.users.MeResponse
import com.sobczal2.biteright.data.api.responses.users.OnboardResponse
import com.sobczal2.biteright.data.api.responses.users.UpdateProfileResponse
import okhttp3.ResponseBody
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT

interface UsersApi {
    @GET("users/me")
    suspend fun me(): Response<MeResponse>

    @POST("users/onboard")
    suspend fun onboard(@Body onboardRequest: OnboardRequest): Response<OnboardResponse>

    @PUT("users/profile")
    suspend fun updateProfile(@Body updateProfileRequest: UpdateProfileRequest): Response<UpdateProfileResponse>
}