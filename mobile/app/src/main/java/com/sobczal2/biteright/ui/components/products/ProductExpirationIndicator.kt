package com.sobczal2.biteright.ui.components.products

import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import com.sobczal2.biteright.util.humanizePeriod
import java.time.LocalDate
import java.time.Period

@Composable
fun ProductExpirationIndicator(
    expirationDate: LocalDate
) {

    if (expirationDate.isBefore(LocalDate.now())) {
        Text(
            text = "${stringResource(id = R.string.expired_for)} ${humanizePeriod(period = Period.between(expirationDate, LocalDate.now()))}",
            style = MaterialTheme.typography.labelMedium,
            color = MaterialTheme.colorScheme.error
        )
    } else {
        Text(
            text = "${stringResource(id = R.string.expires_in)} ${humanizePeriod(period = Period.between(LocalDate.now(), expirationDate))}",
            style = MaterialTheme.typography.labelMedium,
        )

    }
}