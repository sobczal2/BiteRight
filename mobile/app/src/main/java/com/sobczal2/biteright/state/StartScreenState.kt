package com.sobczal2.biteright.state

import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState

data class StartScreenState(
    val usernameFieldState: TextFormFieldState = TextFormFieldState(),
    val formSubmitting: Boolean = false,
    override val ongoingLoadingActions: Set<String> = emptySet(),
    override val globalError: String? = null,
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
