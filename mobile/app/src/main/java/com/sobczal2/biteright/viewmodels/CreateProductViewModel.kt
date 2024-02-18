package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.common.amounts.FormAmountWithUnit
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest as CategoriesSearchRequest
import com.sobczal2.biteright.data.api.requests.units.SearchRequest as UnitsSearchRequest

@HiltViewModel
class CreateProductViewModel @Inject constructor(
    private val currencyRepository: CurrencyRepository,
    private val categoryRepository: CategoryRepository,
    private val unitRepository: UnitRepository,
    private val productRepository: ProductRepository
) : ViewModel() {
    private val _state = MutableStateFlow(CreateProductScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<CreateProductScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
            launch { fetchCurrencies() }
            launch { fetchCategories() }
            launch { fetchUnits() }
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
                    submitForm(event.onSuccess)
                }
            }

            is CreateProductScreenEvent.OnPriceChange -> {
                onPriceChange(event.value)
            }

            is CreateProductScreenEvent.OnExpirationDateChange -> {
                onExpirationDateChange(event.value)
            }

            is CreateProductScreenEvent.OnCategoryChange -> {
                onCategoryChange(event.value)
            }

            is CreateProductScreenEvent.OnAmountChange -> {
                onAmountChange(event.value)
            }
        }
    }

    private fun onAmountChange(value: FormAmountWithUnit) {
        _state.update {
            it.copy(
                amountFormFieldState = it.amountFormFieldState.copy(
                    value = value
                )
            )
        }
    }

    private fun onCategoryChange(value: CategoryDto?) {
        _state.update {
            it.copy(
                categoryFieldState = it.categoryFieldState.copy(
                    value = value
                )
            )
        }
    }

    private fun onExpirationDateChange(value: ExpirationDate) {
        _state.update {
            it.copy(
                expirationDateFieldState = it.expirationDateFieldState.copy(
                    value = value
                )
            )
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

    private fun onPriceChange(value: FormPriceWithCurrency) {
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

    private suspend fun fetchCategories() {
        _state.update {
            it.copy(
                globalLoading = true
            )
        }

        val categoriesResult = categoryRepository.search(
            CategoriesSearchRequest(
                query = "",
                paginationParams = PaginationParams(0, 10)
            )
        )

        categoriesResult.fold(
            { categories ->
                _state.update {
                    it.copy(
                        categoryFieldState = it.categoryFieldState.copy(
                            availableCategories = categories.items
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

    private suspend fun fetchUnits() {
        _state.update {
            it.copy(
                globalLoading = true
            )
        }

        val unitsResult = unitRepository.search(
            UnitsSearchRequest(
                query = "",
                paginationParams = PaginationParams(0, 10)
            )
        )

        unitsResult.fold(
            { units ->
                _state.update {
                    it.copy(
                        amountFormFieldState = it.amountFormFieldState.copy(
                            availableUnits = units.items
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


    private suspend fun submitForm(
        onSuccess: () -> Unit = {},
    ) {
        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        productRepository.createProduct(
            CreateRequest(
                name = state.value.nameFieldState.value,
                description = state.value.descriptionFieldState.value,
                price = state.value.priceFieldState.value.price,
                currencyId = state.value.priceFieldState.value.currency?.id,
                expirationDateKind = state.value.expirationDateFieldState.value.expirationDateKind,
                expirationDate = state.value.expirationDateFieldState.value.localDate,
                categoryId = state.value.categoryFieldState.value?.id!!,
                maximumAmountValue = state.value.amountFormFieldState.value.amount!!,
                amountUnitId = state.value.amountFormFieldState.value.unit!!.id
            )
        )

        _state.update {
            it.copy(
                formSubmitting = false
            )
        }

        onSuccess()
    }
}