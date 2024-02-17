package com.sobczal2.biteright.state

data class WelcomeScreenState(
    override val globalLoading: Boolean = false,
    override val globalError: String? = null
) : ScreenStateBase
