package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.ProfileScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.delay
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import javax.inject.Inject

@HiltViewModel
class ProfileViewModel @Inject constructor(
    private val authManager: AuthManager,
    private val userRepository: UserRepository
) : ViewModel() {

    private val _state = MutableStateFlow(ProfileScreenState())
    val state = _state.asStateFlow()
    fun logout() {
        authManager.logout()
    }

    suspend fun init() {
        _state.update { it.copy(loading = true) }
        val meResponse = userRepository.me()
        meResponse.fold(
            { response -> _state.update { it.copy(user = response.user) } },
            { error -> _state.update { it.copy(error = error.message) } }
        )
        _state.update { it.copy(loading = false) }
    }
}