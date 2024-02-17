package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.events.StartScreenEvent
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.util.StringProvider
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.TimeZone
import javax.inject.Inject

@HiltViewModel
class StartViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val stringProvider: StringProvider
) : ViewModel() {
    private val _state = MutableStateFlow(StartScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<StartScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            events.collect { event ->
                handleEvent(event)
            }
        }
    }

    fun sendEvent(event: StartScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: StartScreenEvent) {
        when (event) {
            is StartScreenEvent.OnUsernameChange -> onUsernameChange(event.value)
            is StartScreenEvent.OnNextClick -> onNextClick(event.onSuccess)
        }
    }

    private fun onUsernameChange(username: String) {
        _state.update {
            it.copy(
                usernameFieldState = it.usernameFieldState.copy(
                    value = username
                )
            )
        }

        clearErrors()
    }

    private fun clearErrors() {
        _state.update {
            it.copy(
                usernameFieldState = it.usernameFieldState.copy(
                    error = null
                ),
                globalError = null
            )
        }
    }

    private fun validate() {
        if (_state.value.usernameFieldState.value.length !in 3..30) {
            _state.update {
                it.copy(
                    usernameFieldState = it.usernameFieldState.copy(
                        error = stringProvider.getString(R.string.username_length_error)
                    )
                )
            }
            return
        }
        if (!Regex("^[\\p{L}\\p{Nd}-_]*\$").matches(_state.value.usernameFieldState.value)) {
            _state.update {
                it.copy(
                    usernameFieldState = it.usernameFieldState.copy(
                        error = stringProvider.getString(R.string.username_invalid_characters_error)
                    )
                )
            }
            return
        }
    }

    private fun onNextClick(onSuccess: () -> Unit) {
        validate()

        if (_state.value.usernameFieldState.error != null) {
            return
        }

        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        val onboardRequest = OnboardRequest(
            username = state.value.usernameFieldState.value,
            timeZoneId = TimeZone.getDefault().id
        )

        viewModelScope.launch {
            val onboardResult = userRepository.onboard(onboardRequest)
            onboardResult.fold(
                {
                    onSuccess()
                },
                { repositoryError ->
                    if (repositoryError is ApiRepositoryError && repositoryError.apiErrors.any { it.key == "username" }) {
                        _state.update {
                            it.copy(
                                usernameFieldState = it.usernameFieldState.copy(
                                    error = repositoryError.apiErrors["username"]?.first()
                                )
                            )
                        }
                    } else {
                        _state.update {
                            it.copy(
                                globalError = repositoryError.message
                            )
                        }
                    }

                    _state.update {
                        it.copy(
                            formSubmitting = false
                        )
                    }
                }
            )
        }

    }

    suspend fun isOnboarded(): Boolean {
        _state.update {
            it.copy(
                globalLoading = true
            )
        }
        val meResult = userRepository.me()

        val isOnboarded = meResult.fold(
            {
                true
            },
            { repositoryError ->
                if (repositoryError is ApiRepositoryError && repositoryError.apiErrorCode == 404) {
                    false
                } else {
                    _state.update {
                        it.copy(
                            globalError = repositoryError.message
                        )
                    }
                    false
                }
            }
        )

        _state.update {
            it.copy(
                globalLoading = false
            )
        }

        return isOnboarded
    }
}