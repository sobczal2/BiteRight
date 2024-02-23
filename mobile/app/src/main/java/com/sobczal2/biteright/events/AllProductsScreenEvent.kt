package com.sobczal2.biteright.events

import java.util.UUID

sealed class AllProductsScreenEvent {
    data object FetchMoreProducts : AllProductsScreenEvent()
    data class OnProductDispose(val productId: UUID) : AllProductsScreenEvent()
    data class OnProductRestore(val productId: UUID) : AllProductsScreenEvent()
}