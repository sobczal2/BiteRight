package com.sobczal2.biteright.state

interface ScreenState {
    val ongoingLoadingActions: Set<String>
    val globalError: String?
}