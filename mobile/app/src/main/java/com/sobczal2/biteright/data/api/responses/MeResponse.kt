package com.sobczal2.biteright.data.api.responses

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.users.UserDto

data class MeResponse(
    @SerializedName("user") val user: UserDto
)