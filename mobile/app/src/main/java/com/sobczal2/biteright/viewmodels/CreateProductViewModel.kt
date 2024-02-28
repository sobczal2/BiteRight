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
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.events.CreateProductScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.CreateProductScreenState
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormMaxAmountWithUnit
import com.sobczal2.biteright.ui.components.products.FormPriceWithCurrency
import com.sobczal2.biteright.util.CommonRegexes
import com.sobczal2.biteright.util.StringProvider
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest as CategoriesSearchRequest
import com.sobczal2.biteright.data.api.requests.currencies.SearchRequest as CurrenciesSearchRequest
import com.sobczal2.biteright.data.api.requests.units.SearchRequest as UnitsSearchRequest

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
            launch { _events.receiveAsFlow().collect { handleEvent(it) } }

            val fetchInitialSearchDataJob = launch { fetchInitialSearchData() }
            val fetchDefaultCategoryJob = launch { fetchDefaultCategory() }
            val fetchCurrentUserJob = launch { fetchCurrentUser() }

            fetchInitialSearchDataJob.join()
            fetchDefaultCategoryJob.join()
            fetchCurrentUserJob.join()

            _state.update { currentState ->
                currentState.copy(
                    maxAmountFieldState = currentState.maxAmountFieldState.copy(
                        value = FormMaxAmountWithUnit.Empty.copy(
                            unit = currentState.startingUnits?.items?.firstOrNull() ?: UnitDto.Empty
                        )
                    )
                )
            }


            _state.update { it.copy(globalLoading = false) }
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

    private fun onAmountChange(value: FormMaxAmountWithUnit) {
        _state.update {
            it.copy(
                maxAmountFieldState = it.maxAmountFieldState.copy(
                    value = value,
                    error = null,
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
                    error = null,
                )
            )
        }
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

        if (_state.value.startingCategories != null && query == "" && paginationParams == PaginationParams.Default) {
            return _state.value.startingCategories!!
        }

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
            CurrenciesSearchRequest(
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
            UnitsSearchRequest(
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

    private suspend fun submitForm(
        onSuccess: () -> Unit = {},
    ) {
        if (!validate()) return

        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        val request = CreateRequest(
            name = state.value.nameFieldState.value,
            description = state.value.descriptionFieldState.value,
            priceValue = state.value.priceFieldState.value.price,
            priceCurrencyId = if (state.value.priceFieldState.value.price != null) state.value.priceFieldState.value.currency.id else null,
            expirationDateKind = state.value.expirationDateFieldState.value.expirationDateKind,
            expirationDate = state.value.expirationDateFieldState.value.localDate,
            categoryId = state.value.categoryFieldState.value.id,
            amountMaxValue = state.value.maxAmountFieldState.value.maxAmount!!,
            amountUnitId = state.value.maxAmountFieldState.value.unit.id
        )

        val createResult = productRepository.create(request)

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

                            CreateRequest::priceValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::priceCurrencyId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            error = value.firstOrNull()
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

                            CreateRequest::amountMaxValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        maxAmountFieldState = it.maxAmountFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            CreateRequest::amountUnitId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        maxAmountFieldState = it.maxAmountFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            else -> {
                                _state.update {
                                    it.copy(
                                        globalError = value.firstOrNull()
                                    )
                                }
                            }
                        }
                    }
                } else {
                    _state.update {
                        it.copy(
                            globalError = repositoryError.message
                        )
                    }
                }
            }
        )

        _state.update {
            it.copy(
                formSubmitting = false
            )
        }
    }

    // TODO: Move to a separate class
    private fun validate(): Boolean {
        var isValid = true

        if (!validateNameField()) isValid = false
        if (!validateDescriptionField()) isValid = false
        if (!validatePriceField()) isValid = false
        if (!validateCategoryField()) isValid = false
        if (!validateMaxAmountField()) isValid = false
        if (!validateExpirationDateField()) isValid = false

        return isValid
    }

    private fun validateMaxAmountField(): Boolean {
        var isValid = true
        if (state.value.maxAmountFieldState.value.maxAmount == null) {
            _state.update {
                it.copy(
                    maxAmountFieldState = it.maxAmountFieldState.copy(
                        error = stringProvider.getString(R.string.validation_amount_empty)
                    )
                )
            }
            isValid = false
        }
        return isValid
    }

    private fun validateCategoryField(): Boolean {
        return true
    }

    private fun validatePriceField(): Boolean {
        var isValid = true

        val minPrice = 0.00
        val maxPrice = 1e6

        if (state.value.priceFieldState.value.price != null && state.value.priceFieldState.value.price!! !in minPrice..maxPrice) {
            _state.update {
                it.copy(
                    priceFieldState = it.priceFieldState.copy(
                        error = stringProvider.getString(
                            R.string.validation_price_value_not_valid,
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

    private fun validateExpirationDateField(): Boolean {

        if (state.value.expirationDateFieldState.value.expirationDateKind.shouldIncludeDate() && state.value.expirationDateFieldState.value.localDate == null) {
            _state.update {
                it.copy(
                    expirationDateFieldState = it.expirationDateFieldState.copy(
                        localDateError = stringProvider.getString(R.string.validation_expiration_date_empty)
                    )
                )
            }
            return false
        }

        return true
    }
}