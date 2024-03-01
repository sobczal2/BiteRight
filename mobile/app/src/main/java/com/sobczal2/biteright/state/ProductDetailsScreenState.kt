package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.dto.products.DetailedProductDto
import com.sobczal2.biteright.dto.users.UserDto

data class ProductDetailsScreenState(
    val product: DetailedProductDto? = null,
    val user: UserDto? = null,
    val imageRequestBuilder: ImageRequest.Builder? = null,
    override val ongoingLoadingActions: Set<String> = emptySet(),
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
