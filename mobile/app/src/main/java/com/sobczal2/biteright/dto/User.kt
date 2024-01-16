package com.sobczal2.biteright.dto

import java.util.Date
import java.util.UUID

data class UserDto(
    val id: UUID,
    val identityId: String,
    val username: String,
    val email: String,
    val joinedAt: Date,
    val profile: ProfileDto
)



data class ProfileDto(
    val countryId: UUID,
    val countryName: String,
    val languageId: UUID,
    val languageName: String,
    val languageCode: String,
    val currencyId: UUID,
    val currencyName: String,
)