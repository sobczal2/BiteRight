package com.sobczal2.biteright.ui.layouts

import androidx.compose.foundation.layout.PaddingValues
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Bolt
import androidx.compose.material.icons.filled.FormatListNumbered
import androidx.compose.material.icons.filled.GridOn
import androidx.compose.material.icons.filled.Person
import androidx.compose.material3.Icon
import androidx.compose.material3.NavigationBar
import androidx.compose.material3.NavigationBarItem
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.ui.components.products.AddProductActionButton
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.BiteRightPreview

@Composable
fun HomeLayout(
    currentRoute: Routes?,
    homeNavigate: (Routes.HomeGraph) -> Unit,
    topLevelNavigate: (Routes) -> Unit,
    content: @Composable (PaddingValues) -> Unit,
) {
    Scaffold(
        bottomBar = {
            NavigationBar(
                modifier = Modifier
                    .fillMaxWidth(),
            ) {
                NavigationBarItem(
                    selected = currentRoute == Routes.HomeGraph.CurrentProducts,
                    onClick = {
                        homeNavigate(Routes.HomeGraph.CurrentProducts)
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
                    selected = currentRoute == Routes.HomeGraph.AllProducts,
                    onClick = {
                        homeNavigate(Routes.HomeGraph.AllProducts)
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
                    selected = currentRoute == Routes.HomeGraph.Templates,
                    onClick = {
                        homeNavigate(Routes.HomeGraph.Templates)
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
                    selected = currentRoute == Routes.HomeGraph.Profile,
                    onClick = {
                        homeNavigate(Routes.HomeGraph.Profile)
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
        floatingActionButton = {
            if (
                currentRoute == Routes.HomeGraph.CurrentProducts
                || currentRoute == Routes.HomeGraph.AllProducts
            ) {
                AddProductActionButton {
                    topLevelNavigate(Routes.CreateProduct)
                }
            }
        },
    ) { paddingValues ->
        content(paddingValues)
    }
}

@Composable
@BiteRightPreview
fun HomeLayoutPreview() {
    BiteRightTheme {
        HomeLayout(
            currentRoute = Routes.HomeGraph.CurrentProducts,
            content = {},
            homeNavigate = {},
            topLevelNavigate = {},
        )
    }
}