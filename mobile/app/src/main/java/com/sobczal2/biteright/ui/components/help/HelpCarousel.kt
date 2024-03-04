package com.sobczal2.biteright.ui.components.help

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.Image
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.pager.HorizontalPager
import androidx.compose.foundation.pager.rememberPagerState
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.Immutable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.painterResource
import kotlin.time.Duration
import kotlin.time.Duration.Companion.seconds

@Immutable
data class HelpCarouselEntry(
    val title: String,
    val description: String,
    val image: Int,
)

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun HelpCarousel(
    entries: List<HelpCarouselEntry>,
    modifier: Modifier = Modifier,
    entryShowDuration: Duration = 5.seconds
) {
    val horizontalPagerState = rememberPagerState {
        entries.size
    }

    HorizontalPager(
        state = horizontalPagerState,
        modifier = modifier,
    ) {
        val entry = entries[it]
        Column(
            modifier = Modifier.fillMaxWidth(),
        ) {
            Text(text = entry.title)
            Image(
                modifier = Modifier.fillMaxWidth(),
                painter = painterResource(entry.image),
                contentDescription = null,
            )
            Text(text = entry.description)
        }
    }
}