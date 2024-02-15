package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.util.ResourceIdOrString

data class ProfileScreenState (
    val user: UserDto? = null,
    val loading: Boolean = false,
    val error: ResourceIdOrString? = null
)