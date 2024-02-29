package com.sobczal2.biteright.viewmodels

import android.content.Context
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import arrow.core.Either
import arrow.core.left
import arrow.core.right
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.events.WelcomeScreenEvent
import com.sobczal2.biteright.state.WelcomeScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import dagger.hilt.android.qualifiers.ActivityContext
import kotlinx.coroutines.CompletableDeferred
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class WelcomeViewModel @Inject constructor(
    private val authManager: AuthManager
) : ViewModel() {
    private val _state = MutableStateFlow(WelcomeScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<WelcomeScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            events.collect { event ->
                handleEvent(event)
            }
        }
    }

    fun sendEvent(event: WelcomeScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: WelcomeScreenEvent) {
        when (event) {
            is WelcomeScreenEvent.OnGetStartedClick -> onGetStartedClick(
                context = event.context,
                onSuccess = event.onSuccess
            )
        }
    }

    private fun onGetStartedClick(
        @ActivityContext context: Context,
        onSuccess: () -> Unit
    ) {
        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions + WelcomeViewModel::onGetStartedClick.name,
            )
        }
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
                            globalError = context.getString(errorStringId)
                        )
                    }
                }
            )
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - WelcomeViewModel::onGetStartedClick.name,
                )
            }
        }
    }
}