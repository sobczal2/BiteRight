package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProfileScreenEvent
import com.sobczal2.biteright.state.ProfileScreenState
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScreenLoader
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.ProfileViewModel
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter
import java.util.UUID

@Composable
fun ProfileScreen(
    viewModel: ProfileViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    ScreenLoader(loading = state.value.globalLoading) {
        ProfileScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun ProfileScreenContent(
    state: ProfileScreenState = ProfileScreenState(),
    sendEvent: (ProfileScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    HomeLayout(
        currentTab = HomeLayoutTab.PROFILE,
        handleNavigationEvent = handleNavigationEvent,
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.xxl),
            verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
        ) {
            Text(
                text = stringResource(id = R.string.profile),
                style = MaterialTheme.typography.displayMedium
            )
            TextField(value = state.user?.username ?: "", onValueChange = { }, label = {
                Text(stringResource(id = R.string.username))
            }, modifier = Modifier.fillMaxWidth(), enabled = false
            )
            TextField(value = state.user?.email ?: "", onValueChange = { }, label = {
                Text(
                    stringResource(id = R.string.email)
                )
            }, modifier = Modifier.fillMaxWidth(), enabled = false

            )
            TextField(value = state.user?.profile?.currencyId.toString(),
                onValueChange = { },
                label = {
                    Text(
                        stringResource(id = R.string.currency)
                    )
                },
                modifier = Modifier.fillMaxWidth(),
                enabled = false
            )
            TextField(value = state.user?.profile?.timeZoneId ?: "", onValueChange = { }, label = {
                Text(
                    stringResource(id = R.string.time_zone)
                )
            }, modifier = Modifier.fillMaxWidth(), enabled = false
            )
            Text(
                text = "${stringResource(id = R.string.joined_at)}: ${
                    state.user?.joinedAt?.format(
                        DateTimeFormatter.ISO_DATE
                    )
                }", style = MaterialTheme.typography.bodyLarge
            )
            Button(onClick = {
                sendEvent(ProfileScreenEvent.OnLogoutClick)
            }) {
                Text(stringResource(id = R.string.logout))
            }
        }
    }
}

@Composable
@Preview(apiLevel = 33)
@Preview("Dark Theme", apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
fun ProfileScreenPreview() {
    BiteRightTheme {
        ProfileScreenContent(
            state = ProfileScreenState(
                user = UserDto(
                    id = UUID.randomUUID(),
                    identityId = "identityId",
                    username = "username",
                    email = "email",
                    joinedAt = LocalDateTime.now(),
                    profile = com.sobczal2.biteright.dto.users.ProfileDto(
                        currencyId = UUID.randomUUID(), timeZoneId = "timeZoneId"
                    )
                )
            ),
        )
    }
}