package com.sobczal2.biteright.data.api.responses.currencies

import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto

data class SearchResponse(
    val currencies: PaginatedList<CurrencyDto>
)
