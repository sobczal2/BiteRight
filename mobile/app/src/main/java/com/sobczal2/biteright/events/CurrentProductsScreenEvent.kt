package com.sobczal2.biteright.events

import java.util.UUID

sealed class CurrentProductsScreenEvent {
    data object OnChangeAmountDialogDismiss : CurrentProductsScreenEvent()
    data class OnProductDispose(val productId: UUID) : CurrentProductsScreenEvent()
    data class OnProductLongClick(val productId: UUID) : CurrentProductsScreenEvent()
    data class OnChangeAmountDialogConfirm(val productId: UUID, val newAmount: Double) : CurrentProductsScreenEvent()
}