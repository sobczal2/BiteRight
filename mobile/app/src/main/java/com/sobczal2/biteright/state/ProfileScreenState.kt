package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.users.UserDto

data class ProfileScreenState (
    val user: UserDto? = null,
    override val ongoingLoadingActions: Set<String> = setOf(),
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
