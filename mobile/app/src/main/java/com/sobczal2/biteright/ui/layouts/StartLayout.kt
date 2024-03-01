package com.sobczal2.biteright.ui.layouts

import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.theme.dimension

@Composable
fun StartLayout(
    content: @Composable () -> Unit
) {
    Scaffold {paddingValues ->
        Column(
            horizontalAlignment = Alignment.CenterHorizontally,
            modifier = Modifier
                .padding(paddingValues)
                .padding(top = MaterialTheme.dimension.xxxl),
        ) {
            BiteRightLogo(
                modifier = Modifier.size(300.dp)
            )
            content()
        }
    }
}