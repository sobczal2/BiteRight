package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.state.WelcomeScreenState
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

class WelcomeViewModel @Inject constructor() : ViewModel() {
    private val _state = MutableStateFlow(WelcomeScreenState())
    val state = _state.asStateFlow()
    fun onGetStartedClick(
        onSuccess: () -> Unit,
    ) {
        _state.update { it.copy(loading = true) }
        viewModelScope.launch {
            delay(2000)
            _state.update { it.copy(loading = false) }
            _state.update { it.copy(error = "xd") }
        }
    }
}