package com.sobczal2.biteright.viewmodel.screens

import android.content.Context
import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.domain.repository.UserRepository
import com.sobczal2.biteright.dto.Status
import com.sobczal2.biteright.util.LocaleHelper
import dagger.hilt.android.lifecycle.HiltViewModel
import dagger.hilt.android.qualifiers.ApplicationContext
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import javax.inject.Inject

@HiltViewModel
class OnboardViewModel @Inject constructor(
    private val userRepository: UserRepository
) : ViewModel() {
    private val _state = MutableStateFlow(OnboardState())
    val state = _state.asStateFlow()

    fun setUsername(username: String) {
        _state.value = _state.value.copy(username = username)
    }

    suspend fun isOnboarded(context: Context): Boolean {
        val result = userRepository.me()
        return result.fold(
            {
                LocaleHelper.setLocale(context, it.profile.languageCode)
                true
            },
            {
                if (it.status == Status.CONNECTION_ERROR) {
                    _state.value = _state.value.copy(error = "CONNECTION_ERROR")
                } else if (it.status == Status.UNKNOWN_ERROR) {
                    _state.value = _state.value.copy(error = "UNKNOWN_ERROR")
                } else if (it.status == Status.VALIDATION_ERROR) {
                    _state.value = _state.value.copy(error = "VALIDATION_ERROR")
                } else if (it.status == Status.UNAUTHORIZED) {
                    _state.value = _state.value.copy(error = "UNAUTHORIZED")
                }
                it.status == Status.UNAUTHORIZED
            }
        )
    }
}

data class OnboardState(
    val username: String = "",
    val error: String = ""
)