package com.sobczal2.biteright.data.api.common

import androidx.compose.ui.text.intl.Locale
import okhttp3.Interceptor
import okhttp3.Response

class LanguageInterceptor : Interceptor {
    override fun intercept(chain: Interceptor.Chain): Response {
        val original = chain.request()

        val language = Locale.current.language;

        val requestBuilder = original.newBuilder()
            .addHeader("Accept-Language", language)

        val request = requestBuilder.build()
        return chain.proceed(request)
    }
}