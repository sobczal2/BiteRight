package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.dimension


@Composable
fun ComingSoonBanner(
    onBackClicked: () -> Unit
) {
    Scaffold(

    ) {paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.Center
        ) {
            BiteRightLogo(
                modifier = Modifier
                    .size(200.dp)
            )
            Text(
                text = stringResource(id = R.string.coming_soon),
                style = MaterialTheme.typography.displayMedium,
                textAlign = TextAlign.Center,
            )
            OutlinedButton(
                onClick = onBackClicked,
                shape = MaterialTheme.shapes.extraSmall,
                modifier = Modifier
                    .fillMaxWidth(0.7f)
                    .padding(top = MaterialTheme.dimension.md),
            ) {
                Text(
                    text = stringResource(id = R.string.back),
                )
            }
        }
    }
}