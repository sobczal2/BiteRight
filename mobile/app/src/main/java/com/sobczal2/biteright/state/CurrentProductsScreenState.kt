package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.dto.products.SimpleProductDto
import java.util.UUID

data class CurrentProductsScreenState(
    val currentProducts: List<SimpleProductDto> = emptyList(),
    val imageRequestBuilder: ImageRequest.Builder? = null,
    val changeAmountDialogTargetId: UUID? = null,
    val changeAmountDialogLoading: Boolean = false,
    override val ongoingLoadingActions: Set<String> = emptySet(),
    override val globalError: String? = null,
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
