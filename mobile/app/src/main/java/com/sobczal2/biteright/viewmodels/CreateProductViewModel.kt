package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.products.CreateRequest
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.amounts.FormAmountWithUnit
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import com.sobczal2.biteright.util.CommonRegexes
import com.sobczal2.biteright.util.StringProvider
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
import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest as CurrenciesSearchRequest

@HiltViewModel
class CreateProductViewModel @Inject constructor(
    private val currencyRepository: CurrencyRepository,
    private val categoryRepository: CategoryRepository,
    private val unitRepository: UnitRepository,
    private val productRepository: ProductRepository,
    private val userRepository: UserRepository,
    private val stringProvider: StringProvider,
    imageRequestBuilder: ImageRequest.Builder,
) : ViewModel() {
    private val _state = MutableStateFlow(
        CreateProductScreenState(
            imageRequestBuilder = imageRequestBuilder
        )
    )
    val state = _state.asStateFlow()

    private val _events = Channel<CreateProductScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch {  _events.receiveAsFlow().collect { handleEvent(it) } }
            _state.update { it.copy(globalLoading = true) }

            val fetchCurrenciesJob = launch { fetchCurrencies() }
            val fetchUnitsJob = launch { fetchUnits() }
            val fetchDefaultCategoryJob = launch { fetchDefaultCategory() }
            val fetchCurrentUserJob = launch { fetchCurrentUser() }

            fetchCurrenciesJob.join()
            fetchUnitsJob.join()
            fetchDefaultCategoryJob.join()
            fetchCurrentUserJob.join()

            _state.update { it.copy(globalLoading = false) }
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
                    value = value,
                    amountError = null,
                    unitError = null,
                )
            )
        }
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
                    priceError = null,
                    currencyError = null,
                )
            )
        }
    }

    private suspend fun fetchCurrencies() {
        val currenciesResult = currencyRepository.search(CurrenciesSearchRequest(
            query = "",
            paginationParams = PaginationParams(0, 10)
        ))

        currenciesResult.fold(
            { response ->
                _state.update {
                    it.copy(
                        priceFieldState = it.priceFieldState.copy(
                            availableCurrencies = response.currencies.items
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
    }

    private suspend fun fetchUnits() {
        val unitsResult = unitRepository.search(
            UnitsSearchRequest(
                query = "",
                paginationParams = PaginationParams(0, 10)
            )
        )

        unitsResult.fold(
            { response ->
                _state.update {
                    it.copy(
                        amountFormFieldState = it.amountFormFieldState.copy(
                            availableUnits = response.units.items
                        )
                    )
                }
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )
    }

    private suspend fun fetchDefaultCategory() {
        val defaultCategoryResult = categoryRepository.getDefault()

        defaultCategoryResult.fold(
            { response ->
                _state.update {
                    it.copy(
                        categoryFieldState = it.categoryFieldState.copy(
                            value = response.category
                        )
                    )
                }
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )
    }

    private suspend fun fetchCurrentUser() {
        val currentUserResult = userRepository.me()

        currentUserResult.fold(
            { response ->
                _state.update { state ->
                    state.copy(
                        priceFieldState = state.priceFieldState.copy(
                            value = state.priceFieldState.value.copy(
                                currency = response.user.profile.currency
                            )
                        )
                    )
                }
            },
            { repositoryError ->
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )
    }

    suspend fun searchCategories(
        query: String,
        paginationParams: PaginationParams
    ): PaginatedList<CategoryDto> {

        val categoriesResult = categoryRepository.search(
            CategoriesSearchRequest(
                query = query,
                paginationParams = paginationParams
            )
        )

        categoriesResult.fold(
            { response ->
                return response.categories
            },
            { repositoryError ->
                _state.value = state.value.copy(
                    globalError = repositoryError.message
                )
            }
        )
        return emptyPaginatedList()
    }

    private suspend fun submitForm(
        onSuccess: () -> Unit = {},
    ) {
        if (!validate()) return

        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        val createResult = productRepository.createProduct(
            CreateRequest(
                name = state.value.nameFieldState.value,
                description = state.value.descriptionFieldState.value,
                price = state.value.priceFieldState.value.price,
                currencyId = state.value.priceFieldState.value.currency.id,
                expirationDateKind = state.value.expirationDateFieldState.value.expirationDateKind,
                expirationDate = state.value.expirationDateFieldState.value.localDate,
                categoryId = state.value.categoryFieldState.value.id,
                maximumAmountValue = state.value.amountFormFieldState.value.amount!!,
                amountUnitId = state.value.amountFormFieldState.value.unit!!.id
            )
        )

        createResult.fold(
            {
                onSuccess()
            },
            { repositoryError ->
                if (repositoryError is ApiRepositoryError) {
                    repositoryError.apiErrors.forEach { (key, value) ->
                        when (key.lowercase()) {
                            CreateRequest::name.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        nameFieldState = it.nameFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::description.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        descriptionFieldState = it.descriptionFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::price.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            priceError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::currencyId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            currencyError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::expirationDateKind.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        expirationDateFieldState = it.expirationDateFieldState.copy(
                                            expirationDateKindError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::expirationDate.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        expirationDateFieldState = it.expirationDateFieldState.copy(
                                            localDateError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::categoryId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        categoryFieldState = it.categoryFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::maximumAmountValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        amountFormFieldState = it.amountFormFieldState.copy(
                                            amountError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::amountUnitId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        amountFormFieldState = it.amountFormFieldState.copy(
                                            unitError = value.firstOrNull()
                                        )
                                    )
                                }
                            }
                        }
                    }
                } else {
                    _state.value = state.value.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )

        _state.update {
            it.copy(
                formSubmitting = false
            )
        }
    }

    private fun validate(): Boolean {
        var isValid = true

        if (!validateNameField()) isValid = false
        if (!validateDescriptionField()) isValid = false
        if (!validatePriceField()) isValid = false
        if (!validateCategoryField()) isValid = false
        if (!validateAmountField()) isValid = false

        return isValid
    }

    private fun validateAmountField(): Boolean {
        var isValid = true
        if (state.value.amountFormFieldState.value.amount == null) {
            _state.update {
                it.copy(
                    amountFormFieldState = it.amountFormFieldState.copy(
                        amountError = stringProvider.getString(R.string.validation_amount_empty)
                    )
                )
            }
            isValid = false
        }

        if (state.value.amountFormFieldState.value.unit == null) {
            _state.update {
                it.copy(
                    amountFormFieldState = it.amountFormFieldState.copy(
                        unitError = stringProvider.getString(R.string.validation_unit_empty)
                    )
                )
            }
            isValid = false
        }
        return isValid
    }

    private fun validateCategoryField(): Boolean {
        var isValid = true
        if (state.value.categoryFieldState.value == null) {
            _state.update {
                it.copy(
                    categoryFieldState = it.categoryFieldState.copy(
                        error = stringProvider.getString(R.string.validation_category_empty)
                    )
                )
            }
            isValid = false
        }
        return isValid
    }

    private fun validatePriceField(): Boolean {
        var isValid = true
        if (state.value.priceFieldState.value.price != null && state.value.priceFieldState.value.currency == null) {
            _state.update {
                it.copy(
                    priceFieldState = it.priceFieldState.copy(
                        currencyError = stringProvider.getString(R.string.validation_currency_empty_when_price_provided)
                    )
                )
            }
            isValid = false
        }

        if (state.value.priceFieldState.value.price == null && state.value.priceFieldState.value.currency != null) {
            _state.update {
                it.copy(
                    priceFieldState = it.priceFieldState.copy(
                        priceError = stringProvider.getString(R.string.validation_price_empty_when_currency_provided)
                    )
                )
            }
            isValid = false
        }

        val minPrice = 0.00
        val maxPrice = 1e6

        if (state.value.priceFieldState.value.price != null && state.value.priceFieldState.value.price!! !in minPrice..maxPrice) {
            _state.update {
                it.copy(
                    priceFieldState = it.priceFieldState.copy(
                        priceError = stringProvider.getString(
                            R.string.validation_price_not_valid,
                            minPrice,
                            maxPrice
                        )
                    )
                )
            }
            isValid = false
        }
        return isValid
    }

    private fun validateDescriptionField(): Boolean {
        var isValid = true
        val maxDescriptionLength = 512

        if (state.value.descriptionFieldState.value.length > maxDescriptionLength) {
            _state.update {
                it.copy(
                    descriptionFieldState = it.descriptionFieldState.copy(
                        error = stringProvider.getString(
                            R.string.validation_description_length_not_valid,
                            maxDescriptionLength
                        )
                    )
                )
            }
            isValid = false
        }

        val validDescriptionCharacters = CommonRegexes.alphanumericWithSpacesAndSpecialCharacters

        if (!validDescriptionCharacters.matches(state.value.descriptionFieldState.value)) {
            _state.update {
                it.copy(
                    descriptionFieldState = it.descriptionFieldState.copy(
                        error = stringProvider.getString(
                            R.string.validation_description_characters_not_valid,
                            validDescriptionCharacters
                        )
                    )
                )
            }
            isValid = false
        }
        return isValid
    }

    private fun validateNameField(): Boolean {
        var valid = true
        if (state.value.nameFieldState.value.isEmpty()) {
            _state.update {
                it.copy(
                    nameFieldState = it.nameFieldState.copy(
                        error = stringProvider.getString(R.string.validation_name_empty)
                    )
                )
            }
            valid = false
        }

        val nameMinLength = 3
        val nameMaxLength = 64

        if (state.value.nameFieldState.value.length !in nameMinLength..nameMaxLength) {
            _state.update {
                it.copy(
                    nameFieldState = it.nameFieldState.copy(
                        error = stringProvider.getString(
                            R.string.validation_name_length_not_valid,
                            nameMinLength,
                            nameMaxLength
                        )
                    )
                )
            }
            valid = false
        }

        val validCharacters = CommonRegexes.alphanumericWithSpacesAndSpecialCharacters

        if (!validCharacters.matches(state.value.nameFieldState.value)) {
            _state.update {
                it.copy(
                    nameFieldState = it.nameFieldState.copy(
                        error = stringProvider.getString(
                            R.string.validation_name_characters_not_valid,
                            validCharacters
                        )
                    )
                )
            }
            valid = false
        }
        return valid
    }
}