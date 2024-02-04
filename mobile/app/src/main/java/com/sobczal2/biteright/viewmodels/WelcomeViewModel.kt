package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import arrow.core.Either
import com.auth0.android.Auth0
import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.provider.WebAuthProvider
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.util.ResourceIdOrString
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

class WelcomeViewModel @Inject constructor() : ViewModel() {
    private val _state = MutableStateFlow(WelcomeScreenState())
    val state = _state.asStateFlow()
    fun onGetStartedClick(
        login: () -> Either<Unit, Int>,
    ) {
        _state.update { it.copy(loading = true) }
        login().fold(
            { _state.update { it.copy(loading = false) } },
            { errorStringId ->
                _state.update {
                    it.copy(
                        loading = false,
                        error = ResourceIdOrString(
                            resourceId = errorStringId
                        )
                    )
                }
            }
        )
    }
}