package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.state.CreateProductScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import javax.inject.Inject

@HiltViewModel
class CreateProductViewModel @Inject constructor(

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
        _state.value = _state.value.copy(currencyString = "USD")
    }
}