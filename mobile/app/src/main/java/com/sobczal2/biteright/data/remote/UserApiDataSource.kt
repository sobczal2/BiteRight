package com.sobczal2.biteright.data.remote

import com.sobczal2.biteright.dto.UserDto
import retrofit2.Call
import retrofit2.http.GET

interface UserApiDataSource {
    @GET("/api/users/me")
    suspend fun me(): Call<UserDto>
}