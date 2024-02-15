package com.sobczal2.biteright.data.api.responses.currencies

import com.sobczal2.biteright.dto.currencies.CurrencyDto

data class ListResponse(
    val currencies: List<CurrencyDto>
)
