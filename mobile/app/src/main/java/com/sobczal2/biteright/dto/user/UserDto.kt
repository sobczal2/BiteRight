package com.sobczal2.biteright.dto.user

import com.google.gson.annotations.SerializedName

data class UserDto(
    @SerializedName("user_id")
    val userId: Int,
    val email: String,
    val name: String,
)

data class SignUpRequest(
    val email: String,
    val name: String,
    val password: String,
)

data class SignUpResponse(
    @SerializedName("user_id")
    val userId: Int,
    val jwt: String,
    @SerializedName("refresh_token")
    val refreshToken: String,
)

data class SignInRequest(
    val email: String,
    val password: String,
)

data class SignInResponse(
    @SerializedName("user_id")
    val userId: Int,
    val jwt: String,
    @SerializedName("refresh_token")
    val refreshToken: String,
)

data class MeResponse(
    val user: UserDto,
)