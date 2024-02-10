package com.sobczal2.biteright.util

import androidx.compose.runtime.Composable
import androidx.compose.ui.res.pluralStringResource
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import java.time.Period

@Composable
fun humanizePeriod(period: Period): String {
    val humanizedText = when {
        period.years > 0 -> {
            val years = period.years
            pluralStringResource(id = R.plurals.years_count, years, years)
        }

        period.months > 0 -> {
            val months = period.months
            pluralStringResource(id = R.plurals.months_count, months, months)
        }

        period.days >= 14 -> {
            val weeks = period.days / 7
            pluralStringResource(id = R.plurals.weeks_count, weeks, weeks)
        }
        period.days > 0 -> {
            val days = period.days
            pluralStringResource(id = R.plurals.days_count, days, days)
        }
        else -> stringResource(id = R.string.today)
    }

    return humanizedText
}
