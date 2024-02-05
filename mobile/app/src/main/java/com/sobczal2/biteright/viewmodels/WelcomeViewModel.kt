package com.sobczal2.biteright.viewmodels

import android.content.Context
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.util.ResourceIdOrString
import dagger.hilt.android.lifecycle.HiltViewModel
import dagger.hilt.android.qualifiers.ActivityContext
import kotlinx.coroutines.CompletableDeferred
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class WelcomeViewModel @Inject constructor(
    private val authManager: AuthManager
) : ViewModel() {
    private val _state = MutableStateFlow(WelcomeScreenState())
    val state = _state.asStateFlow()
    fun onGetStartedClick(
        @ActivityContext context: Context,
        onSuccess: () -> Unit
    ) {
        _state.update { it.copy(loading = true) }
        viewModelScope.launch {
            val loginResult = CompletableDeferred<Either<Unit, Int>>()
            authManager.login(
                onSuccess = {
                    loginResult.complete(Unit.left())
                },
                onFailure = { errorStringId ->
                    loginResult.complete(errorStringId.right())
                },
                activityContext = context
            )
            val result = loginResult.await()
            result.fold(
                { onSuccess() },
                { errorStringId ->
                    _state.update {
                        it.copy(
                            loading = false,
                            generalError = ResourceIdOrString(
                                resourceId = errorStringId
                            )
                        )
                    }
                }
            )
            _state.update {
                it.copy(loading = false)
            }
        }
    }
}