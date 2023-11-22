package com.sobczal2.biteright.data.remote

import com.sobczal2.biteright.dto.user.MeResponse
import com.sobczal2.biteright.dto.user.SignInRequest
import com.sobczal2.biteright.dto.user.SignInResponse
import com.sobczal2.biteright.dto.user.SignUpRequest
import com.sobczal2.biteright.dto.user.SignUpResponse
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface UserApiDataSource {
    @POST("user/sign-up")
    suspend fun signUp(@Body signUpRequest: SignUpRequest): Response<SignUpResponse>

    @POST("user/sign-in")
    suspend fun signIn(@Body signInRequest: SignInRequest): Response<SignInResponse>

    @GET("user/me")
    suspend fun me(): Response<MeResponse>
}