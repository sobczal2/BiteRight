package com.sobczal2.biteright.state

data class EditProfileScreenState(
    override val ongoingLoadingActions: Set<String> = setOf(),
    override val globalError: String? = null,

) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
