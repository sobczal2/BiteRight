package com.sobczal2.biteright.viewmodel.screens

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import javax.inject.Inject

@HiltViewModel
class WelcomeViewModel @Inject constructor(
) : ViewModel() {
    private val _state = MutableStateFlow(WelcomeState())
    val state = _state.asStateFlow();

    fun setError(error: String) {
        _state.value = _state.value.copy(error = error)
    }

    fun clearError() {
        _state.value = _state.value.copy(error = null)
    }
}

data class WelcomeState(
    val error: String? = null,
)