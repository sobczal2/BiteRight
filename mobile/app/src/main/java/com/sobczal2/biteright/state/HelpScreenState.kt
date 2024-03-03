package com.sobczal2.biteright.state

data class HelpScreenState(
    override val ongoingLoadingActions: Set<String> = emptySet()
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}