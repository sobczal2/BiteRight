package com.sobczal2.biteright.state

import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.products.PriceFormFieldState

data class CreateProductScreenState(
    val nameFieldState: TextFormFieldState = TextFormFieldState(),
    val descriptionFieldState: TextFormFieldState = TextFormFieldState(),
    val priceFieldState: PriceFormFieldState = PriceFormFieldState(availableCurrencies = emptyList()),
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenStateBase
