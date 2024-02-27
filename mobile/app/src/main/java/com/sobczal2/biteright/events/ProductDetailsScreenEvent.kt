package com.sobczal2.biteright.events

import java.util.UUID

sealed class ProductDetailsScreenEvent {
    data class LoadDetails(val productId: UUID) : ProductDetailsScreenEvent()

}