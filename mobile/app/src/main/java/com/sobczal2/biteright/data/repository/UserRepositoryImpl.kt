package com.sobczal2.biteright.data.repository

import android.content.Context
import com.auth0.android.Auth0
import com.auth0.android.authentication.AuthenticationException
import com.auth0.android.callback.Callback
import com.auth0.android.provider.AuthCallback
import com.auth0.android.provider.AuthProvider
import com.auth0.android.provider.WebAuthProvider
import com.auth0.android.result.Credentials
import com.sobczal2.biteright.R
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.withContext
import javax.inject.Inject

class UserRepositoryImpl @Inject constructor(
) : UserRepository {
}