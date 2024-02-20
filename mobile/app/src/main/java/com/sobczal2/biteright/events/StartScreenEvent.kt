package com.sobczal2.biteright.events

sealed class StartScreenEvent {
    data class OnUsernameChange(val value: String) : StartScreenEvent()
    data class OnNextClick(val onSuccess: () -> Unit) : StartScreenEvent()
}