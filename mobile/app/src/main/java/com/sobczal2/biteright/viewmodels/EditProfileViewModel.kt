package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.data.api.requests.users.MeRequest
import com.sobczal2.biteright.data.api.requests.users.UpdateProfileRequest
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.events.EditProfileScreenEvent
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.EditProfileScreenState
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
class EditProfileViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val currencyRepository: CurrencyRepository
) : ViewModel() {
    private val _state = MutableStateFlow(EditProfileScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<EditProfileScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { _events.receiveAsFlow().collect { event -> handleEvent(event) } }
            launch { fetchInitialSearchData() }
        }
    }

    fun sendEvent(event: EditProfileScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: EditProfileScreenEvent) {
        when (event) {
            is EditProfileScreenEvent.OnSubmitClick -> handleSubmitClick(event.onSuccess)
            is EditProfileScreenEvent.OnCurrencyChange -> handleCurrencyChange(event.value)
            is EditProfileScreenEvent.OnTimeZoneChange -> handleTimeZoneChange(event.value)
        }
    }

    fun fetchUserData() {
        viewModelScope.launch {
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions + EditProfileViewModel::fetchUserData.name,
                )
            }
            val meResponse = userRepository.me(
                MeRequest()
            )
            meResponse.fold(
                { response ->
                    _state.update {
                        it.copy(
                            currencyFieldState = it.currencyFieldState.copy(value = response.user.profile.currency),
                            timeZoneFieldState = it.timeZoneFieldState.copy(
                                value = TimeZone.getTimeZone(
                                    response.user.profile.timeZoneId
                                )
                            )
                        )
                    }
                },
                { error ->
                    _state.update {
                        it.copy(globalError = error.message)
                    }
                }
            )
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - EditProfileViewModel::fetchUserData.name,
                )
            }
        }
    }

    private fun handleCurrencyChange(value: CurrencyDto) {
        _state.update {
            it.copy(currencyFieldState = it.currencyFieldState.copy(value = value))
        }
    }

    private fun handleTimeZoneChange(value: TimeZone) {
        _state.update {
            it.copy(timeZoneFieldState = it.timeZoneFieldState.copy(value = value))
        }
    }

    private fun handleSubmitClick(onSuccess: () -> Unit) {
        viewModelScope.launch {
            _state.update {
                it.copy(formSubmitting = true)
            }

            val response = userRepository.updateProfile(
                UpdateProfileRequest(
                    currencyId = _state.value.currencyFieldState.value.id,
                    timeZoneId = _state.value.timeZoneFieldState.value.id
                )
            )

            response.fold(
                {
                    onSuccess()
                },
                { error ->
                    _state.update {
                        it.copy(
                            formSubmitting = false,
                            globalError = error.message
                        )
                    }
                }
            )
        }
    }

    private suspend fun fetchInitialSearchData() {
        _state.update {
            it.copy(
                ongoingLoadingActions = it.ongoingLoadingActions + EditProfileViewModel::fetchInitialSearchData.name,
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
                    ongoingLoadingActions = it.ongoingLoadingActions - EditProfileViewModel::fetchInitialSearchData.name,
                )
            }
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
                _state.update { state ->
                    state.copy(
                        globalError = repositoryError.message
                    )
                }
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
}