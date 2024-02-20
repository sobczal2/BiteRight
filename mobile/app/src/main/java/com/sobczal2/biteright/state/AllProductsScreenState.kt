package com.sobczal2.biteright.state

import coil.request.ImageRequest

data class AllProductsScreenState(
    val imageRequestBuilder: ImageRequest.Builder? = null,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenState
