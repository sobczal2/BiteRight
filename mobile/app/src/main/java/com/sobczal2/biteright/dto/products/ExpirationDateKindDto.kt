package com.sobczal2.biteright.dto.products

import com.sobczal2.biteright.R

enum class ExpirationDateKindDto(val value: Int) {
    Unknown(0),
    Infinite(1),
    BestBefore(2),
    UseBy(3);

    fun toLocalizedResourceID() = when (this) {
        Unknown -> R.string.unknown
        Infinite -> R.string.infinite
        BestBefore -> R.string.best_before
        UseBy -> R.string.use_by
    }

    fun shouldIncludeDate() = when (this) {
        Unknown -> false
        Infinite -> false
        BestBefore -> true
        UseBy -> true
    }
}