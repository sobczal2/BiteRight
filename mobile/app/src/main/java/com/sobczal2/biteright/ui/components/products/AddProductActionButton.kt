package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.layout.padding
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material3.ExtendedFloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.dimension

@Composable
fun AddProductActionButton(
    onClick: () -> Unit,
) {
    ExtendedFloatingActionButton(
        onClick = onClick,
    ) {
        Icon(
            imageVector = Icons.Default.Add,
            contentDescription = stringResource(id = R.string.add_product),
            modifier = Modifier.padding(end = MaterialTheme.dimension.xs)
        )
        Text(text = stringResource(id = R.string.add_product))
    }
}