package com.sobczal2.biteright.events

import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import java.util.UUID

sealed class EditProductScreenEvent {
    data class LoadDetails(val productId: UUID) : EditProductScreenEvent()
    data class OnNameChange(val value: String) : EditProductScreenEvent()
    data class OnCategoryChange(val value: CategoryDto) : EditProductScreenEvent()
    data class OnExpirationDateChange(val value: ExpirationDate) : EditProductScreenEvent()
    data class OnPriceChange(val value: FormPriceWithCurrency) : EditProductScreenEvent()
    data class OnDescriptionChange(val value: String) : EditProductScreenEvent()
}