package com.sobczal2.biteright.ui.theme

import androidx.compose.material3.Typography
import androidx.compose.ui.text.font.Font
import androidx.compose.ui.text.font.FontFamily
import androidx.compose.ui.text.font.FontStyle
import androidx.compose.ui.text.font.FontWeight
import com.sobczal2.biteright.R

private val averiaLibreFamily = FontFamily(
    Font(R.font.averialibre_bold, FontWeight.Bold),
    Font(R.font.averialibre_bolditalic, FontWeight.Bold, FontStyle.Italic),
    Font(R.font.averialibre_italic, FontWeight.Normal, FontStyle.Italic),
    Font(R.font.averialibre_light, FontWeight.Light),
    Font(R.font.averialibre_lightitalic, FontWeight.Light, FontStyle.Italic),
    Font(R.font.averialibre_regular, FontWeight.Normal),
)

private val defaultTypography = Typography()
val Typography = Typography(
    displayLarge = defaultTypography.displayLarge.copy(fontFamily = averiaLibreFamily),
    displayMedium = defaultTypography.displayMedium.copy(fontFamily = averiaLibreFamily),
    displaySmall = defaultTypography.displaySmall.copy(fontFamily = averiaLibreFamily),

    headlineLarge = defaultTypography.headlineLarge.copy(fontFamily = averiaLibreFamily),
    headlineMedium = defaultTypography.headlineMedium.copy(fontFamily = averiaLibreFamily),
    headlineSmall = defaultTypography.headlineSmall.copy(fontFamily = averiaLibreFamily),

    titleLarge = defaultTypography.titleLarge.copy(fontFamily = averiaLibreFamily),
    titleMedium = defaultTypography.titleMedium.copy(fontFamily = averiaLibreFamily),
    titleSmall = defaultTypography.titleSmall.copy(fontFamily = averiaLibreFamily),

    bodyLarge = defaultTypography.bodyLarge.copy(fontFamily = averiaLibreFamily),
    bodyMedium = defaultTypography.bodyMedium.copy(fontFamily = averiaLibreFamily),
    bodySmall = defaultTypography.bodySmall.copy(fontFamily = averiaLibreFamily),

    labelLarge = defaultTypography.labelLarge.copy(fontFamily = averiaLibreFamily),
    labelMedium = defaultTypography.labelMedium.copy(fontFamily = averiaLibreFamily),
    labelSmall = defaultTypography.labelSmall.copy(fontFamily = averiaLibreFamily)
)