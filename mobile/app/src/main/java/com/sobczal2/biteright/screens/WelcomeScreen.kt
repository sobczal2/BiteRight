package com.sobczal2.biteright.screens

import android.content.res.Configuration
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.collectAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import androidx.hilt.navigation.compose.hiltViewModel
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.WelcomeScreenEvent
import com.sobczal2.biteright.state.WelcomeScreenState
import com.sobczal2.biteright.ui.components.common.BiteRightLogo
import com.sobczal2.biteright.ui.components.common.ErrorBox
import com.sobczal2.biteright.ui.components.products.PriceFormField
import com.sobczal2.biteright.ui.components.products.PriceFormFieldEvents
import com.sobczal2.biteright.ui.components.products.PriceFormFieldOptions
import com.sobczal2.biteright.ui.components.products.PriceFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.viewmodels.WelcomeViewModel
import java.util.UUID

@Composable
fun WelcomeScreen(
    viewModel: WelcomeViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
) {
    val state = viewModel.state.collectAsState()

    WelcomeScreenContent(
        state = state.value,
        sendEvent = viewModel::sendEvent,
        handleNavigationEvent = handleNavigationEvent,
    )
}


@Composable
fun WelcomeScreenContent(
    state: WelcomeScreenState = WelcomeScreenState(),
    sendEvent: (WelcomeScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val context = LocalContext.current
    Surface(
        modifier = Modifier
            .fillMaxSize(),
    ) {
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(MaterialTheme.dimension.lg),
            horizontalAlignment = Alignment.CenterHorizontally,
            verticalArrangement = Arrangement.SpaceAround
        ) {
            BiteRightLogo(
                modifier = Modifier
                    .size(300.dp)
            )
            Button(
                onClick = {
                    sendEvent(WelcomeScreenEvent.OnGetStartedClick(context = context) {
                        handleNavigationEvent(NavigationEvent.NavigateToStart)
                    })
                }
            ) {
                Text(
                    text = stringResource(id = R.string.get_started),
                    style = MaterialTheme.typography.displaySmall
                )
            }
            PriceFormField(state = PriceFormFieldState(availableCurrencies = listOf(
                CurrencyDto(UUID.randomUUID(), "USD", "US Dollar", "USD"),
            )), onEvent = {}, options = PriceFormFieldOptions()
            )
            ErrorBox(error = state.globalError)
        }
    }
}

@Composable
@Preview
@Preview(uiMode = Configuration.UI_MODE_NIGHT_YES)
fun WelcomeScreenPreview() {
    BiteRightTheme {
        WelcomeScreenContent()
    }
}