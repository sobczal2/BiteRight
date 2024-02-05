package com.sobczal2.biteright.data.api

import com.sobczal2.biteright.AuthManager
import okhttp3.Interceptor
import okhttp3.Response
import javax.inject.Inject

class UnauthorizedInterceptor @Inject constructor(
    private val authManager: AuthManager
) : Interceptor {
    override fun intercept(chain: Interceptor.Chain): Response {
        val next = chain.proceed(chain.request())
        if (next.code == 401) {
            authManager.logout()
        }
        return next
    }
}