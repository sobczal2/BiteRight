package com.sobczal2.biteright.state

import com.sobczal2.biteright.util.ResourceIdOrString

data class StartScreenState(
    val username: String = "",
    val usernameError: ResourceIdOrString? = null,
    val loading: Boolean = false,
    val generalError: ResourceIdOrString? = null,
)