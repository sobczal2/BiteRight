package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.events.ProfileScreenEvent
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.ProfileScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ProfileViewModel @Inject constructor(
    private val authManager: AuthManager, private val userRepository: UserRepository
) : ViewModel() {

    private val _state = MutableStateFlow(ProfileScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<ProfileScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
            launch { fetchUserData() }
        }
    }

    fun sendEvent(event: ProfileScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: ProfileScreenEvent) {
        when (event) {
            is ProfileScreenEvent.OnLogoutClick -> logout()
        }
    }

    private suspend fun fetchUserData() {
        _state.update { it.copy(globalLoading = true) }
        val meResponse = userRepository.me()
        meResponse.fold({ response -> _state.update { it.copy(user = response.user) } },
            { error -> _state.update { it.copy(globalError = error.message) } })
        _state.update { it.copy(globalLoading = false) }
    }

    private fun logout() {
        authManager.logout()
    }

}