package com.sobczal2.biteright.events

import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.ui.components.products.FormMaxAmountWithUnit
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency

sealed class CreateProductScreenEvent {
    data class OnSubmitClick(val onSuccess: () -> Unit) : CreateProductScreenEvent()
    data class OnNameChange(val value: String) : CreateProductScreenEvent()
    data class OnDescriptionChange(val value: String) : CreateProductScreenEvent()
    data class OnPriceChange(val value: FormPriceWithCurrency) : CreateProductScreenEvent()
    data class OnExpirationDateChange(val value: ExpirationDate) : CreateProductScreenEvent()
    data class OnCategoryChange(val value: CategoryDto) : CreateProductScreenEvent()
    data class OnAmountChange(val value: FormMaxAmountWithUnit) : CreateProductScreenEvent()
}