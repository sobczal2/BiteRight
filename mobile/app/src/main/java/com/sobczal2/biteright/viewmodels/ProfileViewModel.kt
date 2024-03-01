package com.sobczal2.biteright.viewmodels

import androidx.compose.material3.SnackbarHostState
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.data.api.requests.users.MeRequest
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
    lateinit var snackbarHostState: SnackbarHostState

    private val _state = MutableStateFlow(ProfileScreenState())
    val state = _state.asStateFlow()

    private val _events = Channel<ProfileScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
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

    fun fetchUserData() {
        viewModelScope.launch {
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions + ProfileViewModel::fetchUserData.name,
                )
            }
            val meResponse = userRepository.me(
                MeRequest()
            )
            meResponse.fold(
                { response ->
                    _state.update {
                        it.copy(user = response.user)
                    }
                },
                { error ->
                    snackbarHostState.showSnackbar(
                        message = error.message
                    )
                }
            )
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - ProfileViewModel::fetchUserData.name,
                )
            }
        }
    }

    private fun logout() {
        authManager.logout()
    }
}