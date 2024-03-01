package com.sobczal2.biteright.routing

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.data.api.requests.users.MeRequest
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class StartRouterViewModel @Inject constructor(
    private val authManager: AuthManager,
    private val userRepository: UserRepository
) : ViewModel() {
    private val _loading = MutableStateFlow(false)
    val loading = _loading.asStateFlow()

    fun navigateAccordingToUserAuthProgress(
        startNavigate: (Routes.StartGraph) -> Unit,
        topLevelNavigate: (Routes) -> Unit
    ) {
        viewModelScope.launch {
            if (!authManager.loggedIn) return@launch

            _loading.value = true

            val meResponse = userRepository.me(MeRequest())

            meResponse.fold(
                {
                    topLevelNavigate(Routes.HomeGraph())
                },
                {
                    if (it is ApiRepositoryError && it.apiErrorCode == 404) {
                        startNavigate(Routes.StartGraph.Onboard)
                    } else {
                        // TODO: Handle other errors
                    }
                }
            )

            _loading.value = false
        }
    }
}