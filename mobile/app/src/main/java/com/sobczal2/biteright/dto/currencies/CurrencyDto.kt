package com.sobczal2.biteright.dto.currencies

import com.google.gson.annotations.SerializedName
import java.util.UUID

data class CurrencyDto(
    @SerializedName("id") val id: UUID,
    @SerializedName("name") val name: String,
    @SerializedName("symbol") val symbol: String,
    @SerializedName("code") val code: String
) {
    companion object {
        val Empty = CurrencyDto(
            id = UUID.randomUUID(),
            name = "",
            symbol = "",
            code = ""
        )
    }
}
