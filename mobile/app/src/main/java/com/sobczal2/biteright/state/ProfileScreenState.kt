package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.users.UserDto

data class ProfileScreenState (
    val user: UserDto? = null,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenStateBase