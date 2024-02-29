package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.rememberScrollState
import androidx.compose.foundation.verticalScroll
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalFocusManager
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.events.EditProfileScreenEvent
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.state.EditProfileScreenState
import com.sobczal2.biteright.ui.components.common.ButtonWithLoader
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.components.currencies.CurrencyFormField
import com.sobczal2.biteright.ui.components.users.TimeZoneFormField
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.EditProfileViewModel
import java.util.TimeZone

@Composable
fun EditProfileScreen(
    viewModel: EditProfileViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    ScaffoldLoader(loading = state.value.isLoading()) {
        EditProfileScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            searchCurrencies = viewModel::searchCurrencies,
            searchTimeZones = viewModel::searchTimeZones,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun EditProfileScreenContent(
    state: EditProfileScreenState = EditProfileScreenState(),
    sendEvent: (EditProfileScreenEvent) -> Unit = {},
    searchCurrencies: suspend (String, PaginationParams) -> PaginatedList<CurrencyDto> = { _, _ -> emptyPaginatedList() },
    searchTimeZones: suspend (String, PaginationParams) -> PaginatedList<TimeZone> = { _, _ -> emptyPaginatedList() },
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val focusManager = LocalFocusManager.current

    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md)
                .verticalScroll(rememberScrollState()),
            verticalArrangement = Arrangement.SpaceBetween
        ) {
            Column(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(bottom = MaterialTheme.dimension.xl),
                verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md),
            ) {
                Text(
                    text = stringResource(id = R.string.edit_profile),
                    style = MaterialTheme.typography.displaySmall.copy(
                        textAlign = TextAlign.Center
                    ),
                    modifier = Modifier.fillMaxWidth()
                )

                CurrencyFormField(
                    state = state.currencyFieldState,
                    onChange = {
                        sendEvent(EditProfileScreenEvent.OnCurrencyChange(it))
                    },
                    searchCurrencies = searchCurrencies,
                )

                TimeZoneFormField(
                    state = state.timeZoneFieldState,
                    onChange = {
                        sendEvent(EditProfileScreenEvent.OnTimeZoneChange(it))
                    },
                    searchTimeZones = searchTimeZones,
                )
            }

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
                ButtonWithLoader(
                    loading = state.formSubmitting,
                    onClick = {
                        focusManager.clearFocus()
                        sendEvent(
                            EditProfileScreenEvent.OnSubmitClick(
                                onSuccess = {
                                    handleNavigationEvent(NavigationEvent.NavigateBack)
                                }
                            )
                        )
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = stringResource(id = R.string.save))
                }

                OutlinedButton(
                    onClick = {
                        handleNavigationEvent(NavigationEvent.NavigateBack)
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                    colors = ButtonDefaults.outlinedButtonColors().copy(
                        contentColor = MaterialTheme.colorScheme.error,
                    )
                )
                {
                    Text(text = stringResource(id = R.string.cancel))
                }
            }
        }
    }
}