package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.events.EditProfileScreenEvent
import com.sobczal2.biteright.state.EditProfileScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class EditProfileViewModel @Inject constructor(

) : ViewModel() {
    private val _state = MutableStateFlow(EditProfileScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<EditProfileScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            _events.receiveAsFlow().collect { event -> handleEvent(event) }
        }
    }

    fun sendEvent(event: EditProfileScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: EditProfileScreenEvent) {
        when (event) {
            else -> {}
        }
    }
}