package com.sobczal2.biteright.dto.products

import com.sobczal2.biteright.R

enum class ExpirationDateKindDto(val value: Int) {
    Unknown(0),
    Infinite(1),
    BestBefore(2),
    UseBy(3);

    companion object {
        fun toLocalizedResourceID(kind: ExpirationDateKindDto) = when (kind) {
            Unknown -> R.string.unknown
            Infinite -> R.string.infinite
            BestBefore -> R.string.best_before
            UseBy -> R.string.use_by
            else -> throw IllegalArgumentException("Unknown expiration date kind: $kind")
        }
    }
}