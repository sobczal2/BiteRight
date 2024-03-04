package com.sobczal2.biteright.ui.components.help

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.pager.HorizontalPager
import androidx.compose.foundation.pager.rememberPagerState
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.Immutable
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextAlign
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import kotlinx.coroutines.launch
import kotlin.time.Duration
import kotlin.time.Duration.Companion.seconds

@Immutable
data class HelpCarouselEntry(
    val title: String,
    val description: String,
)

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun HelpCarousel(
    entries: List<HelpCarouselEntry>,
    modifier: Modifier = Modifier,
    entryShowDuration: Duration = 5.seconds,
    onContinue: () -> Unit = {},
) {
    val coroutineScope = rememberCoroutineScope()

    val horizontalPagerState = rememberPagerState {
        entries.size
    }

    HorizontalPager(
        state = horizontalPagerState,
        modifier = modifier,
    ) {
        val entry = entries[it]
        Column(
            modifier = Modifier
                .fillMaxSize()
                .verticalScroll(rememberScrollState())
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.SpaceBetween
        ) {
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = MaterialTheme.dimension.xl),
                verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md),
            ) {
                Text(
                    text = entry.title,
                    modifier = Modifier.fillMaxWidth(),
                    style = MaterialTheme.typography.displaySmall.copy(
                        textAlign = TextAlign.Center
                    )
                )
                Text(
                    text = entry.description,
                    modifier = Modifier.fillMaxWidth(),
                    style = MaterialTheme.typography.bodyLarge.copy(
                        textAlign = TextAlign.Justify
                    )
                )
            }

            if (it == entries.size - 1) {
                Button(
                    onClick = onContinue,
                    modifier = Modifier.fillMaxWidth(),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = "Continue")
                }
            } else {
                Button(
                    onClick = {
                        coroutineScope.launch {
                            horizontalPagerState.animateScrollToPage(it + 1)
                        }
                    },
                    modifier = Modifier.fillMaxWidth(),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = "Next")
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun HelpCarouselPreview() {
    val entries = listOf(
        HelpCarouselEntry(
            title = "Title 1",
            description = "Description 1",
        ),
        HelpCarouselEntry(
            title = "Title 2",
            description = "Description 2",
        ),
        HelpCarouselEntry(
            title = "Title 3",
            description = "Description 3",
        ),
    )

    HelpCarousel(
        entries = entries,
    )
}