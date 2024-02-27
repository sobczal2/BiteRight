package com.sobczal2.biteright.events

import java.util.UUID

sealed class EditProductScreenEvent {
    data class LoadDetails(val productId: UUID) : EditProductScreenEvent()
}