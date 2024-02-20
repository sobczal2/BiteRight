package com.sobczal2.biteright.events

import java.util.UUID

sealed class CurrentProductsScreenEvent {
    data class OnProductDispose(val productId: UUID) : CurrentProductsScreenEvent()
}