package com.sobczal2.biteright.viewmodels

import androidx.compose.material3.SnackbarHostState
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.categories.SearchRequest
import com.sobczal2.biteright.data.api.requests.products.DeleteRequest
import com.sobczal2.biteright.data.api.requests.products.EditRequest
import com.sobczal2.biteright.data.api.requests.products.GetDetailsRequest
import com.sobczal2.biteright.data.api.requests.users.MeRequest
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
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.EditProductScreenState
import com.sobczal2.biteright.ui.components.products.ExpirationDate
import com.sobczal2.biteright.ui.components.products.FormAmountWithUnit
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
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class EditProductViewModel @Inject constructor(
    private val productRepository: ProductRepository,
    private val userRepository: UserRepository,
    private val categoryRepository: CategoryRepository,
    private val currencyRepository: CurrencyRepository,
    private val unitRepository: UnitRepository,
    private val stringProvider: StringProvider,
    imageRequestBuilder: ImageRequest.Builder,
) : ViewModel() {
    lateinit var snackbarHostState: SnackbarHostState
    private val _state = MutableStateFlow(
        EditProductScreenState(
            imageRequestBuilder = imageRequestBuilder
        )
    )
    val state = _state.asStateFlow()

    private val _events = Channel<EditProductScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { handleEvent(it) } }
            launch { fetchInitialSearchData() }
        }
    }

    private suspend fun fetchInitialSearchData() {
        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions + EditProductViewModel::fetchInitialSearchData.name,
            )
        }

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

            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - EditProductViewModel::fetchInitialSearchData.name,
                )
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

            is EditProductScreenEvent.OnAmountChange -> {
                onAmountChange(event.value)
            }

            is EditProductScreenEvent.OnSubmitClick -> {
                viewModelScope.launch {
                    submitForm(event.onSuccess)
                }
            }

            is EditProductScreenEvent.OnDeleteClick -> {
                viewModelScope.launch {
                    deleteProduct(event.onSuccess)
                }
            }
        }
    }

    private fun loadDetails(productId: UUID) {
        _state.update {
            it.copy(
                productId = productId,
                ongoingLoadingActions = it.ongoingLoadingActions + EditProductViewModel::loadDetails.name,
            )
        }

        viewModelScope.launch {

            val meResult = userRepository.me(
                MeRequest()
            )

            val userDto = meResult.fold(
                { response ->
                    response.user
                },
                { error ->
                    snackbarHostState.showSnackbar(
                        message = error.message,
                    )
                    return@launch
                }
            )

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
                            amountFieldState = it.amountFieldState.copy(
                                value = FormAmountWithUnit(
                                    currentAmount = product.amountCurrentValue,
                                    maxAmount = product.amountMaxValue,
                                    unit = product.amountUnit
                                )
                            )
                        )
                    }
                },
                { error ->
                    snackbarHostState.showSnackbar(
                        message = error.message,
                    )
                }
            )

            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - EditProductViewModel::loadDetails.name,
                )
            }
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
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
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
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
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
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
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

    private fun onAmountChange(value: FormAmountWithUnit) {
        _state.update {
            it.copy(
                amountFieldState = it.amountFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private suspend fun submitForm(
        onSuccess: () -> Unit
    ) {
        if (!validate()) return

        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        val request = EditRequest(
            productId = state.value.productId!!,
            name = state.value.nameFieldState.value,
            description = state.value.descriptionFieldState.value,
            priceValue = state.value.priceFieldState.value.price,
            priceCurrencyId = if (state.value.priceFieldState.value.price != null) state.value.priceFieldState.value.currency.id else null,
            expirationDateKind = state.value.expirationDateFieldState.value.expirationDateKind,
            expirationDate = state.value.expirationDateFieldState.value.localDate,
            categoryId = state.value.categoryFieldState.value.id,
            amountCurrentValue = state.value.amountFieldState.value.currentAmount!!,
            amountMaxValue = state.value.amountFieldState.value.maxAmount!!,
            amountUnitId = state.value.amountFieldState.value.unit.id
        )

        val editResult = productRepository.edit(request)

        editResult.fold(
            { _ ->
                onSuccess()
            },
            { repositoryError ->
                if (repositoryError is ApiRepositoryError) {
                    repositoryError.apiErrors.forEach { (key, value) ->
                        when (key.lowercase()) {
                            EditRequest::name.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        nameFieldState = it.nameFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::description.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        descriptionFieldState = it.descriptionFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::priceValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::priceCurrencyId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        priceFieldState = it.priceFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::expirationDateKind.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        expirationDateFieldState = it.expirationDateFieldState.copy(
                                            expirationDateKindError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::expirationDate.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        expirationDateFieldState = it.expirationDateFieldState.copy(
                                            localDateError = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::categoryId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        categoryFieldState = it.categoryFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::amountCurrentValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        amountFieldState = it.amountFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::amountMaxValue.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        amountFieldState = it.amountFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            EditRequest::amountUnitId.name.lowercase() -> {
                                _state.update {
                                    it.copy(
                                        amountFieldState = it.amountFieldState.copy(
                                            error = value.firstOrNull()
                                        )
                                    )
                                }
                            }

                            else -> {
                                snackbarHostState.showSnackbar(
                                    message = value.firstOrNull()
                                        ?: stringProvider.getString(R.string.unknown_error)
                                )
                            }
                        }
                    }
                } else {
                    snackbarHostState.showSnackbar(
                        message = repositoryError.message
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

    // TODO: Move to a separate class
    private fun validate(): Boolean {
        var isValid = true

        if (!validateNameField()) isValid = false
        if (!validateDescriptionField()) isValid = false
        if (!validatePriceField()) isValid = false
        if (!validateCategoryField()) isValid = false
        if (!validateAmountField()) isValid = false
        if (!validateExpirationDateField()) isValid = false

        return isValid
    }

    private fun validateAmountField(): Boolean {
        var isValid = true

        if (state.value.amountFieldState.value.currentAmount == null) {
            _state.update {
                it.copy(
                    amountFieldState = it.amountFieldState.copy(
                        error = stringProvider.getString(R.string.validation_current_amount_empty)
                    )
                )
            }
            isValid = false
        }

        if (state.value.amountFieldState.value.maxAmount == null) {
            _state.update {
                it.copy(
                    amountFieldState = it.amountFieldState.copy(
                        error = stringProvider.getString(R.string.validation_max_amount_empty)
                    )
                )
            }
            isValid = false
        }

        if (state.value.amountFieldState.value.maxAmount != null && state.value.amountFieldState.value.currentAmount != null && state.value.amountFieldState.value.currentAmount!! > state.value.amountFieldState.value.maxAmount!!) {
            _state.update {
                it.copy(
                    amountFieldState = it.amountFieldState.copy(
                        error = stringProvider.getString(R.string.validation_current_amount_greater_than_max_amount)
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

    private suspend fun deleteProduct(
        onSuccess: () -> Unit
    ) {
        _state.update {
            it.copy(
                deleteSubmitting = true
            )
        }

        val deleteResult = productRepository.delete(
            DeleteRequest(
                productId = state.value.productId!!
            )
        )

        deleteResult.fold(
            { _ ->
                onSuccess()
            },
            { repositoryError ->
                snackbarHostState.showSnackbar(
                    message = repositoryError.message
                )
            }
        )

        _state.update {
            it.copy(
                deleteSubmitting = false
            )
        }
    }
}