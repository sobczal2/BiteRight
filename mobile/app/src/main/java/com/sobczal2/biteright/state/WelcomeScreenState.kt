package com.sobczal2.biteright.state

data class WelcomeScreenState(
    override val ongoingLoadingActions: Set<String> = emptySet(),
) : ScreenState
