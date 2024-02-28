package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.data.api.requests.products.GetDetailsRequest
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.events.EditProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.EditProductScreenState
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormAmountWithUnit
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class EditProductViewModel @Inject constructor(
    private val productRepository: ProductRepository,
    private val userRepository: UserRepository,
    private val categoryRepository: CategoryRepository,
    private val currencyRepository: CurrencyRepository,
    private val unitRepository: UnitRepository
) : ViewModel() {
    private val _state = MutableStateFlow(EditProductScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<EditProductScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { _events.receiveAsFlow().collect { handleEvent(it) } }

            val fetchInitialSearchDataJob = launch { fetchInitialSearchData() }

            fetchInitialSearchDataJob.join()

            _state.update {
                it.copy(
                    globalLoading = false
                )
            }
        }
    }

    private suspend fun fetchInitialSearchData() {
        coroutineScope {
            val fetchCategoriesJob = launch {
                val result = searchCategories("", PaginationParams.Default)

                _state.update {
                    it.copy(
                        startingCategories = result
                    )
                }
            }
            val fetchCurrenciesJob = launch {
                val result = searchCurrencies("", PaginationParams.Default)
                _state.update {
                    it.copy(
                        startingCurrencies = result
                    )
                }
            }
            val fetchUnitsJob = launch {
                val result = searchUnits("", PaginationParams.Default)
                _state.update {
                    it.copy(
                        startingUnits = result
                    )
                }
            }

            fetchCategoriesJob.join()
            fetchCurrenciesJob.join()
            fetchUnitsJob.join()
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
            is EditProductScreenEvent.OnNameChange -> {
                onNameChange(event.value)
            }
            is EditProductScreenEvent.OnCategoryChange -> {
                onCategoryChange(event.value)
            }
            is EditProductScreenEvent.OnExpirationDateChange -> {
                onExpirationDateChange(event.value)
            }
            is EditProductScreenEvent.OnPriceChange -> {
                onPriceChange(event.value)
            }
            is EditProductScreenEvent.OnDescriptionChange -> {
                onDescriptionChange(event.value)
            }
        }
    }

    private fun loadDetails(productId: UUID) {
        viewModelScope.launch {
            _state.update {
                it.copy(
                    productId = productId,
                )
            }

            val meResult = userRepository.me()

            val userDto = meResult.fold(
                { response ->
                    response.user
                },
                { error ->
                    _state.update {
                        it.copy(
                            globalError = error.message,
                        )
                    }
                    null
                }
            ) ?: return@launch

            val getDetailsResult = productRepository.getDetails(
                GetDetailsRequest(productId)
            )

            getDetailsResult.fold(
                { response ->
                    val product = response.product
                    _state.update {
                        it.copy(
                            nameFieldState = it.nameFieldState.copy(
                                value = product.name
                            ),
                            descriptionFieldState = it.descriptionFieldState.copy(
                                value = product.description
                            ),
                            priceFieldState = it.priceFieldState.copy(
                                value = FormPriceWithCurrency(
                                    price = product.priceValue,
                                    currency = product.priceCurrency ?: userDto.profile.currency
                                )
                            ),
                            expirationDateFieldState = it.expirationDateFieldState.copy(
                                value = ExpirationDate(
                                    expirationDateKind = product.expirationDateKind,
                                    localDate = product.expirationDateValue
                                )
                            ),
                            categoryFieldState = it.categoryFieldState.copy(
                                value = product.category
                            ),
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

    suspend fun searchCategories(
        query: String,
        paginationParams: PaginationParams
    ): PaginatedList<CategoryDto> {

        if (_state.value.startingCategories != null && query == "" && paginationParams == PaginationParams.Default) {
            return _state.value.startingCategories!!
        }

        val categoriesResult = categoryRepository.search(
            SearchRequest(
                query = query,
                paginationParams = paginationParams
            )
        )

        categoriesResult.fold(
            { response ->
                return response.categories
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )
        return emptyPaginatedList()
    }

    suspend fun searchCurrencies(
        query: String,
        paginationParams: PaginationParams
    ): PaginatedList<CurrencyDto> {

        if (_state.value.startingCurrencies != null && query == "" && paginationParams == PaginationParams.Default) {
            return _state.value.startingCurrencies!!
        }

        val currenciesResult = currencyRepository.search(
            com.sobczal2.biteright.data.api.requests.currencies.SearchRequest(
                query = query,
                paginationParams = paginationParams
            )
        )

        currenciesResult.fold(
            { response ->
                return response.currencies
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )
        return emptyPaginatedList()
    }

    suspend fun searchUnits(
        query: String,
        paginationParams: PaginationParams
    ): PaginatedList<UnitDto> {

        if (_state.value.startingUnits != null && query == "" && paginationParams == PaginationParams.Default) {
            return _state.value.startingUnits!!
        }

        val unitsResult = unitRepository.search(
            com.sobczal2.biteright.data.api.requests.units.SearchRequest(
                query = query,
                paginationParams = paginationParams
            )
        )

        unitsResult.fold(
            { response ->
                return response.units
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )

        return emptyPaginatedList()
    }

    private fun onCategoryChange(value: CategoryDto) {
        _state.update {
            it.copy(
                categoryFieldState = it.categoryFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private fun onExpirationDateChange(value: ExpirationDate) {
        _state.update {
            it.copy(
                expirationDateFieldState = it.expirationDateFieldState.copy(
                    value = value,
                    expirationDateKindError = null,
                    localDateError = null,
                )
            )
        }
    }

    private fun onNameChange(value: String) {
        _state.update {
            it.copy(
                nameFieldState = it.nameFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private fun onDescriptionChange(value: String) {
        _state.update {
            it.copy(
                descriptionFieldState = it.descriptionFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private fun onPriceChange(value: FormPriceWithCurrency) {
        _state.update {
            it.copy(
                priceFieldState = it.priceFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }
}