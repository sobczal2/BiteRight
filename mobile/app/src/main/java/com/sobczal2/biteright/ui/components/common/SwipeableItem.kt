package com.sobczal2.biteright.ui.components.common

import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.SwipeToDismissBox
import androidx.compose.material3.SwipeToDismissBoxValue
import androidx.compose.material3.rememberSwipeToDismissBoxState
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import coil.request.ImageRequest

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun SwipeableItem(
    modifier: Modifier = Modifier,
    onSwipeLeft: () -> Boolean,
    onSwipeRight: () -> Boolean,
    swipeLeftBackground: @Composable () -> Unit,
    swipeRightBackground: @Composable () -> Unit,
    canSwipeLeft: Boolean = true,
    canSwipeRight: Boolean = true,
    content: @Composable () -> Unit,
) {
    val swipeToDismissBoxState = rememberSwipeToDismissBoxState(
        initialValue = SwipeToDismissBoxValue.Settled,
        confirmValueChange = { newValue ->
            when (newValue) {
                SwipeToDismissBoxValue.EndToStart -> {
                    onSwipeLeft()
                }

                SwipeToDismissBoxValue.StartToEnd -> {
                    onSwipeRight()
                }

                else -> false
            }
        }
    )



    SwipeToDismissBox(
        state = swipeToDismissBoxState,
        enableDismissFromEndToStart = canSwipeLeft,
        enableDismissFromStartToEnd = canSwipeRight,
        backgroundContent = {
            when (swipeToDismissBoxState.targetValue) {
                SwipeToDismissBoxValue.EndToStart -> swipeLeftBackground()
                SwipeToDismissBoxValue.StartToEnd -> swipeRightBackground()
                else -> Unit
            }
        },
        modifier = modifier
    ) {
        content()
    }
}