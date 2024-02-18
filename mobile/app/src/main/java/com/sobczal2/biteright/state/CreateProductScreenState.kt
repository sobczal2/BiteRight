package com.sobczal2.biteright.state

import com.sobczal2.biteright.ui.components.amounts.AmountFormFieldState
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.categories.CategoryFormFieldState
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormFieldState
import com.sobczal2.biteright.ui.components.products.PriceFormFieldState

data class CreateProductScreenState(
    val nameFieldState: TextFormFieldState = TextFormFieldState(),
    val descriptionFieldState: TextFormFieldState = TextFormFieldState(),
    val priceFieldState: PriceFormFieldState = PriceFormFieldState(),
    val expirationDateFieldState: ExpirationDateFormFieldState = ExpirationDateFormFieldState(),
    val categoryFieldState: CategoryFormFieldState = CategoryFormFieldState(),
    val amountFormFieldState: AmountFormFieldState = AmountFormFieldState(),
    val formSubmitting: Boolean = false,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenStateBase