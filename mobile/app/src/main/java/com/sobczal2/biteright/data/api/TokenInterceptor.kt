package com.sobczal2.biteright.data.api

import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.authentication.storage.CredentialsManagerException
import com.auth0.android.callback.Callback
import com.auth0.android.result.Credentials
import com.sobczal2.biteright.AuthManager
import kotlinx.coroutines.runBlocking
import okhttp3.Interceptor
import okhttp3.Response
import kotlin.coroutines.suspendCoroutine

class TokenInterceptor(private val authManager: AuthManager) : Interceptor {
    override fun intercept(chain: Interceptor.Chain): Response {
        val original = chain.request()

        val jwt = runBlocking { getJwt() }
        if (jwt != null) {
            val requestBuilder = original.newBuilder()
                .addHeader("Authorization", "Bearer $jwt")

            val request = requestBuilder.build()
            return chain.proceed(request)
        }

        return chain.proceed(original)
    }

    private suspend fun getJwt(): String? {
        return suspendCoroutine { continuation ->
            authManager.credentialsManager.getCredentials(object :
                Callback<Credentials, CredentialsManagerException> {
                override fun onFailure(error: CredentialsManagerException) {
                    continuation.resumeWith(Result.success(null))
                }

                override fun onSuccess(result: Credentials) {
                    continuation.resumeWith(Result.success(result.accessToken))
                }
            })
        }
    }
}