package com.sobczal2.biteright

import android.content.Context
import android.util.Log
import com.auth0.android.Auth0
import com.auth0.android.authentication.AuthenticationAPIClient
import com.auth0.android.authentication.AuthenticationException
import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.authentication.storage.SharedPreferencesStorage
import com.auth0.android.callback.Callback
import com.auth0.android.provider.WebAuthProvider
import com.auth0.android.result.Credentials
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class AuthManager(
    @ApplicationContext val context: Context
) {
    private var onLogoutCallback: (() -> Unit)? = null

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
        onFailure: (errorStringId: Int) -> Unit,
        activityContext: Context
    ) {
        WebAuthProvider
            .login(auth0)
            .withScheme(activityContext.getString(R.string.com_auth0_scheme))
            .start(activityContext, object : Callback<Credentials, AuthenticationException> {
                override fun onFailure(error: AuthenticationException) {
                    when (error.getCode()) {
                        "access_denied" -> {
                            when (error.getDescription()) {
                                "email_not_verified" -> onFailure(R.string.verify_your_email)
                                else -> onFailure(R.string.unknown_error)
                            }
                        }

                        "a0.authentication_canceled" -> onFailure(R.string.login_canceled)
                        else -> onFailure(R.string.unknown_error)
                    }
                }

                override fun onSuccess(result: Credentials) {
                    Log.d("AuthManager", "Login successful: $result")
                    onSuccess(result)
                    credentialsManager.saveCredentials(result)
                }
            })
    }

    fun logout() {
        credentialsManager.clearCredentials()
        CoroutineScope(Dispatchers.Main).launch {
            onLogoutCallback?.invoke()
        }
    }

    fun subscribeToLogoutEvent(onLogout: () -> Unit) {
        this.onLogoutCallback = onLogout
    }
}