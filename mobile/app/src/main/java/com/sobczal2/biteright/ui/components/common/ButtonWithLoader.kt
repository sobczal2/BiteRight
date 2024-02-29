package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonColors
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.Dp
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview

@Composable
fun ButtonWithLoader(
    onClick: () -> Unit,
    loading: Boolean,
    modifier: Modifier = Modifier,
    shape: Shape = MaterialTheme.shapes.extraSmall,
    colors: ButtonColors = ButtonDefaults.buttonColors(),
    indicatorHeight: Dp = MaterialTheme.dimension.md,
    content: @Composable () -> Unit
) {
    Button(
        onClick = onClick,
        modifier = modifier,
        enabled = !loading,
        shape = shape,
        colors = colors,
    ) {
        if (loading) {
            CircularProgressIndicator(
                color = MaterialTheme.colorScheme.onPrimary,
                modifier = Modifier.size(indicatorHeight)
            )
        } else {
            content()
        }
    }
}

@Composable
@BiteRightPreview
fun ButtonWithLoaderPreview() {
    BiteRightTheme {
        ButtonWithLoader(
            onClick = {},
            loading = false,
        ) {
            Text("Button")
        }
    }
}

@Composable
@BiteRightPreview
fun ButtonWithLoaderLoadingPreview() {
    BiteRightTheme {

        ButtonWithLoader(
            onClick = {},
            loading = true,
        ) {
            Text("Button")
        }
    }
}