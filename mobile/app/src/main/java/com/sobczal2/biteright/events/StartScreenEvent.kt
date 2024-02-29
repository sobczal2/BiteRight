package com.sobczal2.biteright.events

import com.sobczal2.biteright.dto.currencies.CurrencyDto
import java.util.TimeZone

sealed class StartScreenEvent {
    data class OnUsernameChange(val value: String) : StartScreenEvent()
    data class OnNextClick(val onSuccess: () -> Unit) : StartScreenEvent()
    data class OnCurrencyChange(val value: CurrencyDto) : StartScreenEvent()
    data class OnTimeZoneChange(val value: TimeZone) : StartScreenEvent()
}