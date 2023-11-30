package com.sobczal2.biteright.viewmodel.screens

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import arrow.core.Either
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class SignUpViewModel @Inject constructor(
    private val userRepository: UserRepository
) : ViewModel() {
    private val _state = MutableStateFlow(SignUpState())
    val state = _state.asStateFlow()

    fun onNameChanged(name: String) {
        _state.value = _state.value.copy(name = name, error = _state.value.error.copy(name = ""))
    }

    fun onEmailChanged(email: String) {
        _state.value = _state.value.copy(email = email, error = _state.value.error.copy(email = ""))
    }

    fun onPasswordChanged(password: String) {
        _state.value = _state.value.copy(password = password, error = _state.value.error.copy(password = ""))
    }

    fun onSignUpClicked(onSuccess: () -> Unit) {
        _state.value = _state.value.copy(loading = true, submitEnabled = false)
        viewModelScope.launch {
            val result =
                userRepository.signUp(_state.value.email, _state.value.name, _state.value.password)
            when (result) {
                is Either.Left -> {
                    onSuccess()
                }
                is Either.Right -> {
                    val error = result.value
                    _state.value = _state.value.copy(
                        loading = false,
                        submitEnabled = true,
                        error = SignUpStateError(
                            email = error.errors["email"]?.firstOrNull() ?: "",
                            name = error.errors["name"]?.firstOrNull() ?: "",
                            password = error.errors["password"]?.firstOrNull() ?: "",
                            unknown = error.errors["unknown"]?.firstOrNull() ?: ""
                        )
                    )
                }

            }
        }
    }
}

data class SignUpState(
    val name: String = "",
    val email: String = "",
    val password: String = "",
    val loading: Boolean = false,
    val submitEnabled: Boolean = true,
    val error: SignUpStateError = SignUpStateError()
)

data class SignUpStateError(
    val name: String = "",
    val email: String = "",
    val password: String = "",
    val unknown: String = ""
)