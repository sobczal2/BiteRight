package com.sobczal2.biteright.ui.components.common.forms

import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Shape

data class TextFormFieldState(
    override val value: String = "",
    override val error: String? = null
) : FormFieldState<String>

typealias TextFormFieldEvents = FormFieldEvents<String>

data class TextFormFieldOptions(
    val label: @Composable (() -> Unit)? = null,
    val singleLine: Boolean = true,
    val minLines: Int = 1,
    val maxLines: Int = 1,
    val keyboardOptions: KeyboardOptions = KeyboardOptions.Default,
    val shape: Shape? = null,
    val leadingIcon: @Composable (() -> Unit)? = null,
    val trailingIcon: @Composable (() -> Unit)? = null,
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
        minLines = options.minLines,
        maxLines = options.maxLines,
        keyboardOptions = options.keyboardOptions,
        shape = options.shape ?: MaterialTheme.shapes.small,
        leadingIcon = options.leadingIcon,
        trailingIcon = options.trailingIcon,
    )
}