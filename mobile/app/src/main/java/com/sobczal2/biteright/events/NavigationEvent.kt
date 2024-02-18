package com.sobczal2.biteright.events

sealed class NavigationEvent {
    data object NavigateToWelcome : NavigationEvent()
    data object NavigateToStart : NavigationEvent()
    data object NavigateToCurrentProducts : NavigationEvent()
    data object NavigateToAllProducts : NavigationEvent()
    data object NavigateToTemplates : NavigationEvent()
    data object NavigateToProfile : NavigationEvent()
    data object NavigateToCreateProduct : NavigationEvent()
}