package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.products.SimpleProductDto

data class CurrentProductsScreenState(
    val currentProducts: List<SimpleProductDto> = emptyList(),
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenStateBase