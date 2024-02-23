package com.sobczal2.biteright.util

import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import java.time.LocalDate

fun LocalDate.toEpochMillis(): Long = toEpochDay() * 24 * 60 * 60 * 1000

fun Long.toLocalDate(): LocalDate = LocalDate.ofEpochDay(this / (24 * 60 * 60 * 1000))

@Composable
fun LocalDate.toExpirationString(
    now: LocalDate = LocalDate.now()
): String {
    return when {
        this < now -> stringResource(
            id = R.string.expired_for,
            between(this, now).humanize()
        )

        this == now -> stringResource(id = R.string.expires_today)
        else -> stringResource(id = R.string.expires_in, between(now, this).humanize())
    }
}