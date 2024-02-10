package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import kotlin.math.roundToInt

@Composable
fun ProductConsumptionIndicator(
    consumption: Double
) {
    Box(
        modifier = Modifier
            .padding(8.dp)
            .width(48.dp)
    ) {
        CircularProgressIndicator(
            progress = { consumption.toFloat() },
            modifier = Modifier.size(48.dp),
            trackColor = MaterialTheme.colorScheme.onBackground,
            color = getConsumptionColor(consumption)
        )
        Text(
            text = "${((1 - consumption) * 100).roundToInt()}%",
            modifier = Modifier.align(Alignment.Center),
            style = MaterialTheme.typography.labelMedium
                .copy(textAlign = TextAlign.Center)
        )
    }
}

private fun getConsumptionColor(consumption: Double): Color {
    return when (consumption) {
        in 0.0..0.25 -> Color.Green
        in 0.25..0.5 -> Color.Yellow
        in 0.5..0.75 -> Color(0xFFFFA500)
        in 0.75..1.0 -> Color.Red
        else -> Color.Black
    }
}