package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.input.ImeAction
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.events.OnboardScreenEvent
import com.sobczal2.biteright.routing.Routes
import com.sobczal2.biteright.state.OnboardScreenState
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.SurfaceLoader
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.currencies.CurrencyFormField
import com.sobczal2.biteright.ui.components.users.TimeZoneFormField
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.viewmodels.OnboardViewModel
import java.util.TimeZone


@Composable
fun OnboardScreen(
    viewModel: OnboardViewModel = hiltViewModel(),
    topLevelNavigate: (Routes) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    SurfaceLoader(
        loading = state.value.isLoading(),
    ) {
        OnboardScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            searchCurrencies = viewModel::searchCurrencies,
            searchTimeZones = viewModel::searchTimeZones,
            topLevelNavigate = topLevelNavigate,
        )
    }
}

@Composable
fun OnboardScreenContent(
    state: OnboardScreenState = OnboardScreenState(),
    sendEvent: (OnboardScreenEvent) -> Unit = {},
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto> = { _, _ -> emptyPaginatedList() },
    searchTimeZones: suspend (String, PaginationParams) -> PaginatedList<TimeZone> = { _, _ -> emptyPaginatedList() },
    topLevelNavigate: (Routes) -> Unit = {},
) {
    val focusManager = LocalFocusManager.current

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
            horizontalAlignment = Alignment.CenterHorizontally
        ) {
            TextFormField(
                state = state.usernameFieldState,
                onChange = {
                    sendEvent(OnboardScreenEvent.OnUsernameChange(it))
                },
                options = TextFormFieldOptions(
                    label = { Text(text = stringResource(id = R.string.username)) },
                    keyboardOptions = KeyboardOptions.Default.copy(
                        imeAction = ImeAction.Next
                    )
                ),
                modifier = Modifier.fillMaxWidth()
            )
            CurrencyFormField(
                state = state.currencyFieldState,
                onChange = {
                    sendEvent(OnboardScreenEvent.OnCurrencyChange(it))
                },
                searchCurrencies = searchCurrencies
            )
            TimeZoneFormField(
                state = state.timeZoneFieldState,
                onChange = {
                    sendEvent(OnboardScreenEvent.OnTimeZoneChange(it))
                    focusManager.clearFocus()
                },
                searchTimeZones = searchTimeZones
            )
        }
        ButtonWithLoader(
            onClick = {
                sendEvent(
                    OnboardScreenEvent.OnNextClick {
                        topLevelNavigate(Routes.HomeGraph())
                    }
                )
            },
            loading = state.formSubmitting,
            shape = MaterialTheme.shapes.extraSmall,
            content = {
                Text(
                    text = stringResource(id = R.string.next),
                    style = MaterialTheme.typography.bodyLarge
                )
            },
            modifier = Modifier.fillMaxWidth(),
        )
    }
}

@Composable
@BiteRightPreview
fun StartScreenPreview() {
    BiteRightTheme {
        OnboardScreenContent(
            state = OnboardScreenState()
                .copy(
                    formSubmitting = true,
                )
        )
    }
}