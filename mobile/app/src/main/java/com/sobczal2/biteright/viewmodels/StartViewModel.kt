package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.util.ResourceIdOrString
import com.sobczal2.biteright.util.asResourceIdOrString
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.TimeZone
import javax.inject.Inject

@HiltViewModel
class StartViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val authManager: AuthManager
) : ViewModel() {
    private val _state = MutableStateFlow(StartScreenState())
    val state = _state.asStateFlow()

    fun onUsernameChange(username: String) {
        _state.update {
            it.copy(
                username = username,
            )
        }

        clearErrors()
    }

    private fun clearErrors() {
        _state.update {
            it.copy(
                usernameError = null,
                generalError = null
            )
        }
    }

    private fun triggerValidation() {
        if (_state.value.username.length !in 3..30) {
            _state.update {
                it.copy(
                    usernameError = ResourceIdOrString(R.string.username_length_error)
                )
            }
            return
        }
        if (!Regex("^[\\p{L}\\p{Nd}-_]*\$").matches(_state.value.username)) {
            _state.update {
                it.copy(
                    usernameError = ResourceIdOrString(R.string.username_invalid_characters_error)
                )
            }
            return
        }
    }

    fun onNextClick(onSuccess: () -> Unit) {
        triggerValidation()

        if (_state.value.usernameError != null) {
            return
        }

        _state.update {
            it.copy(
                loading = true
            )
        }

        val onboardRequest = OnboardRequest(
            username = state.value.username,
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
                                usernameError = repositoryError.apiErrors["username"]!!.first()
                                    .asResourceIdOrString()
                            )
                        }
                    } else {
                        _state.update {
                            it.copy(
                                generalError = repositoryError.message
                            )
                        }
                    }

                    _state.update {
                        it.copy(
                            loading = false,
                        )
                    }
                }
            )
        }

    }

    suspend fun isOnboarded(): Boolean {
        _state.update {
            it.copy(
                loading = true
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
                            loading = false,
                            generalError = repositoryError.message
                        )
                    }
                    false
                }
            }
        )

        _state.update {
            it.copy(
                loading = false
            )
        }

        return isOnboarded
    }
}