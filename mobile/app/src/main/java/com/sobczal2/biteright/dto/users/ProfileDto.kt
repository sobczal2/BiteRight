package com.sobczal2.biteright.dto.users

import java.util.UUID

data class ProfileDto(
    val currencyId: UUID,
    val currencyName: String
)