package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.state.CreateProductScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import javax.inject.Inject

@HiltViewModel
class CreateProductViewModel @Inject constructor(
    private val currencyRepository: CurrencyRepository
) : ViewModel() {
    private val _state = MutableStateFlow(CreateProductScreenState())
    val state = _state.asStateFlow()

    fun onNameChange(value: String) {
        _state.value = _state.value.copy(name = value)
    }

    fun onDescriptionChange(value: String) {
        _state.value = _state.value.copy(description = value)
    }

    fun onPriceChange(value: Double?) {
        _state.value = _state.value.copy(price = value)
    }

    fun onSelectCurrencyButtonClick() {
        _state.value = _state.value.copy(currencyDialogOpen = true)
    }

    fun closeCurrencyDialog() {
        _state.value = _state.value.copy(currencyDialogOpen = false)
    }

    fun onCurrencySelected(currency: CurrencyDto?) {
        _state.value = _state.value.copy(currencyDto = currency, currencyDialogOpen = false)
    }

    suspend fun init() {
        initCurrencies()
    }

    private suspend fun initCurrencies() {
        val listResult = currencyRepository.list()
        listResult.fold(
            { currencies ->
                _state.value = _state.value.copy(availableCurrencyDtos = currencies)
            },
            { error ->
                _state.value = _state.value.copy(error = error.message)
            }
        )
    }
}