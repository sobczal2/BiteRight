package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.products.DetailedProductDto
import com.sobczal2.biteright.dto.users.UserDto

data class ProductDetailsScreenState(
    val product: DetailedProductDto? = null,
    val user: UserDto? = null,
    override val ongoingLoadingActions: Set<String> = emptySet(),
    override val globalError: String? = null
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
