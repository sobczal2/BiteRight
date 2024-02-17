//package com.sobczal2.biteright.state
//
//import com.sobczal2.biteright.dto.currencies.CurrencyDto
//import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
//import com.sobczal2.biteright.util.ResourceIdOrString
//import java.time.LocalDate
//
//data class CreateProductScreenState(
//    val name: String = "",
//    val nameError: ResourceIdOrString? = null,
//    val description: String = "",
//    val descriptionError: ResourceIdOrString? = null,
//    val price: Double? = null,
//    val priceError: ResourceIdOrString? = null,
//    val currencyDto: CurrencyDto? = null,
//    val availableCurrencyDtos: List<CurrencyDto> = emptyList(),
//    val expirationDateDialogOpen: Boolean = false,
//    val expirationDateKind: ExpirationDateKindDto? = null,
//    val expirationDateValue: LocalDate? = null,
//    val expirationDateError: ResourceIdOrString? = null,
//) : ScreenStateBase()
