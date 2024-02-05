package com.sobczal2.biteright.state

import com.sobczal2.biteright.util.ResourceIdOrString

data class WelcomeScreenState(
    val loading: Boolean = false,
    val generalError: ResourceIdOrString? = null
)
