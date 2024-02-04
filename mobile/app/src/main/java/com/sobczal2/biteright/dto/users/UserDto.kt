package com.sobczal2.biteright.dto.users

import java.time.Instant
import java.util.UUID

data class UserDto(
    val id: UUID,
    val identityId: String,
    val username: String,
    val email: String,
    val joinedAt: Instant,
    val profile: ProfileDto
)