package com.sobczal2.biteright.viewmodel.screens

import androidx.lifecycle.ViewModel
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import javax.inject.Inject

@HiltViewModel
class OnboardViewModel @Inject constructor(
) : ViewModel() {
    private val _state = MutableStateFlow(OnboardState())
    val state = _state.asStateFlow()

    fun setUsername(username: String) {
        _state.value = _state.value.copy(username = username)
    }
}

data class OnboardState(
    val username: String = "",
)