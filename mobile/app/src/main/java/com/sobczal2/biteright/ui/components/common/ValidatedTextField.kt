package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.ResourceIdOrString
import com.sobczal2.biteright.util.asString

@Composable
fun ValidatedTextField(
    onValueChange: (String) -> Unit,
    error: ResourceIdOrString?,
    modifier: Modifier = Modifier,
    singleLine: Boolean = true,
    imeAction: ImeAction = ImeAction.Next,
    label: @Composable (() -> Unit)? = null,
    initialValue: String = ""
) {
    var text by remember {
        mutableStateOf(
            initialValue
        )
    }
    TextField(
        value = text,
        onValueChange = {
            text = it
            onValueChange(it)
        },
        isError = error != null,
        supportingText = if (error != null) {
            {
                Text(
                    text = error.asString(),
                    color = MaterialTheme.colorScheme.error,
                    style = MaterialTheme.typography.bodySmall,
                    modifier = Modifier
                        .wrapContentSize()
                )
            }
        } else null,
        label = label,
        modifier = modifier,
        singleLine = singleLine,
        keyboardOptions = KeyboardOptions.Default.copy(imeAction = imeAction),
    )
}

@Composable
@Preview(apiLevel = 33)
fun ValidatedTextFieldPreview() {
    BiteRightTheme {
        ValidatedTextField(
            onValueChange = {},
            singleLine = true,
            initialValue = "Initial value",
            label = { Text("Label") },
            error = ResourceIdOrString("Error message")
        )
    }
}