package com.sobczal2.biteright.ui.components

import androidx.compose.foundation.layout.wrapContentSize
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.input.ImeAction
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.ResourceIdOrString
import com.sobczal2.biteright.util.asString

@Composable
fun ValidatedTextField(
    value: String,
    onValueChange: (String) -> Unit,
    error: ResourceIdOrString?,
    singleLine: Boolean,
    modifier: Modifier = Modifier,
    imeAction: ImeAction = ImeAction.Next,
    label: @Composable (() -> Unit)? = null,
) {
    TextField(
        value = value,
        onValueChange = onValueChange,
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
        singleLine = singleLine,
        keyboardOptions = KeyboardOptions.Default.copy(imeAction = imeAction),
    )
}

@Composable
@Preview
fun ValidatedTextFieldPreview() {
    BiteRightTheme {
        ValidatedTextField(
            value = "Hello",
            onValueChange = {},
            singleLine = true,
            error = ResourceIdOrString("This is a as dasd asd as  asdasd asd as dsadasd sa das das ine to see how it looks like."),
        )
    }
}