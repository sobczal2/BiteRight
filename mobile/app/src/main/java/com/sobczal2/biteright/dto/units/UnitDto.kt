package com.sobczal2.biteright.dto.units

import java.util.UUID

data class UnitDto(
    val id: UUID,
    val name: String,
    val abbreviation: String,
    val unitSystem: UnitSystemDto
) {
    companion object {
        val Empty = UnitDto(
            id = UUID.randomUUID(),
            name = "",
            abbreviation = "",
            unitSystem = UnitSystemDto.Empty
        )
    }
}