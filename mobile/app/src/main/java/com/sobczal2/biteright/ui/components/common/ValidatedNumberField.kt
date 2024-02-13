package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.text.input.KeyboardType
import com.sobczal2.biteright.util.ResourceIdOrString
import com.sobczal2.biteright.util.asString

@Composable
fun ValidatedNumberField(
    onValueChange: (Double?) -> Unit,
    error: ResourceIdOrString?,
    modifier: Modifier = Modifier,
    imeAction: ImeAction = ImeAction.Next,
    label: @Composable (() -> Unit)? = null,
    initialValue: Double? = null,
    shape: Shape = TextFieldDefaults.shape
) {
    var text by remember {
        mutableStateOf(
            initialValue?.toString() ?: ""
        )
    }
    val number = text.toDoubleOrNull()

    LaunchedEffect(
        key1 = number,
        block = {
            onValueChange(number)
        }
    )

    TextField(
        value = text,
        onValueChange = {
            text = it
        },
        isError = error != null,
        supportingText = {
            error?.let {
                Text(
                    text = it.asString(),
                    color = MaterialTheme.colorScheme.error,
                    style = MaterialTheme.typography.bodySmall,
                    modifier = Modifier
                        .wrapContentSize()
                )
            }
        },
        label = label,
        modifier = modifier,
        singleLine = true,
        keyboardOptions = KeyboardOptions.Default.copy(
            imeAction = imeAction,
            keyboardType = KeyboardType.Number
        ),
        shape = shape,
    )
}