package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import arrow.core.right
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.StartScreenState
import com.sobczal2.biteright.state.WelcomeScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class StartViewModel @Inject constructor(
    private val userRepository: UserRepository
) : ViewModel() {
    private val _state = MutableStateFlow(StartScreenState())
    val state = _state.asStateFlow()

    suspend fun isOnboarded(): Boolean {
        val meResult = userRepository.me()

        return meResult.fold(
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
                            error = repositoryError.message
                        )
                    }
                    false
                }
            }
        )
    }

    fun onUsernameChange(username: String) {
        _state.update {
            it.copy(
                username = username,
                error = null
            )
        }
    }

    fun onNextClick(onSuccess: () -> Unit) {
        _state.update {
            it.copy(
                loading = true
            )
        }

        val onboardRequest = OnboardRequest(
            username = state.value.username
        )

        viewModelScope.launch {
            val onboardResult = userRepository.onboard(onboardRequest)
            onboardResult.fold(
                {
                    onSuccess()
                },
                { repositoryError ->
                    _state.update {
                        it.copy(
                            loading = false,
                            error = repositoryError.message,
                        )
                    }
                }
            )
        }

    }
}