package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.rounded.Bolt
import androidx.compose.material.icons.rounded.FormatListNumbered
import androidx.compose.material.icons.rounded.GridOn
import androidx.compose.material.icons.rounded.Person
import androidx.compose.material3.BottomAppBar
import androidx.compose.material3.Icon
import androidx.compose.material3.IconButton
import androidx.compose.material3.Scaffold
import androidx.compose.runtime.Composable
import androidx.compose.ui.tooling.preview.Preview
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
            BottomAppBar(
                actions = {
                    IconButton(
                        onClick = mainAppLayoutActions.onCurrentProductsClick,
                        enabled = currentTab != MainAppLayoutTab.CURRENT_PRODUCTS
                    ) {
                        Icon(Icons.Rounded.Bolt, contentDescription = "Current")
                    }
                    IconButton(
                        onClick = mainAppLayoutActions.onAllProductsClick,
                        enabled = currentTab != MainAppLayoutTab.ALL_PRODUCTS
                    ) {
                        Icon(Icons.Rounded.FormatListNumbered, contentDescription = "All")
                    }
                    IconButton(
                        onClick = mainAppLayoutActions.onTemplatesClick,
                        enabled = currentTab != MainAppLayoutTab.TEMPLATES
                    ) {
                        Icon(Icons.Rounded.GridOn, contentDescription = "Templates")
                    }
                    IconButton(
                        onClick = mainAppLayoutActions.onProfileClick,
                        enabled = currentTab != MainAppLayoutTab.PROFILE
                    ) {
                        Icon(Icons.Rounded.Person, contentDescription = "Settings")
                    }
                }
            )
        },
        floatingActionButton = { floatingActionButton?.let { it() } },
    ) {
        content(it)
    }
}

@Composable
@Preview
@Preview("Dark Theme", uiMode = android.content.res.Configuration.UI_MODE_NIGHT_YES)
fun MainAppLayoutPreview() {
    BiteRightTheme {
        MainAppLayout(
            currentTab = MainAppLayoutTab.CURRENT_PRODUCTS,
            content = {}
        )
    }
}