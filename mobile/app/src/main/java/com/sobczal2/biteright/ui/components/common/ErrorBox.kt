package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Warning
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview

@Composable
fun ErrorBox(
    error: String?,
    modifier: Modifier = Modifier
) {
    if (error == null) return
    Card(
        colors = CardDefaults.cardColors(
            containerColor = MaterialTheme.colorScheme.errorContainer,
        ),
        modifier = modifier
            .fillMaxWidth()
    ) {
        Row(
            modifier = Modifier.padding(
                start = MaterialTheme.dimension.sm,
                top = MaterialTheme.dimension.sm,
                bottom = MaterialTheme.dimension.sm
            ),
            verticalAlignment = Alignment.CenterVertically,
        ) {
            Icon(
                imageVector = Icons.Filled.Warning,
                tint = MaterialTheme.colorScheme.error,
                contentDescription = "Error icon",
                modifier = Modifier
                    .size(MaterialTheme.dimension.md)
            )
            Text(
                text = error,
                textAlign = TextAlign.Center,
                style = MaterialTheme.typography.labelMedium,
                color = MaterialTheme.colorScheme.error,
                modifier = Modifier.padding(start = MaterialTheme.dimension.sm)
            )
        }
    }
}

@Composable
@BiteRightPreview
fun ErrorBoxPreview() {
    BiteRightTheme {
        ErrorBox("Something went wrong!")
    }
}

@Composable
fun ErrorBox(
    errors: Map<String, List<String>>?,
    modifier: Modifier = Modifier
) {
    if (errors == null) return
    Card(
        colors = CardDefaults.cardColors(
            containerColor = MaterialTheme.colorScheme.errorContainer,
        ),
        modifier = modifier
            .fillMaxWidth()
    ) {
        errors.forEach { (key, value) ->
            Row {
                Column(
                    modifier = Modifier
                        .padding(
                            start = MaterialTheme.dimension.sm,
                            top = MaterialTheme.dimension.sm
                        ),
                ) {
                    Icon(
                        imageVector = Icons.Filled.Warning,
                        tint = MaterialTheme.colorScheme.error,
                        contentDescription = "Error icon",
                        modifier = Modifier.size(MaterialTheme.dimension.md)
                    )
                }
                Column(
                    modifier = Modifier.padding(MaterialTheme.dimension.sm),
                ) {
                    Row(
                        verticalAlignment = Alignment.CenterVertically
                    ) {
                        Text(
                            text = "$key:",
                            style = MaterialTheme.typography.labelMedium.copy(
                                fontWeight = FontWeight.Bold
                            ),
                            color = MaterialTheme.colorScheme.error
                        )
                    }
                    value.forEach {
                        Text(
                            text = "- $it",
                            style = MaterialTheme.typography.labelMedium,
                            color = MaterialTheme.colorScheme.error
                        )
                    }
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun ErrorBox2Preview() {
    BiteRightTheme {
        ErrorBox(
            mapOf(
                "Username" to listOf("This username is already taken!", "Username is too short!"),
                "Password" to listOf("Password is too short!")
            )
        )
    }
}