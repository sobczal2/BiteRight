package com.sobczal2.biteright.state

import com.sobczal2.biteright.util.ResourceIdOrString

data class StartScreenState(
    val username: String = "",
    val loading: Boolean = false,
    val error: ResourceIdOrString? = null,
    val canContinue: Boolean = false
)