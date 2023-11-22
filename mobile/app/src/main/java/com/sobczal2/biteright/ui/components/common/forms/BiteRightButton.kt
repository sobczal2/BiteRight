package com.sobczal2.biteright.ui.components.common.forms

import android.content.res.Configuration.UI_MODE_NIGHT_NO
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.material3.Button
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.ui.theme.BiteRightTheme

@Composable
fun BiteRightButton(
    text: String,
    loading: Boolean,
    enabled: Boolean,
    onClick: () -> Unit
) {
    if (loading) {
        CircularProgressIndicator(
            modifier = Modifier
                .height(48.dp)
        )
    } else {
        Button(
            onClick = { onClick() },
            enabled = enabled,
            modifier = Modifier
                .fillMaxWidth()
                .height(48.dp),
        ) {
            Text(
                text = text, style = MaterialTheme.typography.bodyLarge
            )
        }
    }
}

@Preview(uiMode = UI_MODE_NIGHT_NO)
@Composable
fun SubmitButtonPreview() {
    BiteRightTheme {
        BiteRightButton(
            text = "Sign up",
            loading = false,
            enabled = true,
            onClick = {}
        )
    }
}