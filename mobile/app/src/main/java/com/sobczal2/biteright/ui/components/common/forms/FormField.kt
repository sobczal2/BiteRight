package com.sobczal2.biteright.ui.components.common.forms

import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier

interface FormFieldState<T> {
    val value: T
    val error: String?
}

sealed class FormFieldEvents<T> {
    data class OnValueChange<T>(val value: T) : FormFieldEvents<T>()
}