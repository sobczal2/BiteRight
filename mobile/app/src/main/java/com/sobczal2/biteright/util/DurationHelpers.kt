package com.sobczal2.biteright.util

import androidx.compose.runtime.Composable
import androidx.compose.ui.res.pluralStringResource
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import java.time.LocalDate
import kotlin.time.Duration
import kotlin.time.Duration.Companion.days

@Composable
fun Duration.humanize(): String {
    val humanizedText = when {
        this.inWholeYears > 0 -> {
            val years = this.inWholeYears
            pluralStringResource(id = R.plurals.years_count, years.toInt(), years)
        }

        this.inWholeMonths > 0 -> {
            val months = this.inWholeMonths
            pluralStringResource(id = R.plurals.months_count, months.toInt(), months)
        }

        this.inWholeDays >= 14 -> {
            val weeks = this.inWholeWeeks
            pluralStringResource(id = R.plurals.weeks_count, weeks.toInt(), weeks)
        }

        this.inWholeDays > 0 -> {
            val days = this.inWholeDays
            pluralStringResource(id = R.plurals.days_count, days.toInt(), days)
        }

        else -> stringResource(id = R.string.today)
    }

    return humanizedText
}

fun between(start: LocalDate, end: LocalDate): Duration =
    (end.toEpochDay() - start.toEpochDay()).days

// approximation
val Duration.inWholeYears: Long
    get() = this.inWholeDays / 365

// approximation
val Duration.inWholeMonths: Long
    get() = this.inWholeDays / 30

val Duration.inWholeWeeks: Long
    get() = this.inWholeDays / 7