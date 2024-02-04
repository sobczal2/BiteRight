package com.sobczal2.biteright.state

import com.sobczal2.biteright.util.ResourceIdOrString

data class HomeViewModelState(
    val username: String = "",
    val email: String = "",
    val loading: Boolean = false,
    val error: ResourceIdOrString? = null
)