package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.state.CreateProductScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class CreateProductViewModel @Inject constructor(
    private val currencyRepository: CurrencyRepository
) : ViewModel() {
    private val _state = MutableStateFlow(CreateProductScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<CreateProductScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            events.collect { event ->
                handleEvent(event)
            }
            fetchCurrencies()
        }
    }

    fun sendEvent(event: CreateProductScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: CreateProductScreenEvent) {
        when (event) {
            is CreateProductScreenEvent.OnNameChange -> {
                _state.update {
                    it.copy(
                        nameFieldState = it.nameFieldState.copy(
                            value = event.value
                        )
                    )
                }
            }
            is CreateProductScreenEvent.OnDescriptionChange -> {
                _state.update {
                    it.copy(
                        descriptionFieldState = it.descriptionFieldState.copy(
                            value = event.value
                        )
                    )
                }
            }
        }
    }

    private suspend fun fetchCurrencies() {
        _state.update {
            it.copy(
                globalLoading = true
            )
        }

        val currenciesResult = currencyRepository.list()

        currenciesResult.fold(
            { currencies ->
                _state.update {
                    it.copy(
                        priceFieldState = it.priceFieldState.copy(
                            availableCurrencies = currencies
                        )
                    )
                }
            },
            { repositoryError ->
                _state.value = state.value.copy(
                    globalError = repositoryError.message
                )
            }
        )

        _state.value = state.value.copy(
            globalLoading = false
        )
    }
}