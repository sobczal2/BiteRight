package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Info
import androidx.compose.material.icons.filled.Warning
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.contentColorFor
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp

@Composable
fun MessageBox(
    message: String,
    messageType: MessageType,
    modifier: Modifier = Modifier
) {
    val backgroundColor = when (messageType) {
        MessageType.Error -> MaterialTheme.colorScheme.errorContainer
        MessageType.Info -> MaterialTheme.colorScheme.primary
    }

    val icon = when (messageType) {
        MessageType.Error -> Icons.Default.Warning
        MessageType.Info -> Icons.Default.Info
    }

    Surface(
        modifier = modifier
            .padding(16.dp)
            .clip(RoundedCornerShape(8.dp)),
        contentColor = contentColorFor(backgroundColor),
        color = backgroundColor
    ) {
        Row(
            modifier = Modifier
                .padding(16.dp)
                .fillMaxWidth(),
            verticalAlignment = Alignment.CenterVertically
        ) {
            Icon(
                imageVector = icon,
                contentDescription = null,
                modifier = Modifier.size(24.dp)
            )
            Spacer(modifier = Modifier.width(8.dp))
            Text(
                text = message,
                fontSize = 16.sp,
                fontWeight = FontWeight.Medium,
            )
        }
    }
}

enum class MessageType {
    Error,
    Info
}

@Composable
@Preview
fun ErrorMessagePreview() {
    MaterialTheme {
        MessageBox(message = "This is an error message", messageType = MessageType.Error)
    }
}

@Composable
@Preview
fun InfoMessagePreview() {
    MaterialTheme {
        MessageBox(message = "This is an info message", messageType = MessageType.Info)
    }
}