package com.sobczal2.biteright

import android.content.Context
import com.auth0.android.Auth0
import com.auth0.android.authentication.AuthenticationAPIClient
import com.auth0.android.authentication.AuthenticationException
import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.authentication.storage.CredentialsManagerException
import com.auth0.android.authentication.storage.SharedPreferencesStorage
import com.auth0.android.callback.Callback
import com.auth0.android.provider.WebAuthProvider
import com.auth0.android.result.Credentials

class AuthManager(
    private val context: Context
) {
    private val auth0 = Auth0(
        context.getString(R.string.com_auth0_client_id),
        context.getString(R.string.com_auth0_domain)
    )

    val credentialsManager = CredentialsManager(
        AuthenticationAPIClient(auth0),
        SharedPreferencesStorage(context)
    )

    val isLoggedIn: Boolean
        get() = credentialsManager.hasValidCredentials()

    fun login(
        onSuccess: (credentials: Credentials) -> Unit,
        onFailure: (errorStringId: Int) -> Unit
    ) {
        WebAuthProvider
            .login(auth0)
            .withScheme(context.getString(R.string.com_auth0_scheme))
            .start(context, object : Callback<Credentials, AuthenticationException> {
                override fun onFailure(error: AuthenticationException) {
                    when (error.getDescription()) {
                        "email_not_verified" -> onFailure(R.string.verify_your_email)
                        else -> onFailure(R.string.unknown_error)
                    }
                }

                override fun onSuccess(result: Credentials) {
                    onSuccess(result)
                    credentialsManager.saveCredentials(result)
                }
            })
    }

    fun logout() {
        credentialsManager.clearCredentials()
    }
}