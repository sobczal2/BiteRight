package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.util.ResourceIdOrString

data class CreateProductScreenState(
    val name: String = "",
    val nameError: ResourceIdOrString? = null,
    val description: String = "",
    val descriptionError: ResourceIdOrString? = null,
    val price: Double? = null,
    val priceError: ResourceIdOrString? = null,
    val currencyDto: CurrencyDto? = null,
    val currencyDialogOpen: Boolean = false,
    val availableCurrencyDtos: List<CurrencyDto> = emptyList(),
    val loading: Boolean = false,
    val error: ResourceIdOrString? = null
)
