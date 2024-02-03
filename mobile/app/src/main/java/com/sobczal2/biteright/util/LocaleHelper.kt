package com.sobczal2.biteright.util

import android.content.Context
import android.content.res.Configuration
import java.util.Locale


object LocaleHelper {
    fun setLocale(context: Context, languageCode: String) {
        val locale = Locale(languageCode)
        Locale.setDefault(locale)

        val configuration = Configuration(context.resources.configuration)
        configuration.setLocale(locale)
        context.createConfigurationContext(configuration)
    }
}