package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Bolt
import androidx.compose.material.icons.filled.FormatListNumbered
import androidx.compose.material.icons.filled.GridOn
import androidx.compose.material.icons.filled.Person
import androidx.compose.material.icons.rounded.Add
import androidx.compose.material3.FloatingActionButton
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.R
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.ui.theme.BiteRightTheme

enum class HomeLayoutTab {
    CURRENT_PRODUCTS,
    ALL_PRODUCTS,
    TEMPLATES,
    PROFILE,
}

@Composable
fun HomeLayout(
    currentTab: HomeLayoutTab,
    handleNavigationEvent: (NavigationEvent) -> Unit,
    floatingActionButton: @Composable (() -> Unit)? = null,
    content: @Composable (PaddingValues) -> Unit,
) {
    Scaffold(
        bottomBar = {
            NavigationBar(
                modifier = Modifier
                    .fillMaxWidth(),
            ) {
                NavigationBarItem(
                    selected = currentTab == HomeLayoutTab.CURRENT_PRODUCTS,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            HomeLayoutTab.CURRENT_PRODUCTS
                        ) { handleNavigationEvent(NavigationEvent.NavigateToCurrentProducts) }
                    },
                    icon = {
                        Icon(
                            Icons.Default.Bolt,
                            contentDescription = stringResource(id = R.string.current),
                        )
                    },
                    label = { Text(stringResource(id = R.string.current)) }
                )
                NavigationBarItem(
                    selected = currentTab == HomeLayoutTab.ALL_PRODUCTS,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            HomeLayoutTab.ALL_PRODUCTS,
                        ) { handleNavigationEvent(NavigationEvent.NavigateToAllProducts) }
                    },
                    icon = {
                        Icon(
                            Icons.Default.FormatListNumbered,
                            contentDescription = stringResource(id = R.string.all),
                        )
                    },
                    label = { Text(stringResource(id = R.string.all)) }
                )
                NavigationBarItem(
                    selected = currentTab == HomeLayoutTab.TEMPLATES,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            HomeLayoutTab.TEMPLATES,
                        ) { handleNavigationEvent(NavigationEvent.NavigateToTemplates) }
                    },
                    icon = {
                        Icon(
                            Icons.Default.GridOn,
                            contentDescription = stringResource(id = R.string.templates),
                        )
                    },
                    label = { Text(stringResource(id = R.string.templates)) }
                )
                NavigationBarItem(
                    selected = currentTab == HomeLayoutTab.PROFILE,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            HomeLayoutTab.PROFILE,
                        ) { handleNavigationEvent(NavigationEvent.NavigateToProfile) }
                    },
                    icon = {
                        Icon(
                            Icons.Default.Person,
                            contentDescription = stringResource(id = R.string.profile),
                        )
                    },
                    label = { Text(stringResource(id = R.string.profile)) }
                )
            }
        },
        floatingActionButton = { floatingActionButton?.let { it() } },
    ) {
        content(it)
    }
}

fun navigateIfNotCurrentTab(
    currentTab: HomeLayoutTab,
    tab: HomeLayoutTab,
    navigate: () -> Unit,
) {
    if (currentTab != tab) {
        navigate()
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun HomeLayoutPreview() {
    BiteRightTheme {
        HomeLayout(
            currentTab = HomeLayoutTab.CURRENT_PRODUCTS,
            content = {},
            handleNavigationEvent = {},
            floatingActionButton = {
                FloatingActionButton(onClick = { }) {
                    Icon(Icons.Rounded.Add, contentDescription = "Add")
                }
            }
        )
    }
}