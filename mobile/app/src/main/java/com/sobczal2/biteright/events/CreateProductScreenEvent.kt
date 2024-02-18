package com.sobczal2.biteright.events

import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.PriceWithCurrency

sealed class CreateProductScreenEvent {
    data object OnSubmitClick : CreateProductScreenEvent()
    data class OnNameChange(val value: String) : CreateProductScreenEvent()
    data class OnDescriptionChange(val value: String) : CreateProductScreenEvent()
    data class OnPriceChange(val value: PriceWithCurrency) : CreateProductScreenEvent()
    data class OnExpirationDateChange(val value: ExpirationDate) : CreateProductScreenEvent()
}