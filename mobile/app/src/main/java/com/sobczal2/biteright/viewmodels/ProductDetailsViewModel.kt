package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.events.ProductDetailsScreenEvent
import com.sobczal2.biteright.events.WelcomeScreenEvent
import com.sobczal2.biteright.state.ProductDetailsScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ProductDetailsViewModel @Inject constructor(

) : ViewModel() {
    private val _state = MutableStateFlow(ProductDetailsScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<ProductDetailsScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            events.collect { event ->
                handleEvent(event)
            }
        }
    }

    fun sendEvent(event: ProductDetailsScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: ProductDetailsScreenEvent) {
        when (event) {
            else -> {}
        }
    }
}