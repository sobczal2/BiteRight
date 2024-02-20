package com.sobczal2.biteright.events

sealed class ProfileScreenEvent {
    data object OnLogoutClick : ProfileScreenEvent()
}