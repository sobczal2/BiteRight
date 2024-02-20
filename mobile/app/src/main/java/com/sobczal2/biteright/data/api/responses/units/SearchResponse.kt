package com.sobczal2.biteright.data.api.responses.units

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.units.UnitDto

data class SearchResponse(
    @SerializedName("units") val units: PaginatedList<UnitDto>
)