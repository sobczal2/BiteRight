package com.sobczal2.biteright.core

import android.content.Context
import com.auth0.android.Auth0
import com.auth0.android.authentication.AuthenticationException
import com.auth0.android.callback.Callback
import com.auth0.android.provider.WebAuthProvider
import com.auth0.android.result.Credentials
import com.sobczal2.biteright.R

class Auth0Manager(private val context: Context) {
    private val auth0 = Auth0(
        context.getString(R.string.com_auth0_client_id),
        context.getString(R.string.com_auth0_domain)
    )

    fun login(
        onSuccess: (Credentials) -> Unit,
        onFailure: (String) -> Unit
    ) {
        WebAuthProvider
            .login(auth0)
            .withScheme(context.getString(R.string.com_auth0_scheme))
            .start(context, object : Callback<Credentials, AuthenticationException> {
                override fun onFailure(error: AuthenticationException) {
                    when(error.getDescription()) {
                        "email_not_verified" -> onFailure(context.getString(R.string.email_not_verified))
                        else -> onFailure(context.getString(R.string.unknown_error))
                    }
                }

                override fun onSuccess(result: Credentials) {
                    onSuccess(result)
                }
            })
    }

    fun logout() {

    }
}