package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.data.api.requests.OnboardRequest
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.StartScreenState
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

    fun onUsernameChange(username: String) {
        _state.update {
            it.copy(
                username = username,
                error = null
            )
        }

        validateUsername(username).let { isValid ->
            _state.update {
                it.copy(
                    canContinue = isValid
                )
            }
        }
    }

    private fun validateUsername(username: String): Boolean {
        return username.length in 3..30
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