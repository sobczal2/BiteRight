package com.sobczal2.biteright.state

import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState

data class StartScreenState(
    val usernameFieldState: TextFormFieldState = TextFormFieldState(),
    val formSubmitting: Boolean = false,
    override val globalLoading: Boolean = true,
    override val globalError: String? = null,
) : ScreenStateBase