package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.dto.products.SimpleProductDto

data class CurrentProductsScreenState(
    val currentProducts: List<SimpleProductDto> = emptyList(),
    val imageRequestBuilder: ImageRequest.Builder? = null,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenStateBase