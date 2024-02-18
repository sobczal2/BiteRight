package com.sobczal2.biteright.viewmodels

import android.util.Log
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.products.PriceWithCurrency
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.delay
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
            launch { events.collect { event -> handleEvent(event) } }
            launch { fetchCurrencies() }
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
                onNameChange(event.value)
            }

            is CreateProductScreenEvent.OnDescriptionChange -> {
                onDescriptionChange(event.value)
            }

            is CreateProductScreenEvent.OnSubmitClick -> {
                viewModelScope.launch {
                    submitForm()
                }
            }

            is CreateProductScreenEvent.OnPriceChange -> {
                onPriceChange(event.value)
            }
        }
    }

    private fun onNameChange(value: String) {
        _state.update {
            it.copy(
                nameFieldState = it.nameFieldState.copy(
                    value = value
                )
            )
        }
    }

    private fun onDescriptionChange(value: String) {
        _state.update {
            it.copy(
                descriptionFieldState = it.descriptionFieldState.copy(
                    value = value
                )
            )
        }
    }

    private fun onPriceChange(value: PriceWithCurrency) {
        _state.update {
            it.copy(
                priceFieldState = it.priceFieldState.copy(
                    value = value
                )
            )
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

    private suspend fun submitForm() {
        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        _state.update {
            it.copy(
                priceFieldState = it.priceFieldState.copy(
                    priceError = if (it.priceFieldState.value.price == null) "Price is required" else null,
                    currencyError = if (it.priceFieldState.value.currency == null) "Currency is required" else null
                )
            )
        }

        Log.d("XDD", "form data: ${state.value.nameFieldState.value}, ${state.value.descriptionFieldState.value}, ${state.value.priceFieldState.value}");
        delay(1000)

        _state.update {
            it.copy(
                formSubmitting = false
            )
        }
    }
}