package com.sobczal2.biteright.events

import com.sobczal2.biteright.dto.currencies.CurrencyDto
import java.util.TimeZone

sealed class OnboardScreenEvent {
    data class OnUsernameChange(val value: String) : OnboardScreenEvent()
    data class OnNextClick(val onSuccess: () -> Unit) : OnboardScreenEvent()
    data class OnCurrencyChange(val value: CurrencyDto) : OnboardScreenEvent()
    data class OnTimeZoneChange(val value: TimeZone) : OnboardScreenEvent()
}