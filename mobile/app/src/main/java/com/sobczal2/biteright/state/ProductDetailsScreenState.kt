package com.sobczal2.biteright.state

data class ProductDetailsScreenState(
    override val globalLoading: Boolean = false,
    override val globalError: String? = null
) : ScreenState