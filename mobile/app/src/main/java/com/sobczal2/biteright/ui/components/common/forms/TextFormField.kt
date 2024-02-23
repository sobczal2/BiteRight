package com.sobczal2.biteright.ui.components.common.forms

import androidx.compose.foundation.interaction.InteractionSource
import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.LocalTextStyle
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TextFieldColors
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.ui.theme.extraSmallTop

data class TextFormFieldState(
    override var value: String = "",
    override val error: String? = null
) : FormFieldState<String>

data class TextFormFieldOptions(
    val label: @Composable (() -> Unit)? = null,
    val singleLine: Boolean = true,
    val minLines: Int = 1,
    val maxLines: Int = 1,
    val keyboardOptions: KeyboardOptions = KeyboardOptions.Default,
    val shape: Shape? = null,
    val leadingIcon: @Composable (() -> Unit)? = null,
    val trailingIcon: @Composable (() -> Unit)? = null,
    val enabled: Boolean = true,
    val readOnly: Boolean = false,
    val colors: TextFieldColors? = null,
    val interactionSource: MutableInteractionSource? = null,
    val textStyle: TextStyle? = null,
)

@Composable
fun TextFormField(
    state: TextFormFieldState,
    onChange: (String) -> Unit,
    options: TextFormFieldOptions,
    modifier: Modifier = Modifier,
) {
    TextField(
        value = state.value,
        onValueChange = {
            onChange(it)
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
        shape = options.shape ?: MaterialTheme.shapes.extraSmallTop,
        leadingIcon = options.leadingIcon,
        trailingIcon = options.trailingIcon,
        enabled = options.enabled,
        readOnly = options.readOnly,
        colors = options.colors ?: TextFieldDefaults.colors(),
        interactionSource = options.interactionSource ?: remember { MutableInteractionSource() },
        textStyle = options.textStyle ?: LocalTextStyle.current,
    )
}