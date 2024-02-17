package com.sobczal2.biteright.events

import android.content.Context
import dagger.hilt.android.qualifiers.ActivityContext

sealed class WelcomeScreenEvent {
    data class OnGetStartedClick(@ActivityContext val context: Context, val onSuccess: () -> Unit) :
        WelcomeScreenEvent()
}