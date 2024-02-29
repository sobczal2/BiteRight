package com.sobczal2.biteright.events

import com.sobczal2.biteright.dto.currencies.CurrencyDto
import java.util.TimeZone

sealed class EditProfileScreenEvent {
    data class OnSubmitClick(val onSuccess: () -> Unit) : EditProfileScreenEvent()
    data class OnCurrencyChange(val value: CurrencyDto) : EditProfileScreenEvent()
    data class OnTimeZoneChange(val value: TimeZone) : EditProfileScreenEvent()
}