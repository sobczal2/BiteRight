package com.sobczal2.biteright.viewmodels

import androidx.compose.material3.SnackbarHostState
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.requests.currencies.GetDefaultRequest
import com.sobczal2.biteright.data.api.requests.users.OnboardRequest
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.events.OnboardScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.OnboardScreenState
import com.sobczal2.biteright.util.StringProvider
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.TimeZone
import javax.inject.Inject

@HiltViewModel
class OnboardViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val stringProvider: StringProvider,
    private val currencyRepository: CurrencyRepository,
) : ViewModel() {
    lateinit var snackbarHostState: SnackbarHostState
    private val _state = MutableStateFlow(OnboardScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<OnboardScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch {
                events.collect { event ->
                    handleEvent(event)
                }
            }
            launch { fetchInitialSearchData() }
            launch { fetchDefaultCurrency() }
        }
    }

    private suspend fun fetchDefaultCurrency() {
        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions + OnboardViewModel::fetchDefaultCurrency.name,
            )
        }

        val defaultCurrencyResult = currencyRepository.getDefault(
            GetDefaultRequest()
        )

        defaultCurrencyResult.fold(
            { response ->
                _state.update {
                    it.copy(
                        currencyFieldState = it.currencyFieldState.copy(
                            value = response.currency,
                            error = null,
                        )
                    )
                }
            },
            { repositoryError ->
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
            }
        )

        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions - OnboardViewModel::fetchDefaultCurrency.name,
            )
        }
    }

    fun sendEvent(event: OnboardScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: OnboardScreenEvent) {
        when (event) {
            is OnboardScreenEvent.OnUsernameChange -> handleUsernameChange(event.value)
            is OnboardScreenEvent.OnNextClick -> handleNextClick(event.onSuccess)
            is OnboardScreenEvent.OnCurrencyChange -> handleCurrencyChange(event.value)
            is OnboardScreenEvent.OnTimeZoneChange -> handleTimeZoneChange(event.value)
        }
    }

    private fun handleTimeZoneChange(value: TimeZone) {
        _state.update {
            it.copy(
                timeZoneFieldState = it.timeZoneFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private fun handleCurrencyChange(value: CurrencyDto) {
        _state.update {
            it.copy(
                currencyFieldState = it.currencyFieldState.copy(
                    value = value,
                    error = null,
                )
            )
        }
    }

    private fun handleUsernameChange(username: String) {
        _state.update {
            it.copy(
                usernameFieldState = it.usernameFieldState.copy(
                    value = username,
                    error = null,
                )
            )
        }
    }

    private fun validate(): Boolean {
        var isValid = true

        val usernameMinLength = 3
        val usernameMaxLength = 64
        if (_state.value.usernameFieldState.value.length !in usernameMinLength..usernameMaxLength) {
            _state.update {
                it.copy(
                    usernameFieldState = it.usernameFieldState.copy(
                        error = stringProvider.getString(
                            R.string.username_length_error,
                            usernameMinLength,
                            usernameMaxLength
                        )
                    )
                )
            }
            isValid = false
        }
        if (!Regex("^[\\p{L}\\p{Nd}-_]*\$").matches(_state.value.usernameFieldState.value)) {
            _state.update {
                it.copy(
                    usernameFieldState = it.usernameFieldState.copy(
                        error = stringProvider.getString(R.string.username_invalid_characters_error)
                    )
                )
            }
            isValid = false
        }

        return isValid
    }

    private fun handleNextClick(onSuccess: () -> Unit) {
        validate()

        if (_state.value.usernameFieldState.error != null) {
            return
        }

        _state.update {
            it.copy(
                formSubmitting = true
            )
        }

        val onboardRequest = OnboardRequest(
            username = state.value.usernameFieldState.value,
            currencyId = state.value.currencyFieldState.value.id,
            timeZoneId = state.value.timeZoneFieldState.value.id
        )

        viewModelScope.launch {
            val onboardResult = userRepository.onboard(onboardRequest)
            onboardResult.fold(
                {
                    onSuccess()
                },
                { repositoryError ->
                    if (repositoryError is ApiRepositoryError && repositoryError.apiErrors.any { it.key == "username" }) {
                        _state.update {
                            it.copy(
                                usernameFieldState = it.usernameFieldState.copy(
                                    error = repositoryError.apiErrors["username"]?.first()
                                )
                            )
                        }
                    } else {
                        snackbarHostState.showSnackbar(
                            message = repositoryError.message,
                        )
                    }

                    _state.update {
                        it.copy(
                            formSubmitting = false
                        )
                    }
                }
            )
        }

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

    fun searchTimeZones(
        query: String,
        paginationParams: PaginationParams
    ): PaginatedList<TimeZone> {
        if (query.isEmpty()) {
            return PaginatedList(
                items = _state.value.availableTimeZones
                    .drop(
                        paginationParams.pageNumber * paginationParams.pageSize
                    )
                    .take(paginationParams.pageSize),
                pageNumber = paginationParams.pageNumber,
                pageSize = paginationParams.pageSize,
                totalCount = _state.value.availableTimeZones.size,
                totalPages = _state.value.availableTimeZones.size / paginationParams.pageSize
            )
        }
        val result =
            _state.value.availableTimeZones.filter { it.id.lowercase().contains(query.lowercase()) }

        return PaginatedList(
            items = result
                .drop(
                    paginationParams.pageNumber * paginationParams.pageSize
                )
                .take(paginationParams.pageSize),
            pageNumber = paginationParams.pageNumber,
            pageSize = paginationParams.pageSize,
            totalCount = result.size,
            totalPages = result.size / paginationParams.pageSize
        )
    }

    private suspend fun fetchInitialSearchData() {
        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions + OnboardViewModel::fetchInitialSearchData.name,
            )
        }

        coroutineScope {
            val fetchCurrenciesJob = launch {
                val result = searchCurrencies("", PaginationParams.Default)

                _state.update {
                    it.copy(startingCurrencies = result)
                }
            }

            fetchCurrenciesJob.join()

            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - OnboardViewModel::fetchInitialSearchData.name,
                )
            }
        }
    }
}