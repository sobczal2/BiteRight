package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.ui.components.currencies.CurrencyFormFieldState
import com.sobczal2.biteright.ui.components.users.TimeZoneFormFieldState
import java.util.TimeZone

data class EditProfileScreenState(
    val currencyFieldState: CurrencyFormFieldState = CurrencyFormFieldState(CurrencyDto.Empty),
    val timeZoneFieldState: TimeZoneFormFieldState = TimeZoneFormFieldState(TimeZone.getDefault()),
    val formSubmitting: Boolean = false,
    val startingCurrencies: PaginatedList<CurrencyDto>? = null,
    val availableTimeZones: List<TimeZone> = TimeZone.getAvailableIDs().map { TimeZone.getTimeZone(it) }.sortedBy { it.id },
    override val ongoingLoadingActions: Set<String> = setOf(),
) : ScreenState {
    fun isLoading(): Boolean = ongoingLoadingActions.isNotEmpty()
}
