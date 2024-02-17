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

data class TextFormFieldState(
    override val value: String = "",
    override val error: String? = null
) : FormFieldState<String>

typealias TextFormFieldEvents = FormFieldEvents<String>

data class TextFormFieldOptions(
    val label: @Composable (() -> Unit)? = null,
    val singleLine: Boolean = true,
    val keyboardOptions: KeyboardOptions = KeyboardOptions.Default,
)

@Composable
fun TextFormField(
    state: TextFormFieldState,
    onEvent: (TextFormFieldEvents) -> Unit,
    options: TextFormFieldOptions,
    modifier: Modifier = Modifier,
) {
    TextField(
        value = state.value,
        onValueChange = {
            onEvent(FormFieldEvents.OnValueChange(it))
        },
        isError = state.error != null,
        supportingText = if (state.error != null) {
            {
                Text(
                    text = state.error,
                    color = MaterialTheme.colorScheme.error,
                    style = MaterialTheme.typography.bodySmall,
                    modifier = Modifier
                        .wrapContentSize()
                )
            }
        } else null,
        label = options.label,
        modifier = modifier,
        singleLine = options.singleLine,
        keyboardOptions = options.keyboardOptions,
    )
}