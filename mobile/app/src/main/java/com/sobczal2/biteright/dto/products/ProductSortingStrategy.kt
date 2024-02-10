package com.sobczal2.biteright.dto.products

enum class ProductSortingStrategy(val value: Int) {
    NameAsc(0),
    NameDesc(1),
    ExpirationDateAsc(2),
    ExpirationDateDesc(3),
    AddedDateTimeAsc(4),
    AddedDateTimeDesc(5),
    ConsumptionAsc(6),
    ConsumptionDesc(7);

    companion object {
        fun fromInt(value: Int) = entries.firstOrNull { it.value == value }
    }
}
