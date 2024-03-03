package com.sobczal2.biteright.viewmodels

import androidx.compose.material3.SnackbarHostState
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.events.HelpScreenEvent
import com.sobczal2.biteright.state.HelpScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class HelpViewModel @Inject constructor(

) : ViewModel() {

    lateinit var snackbarHostState: SnackbarHostState
    private val _state = MutableStateFlow(HelpScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<HelpScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch {
                events.collect { event ->
                    handleEvent(event)
                }
            }
        }
    }

    fun sendEvent(event: HelpScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: HelpScreenEvent) {
        when (event) {
            else -> {}
        }
    }
}