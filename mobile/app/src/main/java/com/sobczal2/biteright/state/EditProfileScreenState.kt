package com.sobczal2.biteright.state

data class EditProfileScreenState(
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,

) : ScreenState