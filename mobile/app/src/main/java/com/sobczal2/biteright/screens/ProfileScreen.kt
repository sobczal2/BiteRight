package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextField
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.users.ProfileDto
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProfileScreenEvent
import com.sobczal2.biteright.state.ProfileScreenState
import com.sobczal2.biteright.ui.components.common.DisplayPair
import com.sobczal2.biteright.ui.components.common.HomeLayout
import com.sobczal2.biteright.ui.components.common.HomeLayoutTab
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.humanize
import com.sobczal2.biteright.viewmodels.ProfileViewModel
import java.time.Instant
import java.util.TimeZone
import java.util.UUID

@Composable
fun ProfileScreen(
    viewModel: ProfileViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    ScaffoldLoader(
        loading = state.value.isLoading(),
    ) {
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
    val user = state.user ?: return
    HomeLayout(
        currentTab = HomeLayoutTab.PROFILE,
        handleNavigationEvent = handleNavigationEvent,
    ) { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
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
                    text = stringResource(id = R.string.profile),
                    style = MaterialTheme.typography.displaySmall.copy(
                        textAlign = TextAlign.Center
                    ),
                    modifier = Modifier.fillMaxWidth()
                )
                DisplayPair(
                    label = stringResource(id = R.string.username),
                    value = user.username,
                    modifier = Modifier.fillMaxWidth()
                )
                DisplayPair(
                    label = stringResource(id = R.string.email),
                    value = user.email,
                    modifier = Modifier.fillMaxWidth()
                )
                DisplayPair(
                    label = stringResource(id = R.string.preferred_currency),
                    value = user.profile.currency.name,
                    modifier = Modifier.fillMaxWidth()
                )
                DisplayPair(
                    label = stringResource(id = R.string.time_zone),
                    value = user.profile.timeZoneId,
                    modifier = Modifier.fillMaxWidth()
                )
                DisplayPair(
                    label = stringResource(id = R.string.joined_at),
                    value = user.joinedAt.humanize(TimeZone.getTimeZone(user.profile.timeZoneId)),
                    modifier = Modifier.fillMaxWidth()
                )
            }
            Column(
                modifier = Modifier.fillMaxWidth(),
            ) {
                Button(
                    shape = MaterialTheme.shapes.extraSmall,
                    onClick = {
                        handleNavigationEvent(NavigationEvent.NavigateToEditProfile)
                    },
                    modifier = Modifier
                        .fillMaxWidth(),
                ) {
                    Text(
                        text = stringResource(id = R.string.edit),
                    )
                }

                Button(
                    colors = ButtonDefaults.buttonColors().copy(
                        containerColor = MaterialTheme.colorScheme.error,
                        contentColor = MaterialTheme.colorScheme.onError
                    ),
                    shape = MaterialTheme.shapes.extraSmall,
                    onClick = {
                        sendEvent(ProfileScreenEvent.OnLogoutClick)
                    },
                    modifier = Modifier
                        .fillMaxWidth(),
                ) {
                    Text(
                        text = stringResource(id = R.string.logout),
                    )
                }
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
                    joinedAt = Instant.now(),
                    profile = ProfileDto(
                        currency = CurrencyDto(
                            id = UUID.randomUUID(),
                            code = "USD",
                            symbol = "$",
                            name = "US Dollar"
                        ),
                        timeZoneId = "Europe/Warsaw"
                    )
                )
            ),
        )
    }
}