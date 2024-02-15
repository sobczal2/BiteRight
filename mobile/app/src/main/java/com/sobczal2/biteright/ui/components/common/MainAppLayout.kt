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
import com.sobczal2.biteright.ui.theme.BiteRightTheme

enum class MainAppLayoutTab {
    CURRENT_PRODUCTS,
    ALL_PRODUCTS,
    TEMPLATES,
    PROFILE,
}

data class MainAppLayoutActions(
    val onCurrentProductsClick: () -> Unit = {},
    val onAllProductsClick: () -> Unit = {},
    val onTemplatesClick: () -> Unit = {},
    val onProfileClick: () -> Unit = {},
)

@Composable
fun MainAppLayout(
    currentTab: MainAppLayoutTab,
    mainAppLayoutActions: MainAppLayoutActions = MainAppLayoutActions(),
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
                    selected = currentTab == MainAppLayoutTab.CURRENT_PRODUCTS,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            MainAppLayoutTab.CURRENT_PRODUCTS,
                            mainAppLayoutActions.onCurrentProductsClick
                        )
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
                    selected = currentTab == MainAppLayoutTab.ALL_PRODUCTS,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            MainAppLayoutTab.ALL_PRODUCTS,
                            mainAppLayoutActions.onAllProductsClick
                        )
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
                    selected = currentTab == MainAppLayoutTab.TEMPLATES,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            MainAppLayoutTab.TEMPLATES,
                            mainAppLayoutActions.onTemplatesClick
                        )
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
                    selected = currentTab == MainAppLayoutTab.PROFILE,
                    onClick = {
                        navigateIfNotCurrentTab(
                            currentTab,
                            MainAppLayoutTab.PROFILE,
                            mainAppLayoutActions.onProfileClick
                        )
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
    currentTab: MainAppLayoutTab,
    tab: MainAppLayoutTab,
    navigate: () -> Unit,
) {
    if (currentTab != tab) {
        navigate()
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun MainAppLayoutPreview() {
    BiteRightTheme {
        MainAppLayout(
            currentTab = MainAppLayoutTab.CURRENT_PRODUCTS,
            content = {},
            floatingActionButton = {
                FloatingActionButton(onClick = { }) {
                    Icon(Icons.Rounded.Add, contentDescription = "Add")
                }
            }
        )
    }
}