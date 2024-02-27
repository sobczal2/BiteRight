package com.sobczal2.biteright.dto.users

import com.google.gson.annotations.SerializedName
import java.time.Instant
import java.time.LocalDate
import java.time.LocalDateTime
import java.util.Date
import java.util.UUID

data class UserDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("identityId") val identityId: String,
    @SerializedName("username") val username: String,
    @SerializedName("email") val email: String,
    @SerializedName("joinedAt") val joinedAt: Instant,
    @SerializedName("profile") val profile: ProfileDto
)