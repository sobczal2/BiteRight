package com.sobczal2.biteright.dto.units


enum class UnitSystemDto(val value: Int) {
    Metric(1),
    Imperial(2),
    Both(3);

    companion object {
        val Empty = Metric
    }
}