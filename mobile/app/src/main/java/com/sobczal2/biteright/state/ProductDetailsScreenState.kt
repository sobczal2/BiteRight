package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.products.DetailedProductDto

data class ProductDetailsScreenState(
    val product: DetailedProductDto? = null,
    override val ongoingLoadingActions: Set<String> = emptySet(),
    override val globalError: String? = null
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
