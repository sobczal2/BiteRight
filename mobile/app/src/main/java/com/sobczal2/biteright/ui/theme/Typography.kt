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

private val caveatFamily = FontFamily(
    Font(R.font.caveat_bold, FontWeight.Bold),
    Font(R.font.caveat_regular, FontWeight.Normal),
    Font(R.font.caveat_medium, FontWeight.Medium),
    Font(R.font.caveat_semibold, FontWeight.SemiBold),
)

private val portable1980Family = FontFamily(
    Font(R.font.portable_1980, FontWeight.Normal),
)

private val shlopFamily = FontFamily(
    Font(R.font.shlop, FontWeight.Normal),
)

private val ghostmeatFamily = FontFamily(
    Font(R.font.ghostmeat, FontWeight.Normal),
)

private val currentFontFamily = averiaLibreFamily;

private val defaultTypography = Typography()
val Typography = Typography(
    displayLarge = defaultTypography.displayLarge.copy(fontFamily = currentFontFamily),
    displayMedium = defaultTypography.displayMedium.copy(fontFamily = currentFontFamily),
    displaySmall = defaultTypography.displaySmall.copy(fontFamily = currentFontFamily),

    headlineLarge = defaultTypography.headlineLarge.copy(fontFamily = currentFontFamily),
    headlineMedium = defaultTypography.headlineMedium.copy(fontFamily = currentFontFamily),
    headlineSmall = defaultTypography.headlineSmall.copy(fontFamily = currentFontFamily),

    titleLarge = defaultTypography.titleLarge.copy(fontFamily = currentFontFamily),
    titleMedium = defaultTypography.titleMedium.copy(fontFamily = currentFontFamily),
    titleSmall = defaultTypography.titleSmall.copy(fontFamily = currentFontFamily),

    bodyLarge = defaultTypography.bodyLarge.copy(fontFamily = currentFontFamily),
    bodyMedium = defaultTypography.bodyMedium.copy(fontFamily = currentFontFamily),
    bodySmall = defaultTypography.bodySmall.copy(fontFamily = currentFontFamily),

    labelLarge = defaultTypography.labelLarge.copy(fontFamily = currentFontFamily),
    labelMedium = defaultTypography.labelMedium.copy(fontFamily = currentFontFamily),
    labelSmall = defaultTypography.labelSmall.copy(fontFamily = currentFontFamily)
)