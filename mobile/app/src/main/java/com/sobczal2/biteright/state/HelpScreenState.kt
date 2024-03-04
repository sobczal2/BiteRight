package com.sobczal2.biteright.state

import com.sobczal2.biteright.ui.components.help.HelpCarouselEntry

data class HelpScreenState(
    val entries: List<HelpCarouselEntry> = emptyList(),
    override val ongoingLoadingActions: Set<String> = emptySet()
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}