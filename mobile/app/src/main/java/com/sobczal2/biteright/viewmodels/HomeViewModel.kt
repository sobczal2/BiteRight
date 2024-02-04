package com.sobczal2.biteright.viewmodels

import androidx.compose.ui.res.stringResource
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.state.HomeViewModelState
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

class HomeViewModel @Inject constructor(
    private val userRepository: UserRepository
) : ViewModel() {
    private val _state = MutableStateFlow(HomeViewModelState())
    val state = _state.asStateFlow()

    init {
        viewModelScope.launch {
            fetchUserData()
        }
    }

    private suspend fun fetchUserData() {
        val meResult = userRepository.me()

        meResult.fold(
            {
                _state.value = state.value.copy(
                    username = it.username,
                    email = it.email
                )
            },
            {
                _state.value = state.value.copy(
                    loading = false,
                    error = it.message
                )
            }
        )
    }
}