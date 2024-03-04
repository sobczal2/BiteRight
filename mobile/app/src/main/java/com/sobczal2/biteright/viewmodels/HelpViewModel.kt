package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.HelpScreenEvent
import com.sobczal2.biteright.state.HelpScreenState
import com.sobczal2.biteright.ui.components.help.HelpCarouselEntry
import com.sobczal2.biteright.util.StringProvider
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class HelpViewModel @Inject constructor(
    stringProvider: StringProvider
) : ViewModel() {

    private val _state = MutableStateFlow(
        HelpScreenState(
            entries = listOf(
                HelpCarouselEntry(
                    title = stringProvider.getString(R.string.help_step_1_title),
                    description = stringProvider.getString(R.string.help_step_1_description),
                ),
                HelpCarouselEntry(
                    title = stringProvider.getString(R.string.help_step_2_title),
                    description = stringProvider.getString(R.string.help_step_2_description),
                ),
                HelpCarouselEntry(
                    title = stringProvider.getString(R.string.help_step_3_title),
                    description = stringProvider.getString(R.string.help_step_3_description),
                ),
            )
        )
    )

    val state = _state.asStateFlow()

    private val _events = Channel<HelpScreenEvent>()
    private val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch {
                events.collect { event ->
                    handleEvent(event)
                }
            }
        }
    }

    fun sendEvent(event: HelpScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: HelpScreenEvent) {
        when (event) {
            else -> {}
        }
    }
}