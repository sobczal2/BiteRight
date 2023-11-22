package com.sobczal2.biteright.viewmodel.screens

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class SignInViewModel @Inject constructor(
    private val userRepository: UserRepository
) : ViewModel() {
    private val _state = MutableStateFlow(SignInState())
    val state = _state.asStateFlow()

    fun onEmailChanged(email: String) {
        _state.value = _state.value.copy(email = email)
    }

    fun onPasswordChanged(password: String) {
        _state.value = _state.value.copy(password = password)
    }

    fun onSignInClicked() {
        _state.value = _state.value.copy(loading = true, submitEnabled = false)

        viewModelScope.launch {
            val result = userRepository.signIn(_state.value.email, _state.value.password)
            if (result.isSuccess) {
                _state.value = _state.value.copy(loading = false, submitEnabled = true, error = "Success")
            } else {
                _state.value = _state.value.copy(loading = false, submitEnabled = true, error = result.exceptionOrNull()?.message ?: "Unknown error")
            }
        }
    }
}

data class SignInState(
    val email: String = "",
    val password: String = "",
    val loading: Boolean = false,
    val submitEnabled: Boolean = true,
    val error: String = ""
)