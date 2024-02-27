package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.data.api.requests.products.GetDetailsRequest
import com.sobczal2.biteright.events.EditProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.state.EditProductScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class EditProductViewModel @Inject constructor(
    private val productRepository: ProductRepository
) : ViewModel() {
    private val _state = MutableStateFlow(EditProductScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<EditProductScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            events.collect { event ->
                handleEvent(event)
            }
        }
    }

    fun sendEvent(event: EditProductScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: EditProductScreenEvent) {
        when (event) {
            is EditProductScreenEvent.LoadDetails -> {
                loadDetails(event.productId)
            }
        }
    }

    private fun loadDetails(productId: UUID) {
        viewModelScope.launch {
            val getDetailsResult = productRepository.getDetails(
                GetDetailsRequest(productId)
            )

            getDetailsResult.fold(
                { response ->
                    _state.update {
                        it.copy(
                            product = response.product
                        )
                    }
                },
                { error ->
                    _state.update {
                        it.copy(
                            globalError = error.message,
                        )
                    }
                }
            )

            _state.value = _state.value.copy(
                globalLoading = false
            )
        }
    }
}