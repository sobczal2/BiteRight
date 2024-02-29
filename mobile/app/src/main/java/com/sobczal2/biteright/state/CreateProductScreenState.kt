package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.ui.components.products.MaxAmountFormFieldState
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.components.categories.CategoryFormFieldState
import com.sobczal2.biteright.ui.components.products.ExpirationDateFormFieldState
import com.sobczal2.biteright.ui.components.products.FormMaxAmountWithUnit
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import com.sobczal2.biteright.ui.components.products.PriceFormFieldState

data class CreateProductScreenState(
    val nameFieldState: TextFormFieldState = TextFormFieldState(),
    val descriptionFieldState: TextFormFieldState = TextFormFieldState(),
    val priceFieldState: PriceFormFieldState = PriceFormFieldState(FormPriceWithCurrency.Empty),
    val expirationDateFieldState: ExpirationDateFormFieldState = ExpirationDateFormFieldState(),
    val categoryFieldState: CategoryFormFieldState = CategoryFormFieldState(CategoryDto.Empty),
    val maxAmountFieldState: MaxAmountFormFieldState = MaxAmountFormFieldState(FormMaxAmountWithUnit.Empty),
    val formSubmitting: Boolean = false,
    val imageRequestBuilder: ImageRequest.Builder? = null,
    val startingCategories: PaginatedList<CategoryDto>? = null,
    val startingCurrencies: PaginatedList<CurrencyDto>? = null,
    val startingUnits: PaginatedList<UnitDto>? = null,
    override val ongoingLoadingActions: Set<String> = emptySet(),
    override val globalError: String? = null,
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}