package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.HomeViewModelState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import javax.inject.Inject

@HiltViewModel
class HomeViewModel @Inject constructor(
    private val userRepository: UserRepository,
    private val authManager: AuthManager
) : ViewModel() {
    private val _state = MutableStateFlow(HomeViewModelState())
    val state = _state.asStateFlow()

    suspend fun fetchUserData() {
        _state.update {
            it.copy(
                loading = true
            )
        }
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

        _state.update {
            it.copy(
                loading = false
            )
        }
    }

    fun onLogoutClick() {
        authManager.logout()
    }

    suspend fun isOnboarded(): Boolean {
        _state.update {
            it.copy(
                loading = true
            )
        }
        val meResult = userRepository.me()

        val isOnboarded = meResult.fold(
            {
                true
            },
            { repositoryError ->
                if (repositoryError is ApiRepositoryError && repositoryError.apiErrorCode == 404) {
                    false
                } else {
                    _state.update {
                        it.copy(
                            loading = false,
                            error = repositoryError.message
                        )
                    }
                    false
                }
            }
        )

        _state.update {
            it.copy(
                loading = false
            )
        }

        return isOnboarded
    }
}