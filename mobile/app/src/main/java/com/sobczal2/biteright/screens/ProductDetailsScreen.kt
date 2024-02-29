package com.sobczal2.biteright.screens

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.ButtonDefaults
import androidx.compose.material3.Card
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedButton
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.currencies.CurrencyDto
import com.sobczal2.biteright.dto.products.DetailedProductDto
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.dto.units.UnitDto
import com.sobczal2.biteright.dto.units.UnitSystemDto
import com.sobczal2.biteright.dto.users.ProfileDto
import com.sobczal2.biteright.dto.users.UserDto
import com.sobczal2.biteright.events.NavigationEvent
import com.sobczal2.biteright.events.ProductDetailsScreenEvent
import com.sobczal2.biteright.state.ProductDetailsScreenState
import com.sobczal2.biteright.ui.components.categories.CategoryImage
import com.sobczal2.biteright.ui.components.common.DisplayPair
import com.sobczal2.biteright.ui.components.common.ScaffoldLoader
import com.sobczal2.biteright.ui.theme.dimension
import com.sobczal2.biteright.ui.theme.mediumStart
import com.sobczal2.biteright.util.BiteRightPreview
import com.sobczal2.biteright.util.humanize
import com.sobczal2.biteright.viewmodels.ProductDetailsViewModel
import java.time.Instant
import java.time.LocalDate
import java.util.Locale
import java.util.TimeZone
import java.util.UUID

@Composable
fun ProductDetailsScreen(
    viewModel: ProductDetailsViewModel = hiltViewModel(),
    handleNavigationEvent: (NavigationEvent) -> Unit,
    productId: UUID
) {
    val state = viewModel.state.collectAsStateWithLifecycle()

    LaunchedEffect(Unit) {
        viewModel.sendEvent(ProductDetailsScreenEvent.LoadDetails(productId))
    }

    ScaffoldLoader(loading = state.value.isLoading()) {
        ProductDetailsScreenContent(
            state = state.value,
            sendEvent = viewModel::sendEvent,
            handleNavigationEvent = handleNavigationEvent,
        )
    }
}

@Composable
fun ProductDetailsScreenContent(
    state: ProductDetailsScreenState = ProductDetailsScreenState(),
    sendEvent: (ProductDetailsScreenEvent) -> Unit = {},
    handleNavigationEvent: (NavigationEvent) -> Unit = {},
) {
    val product = state.product ?: return
    val user = state.user ?: return
    Scaffold { paddingValues ->
        Column(
            modifier = Modifier
                .fillMaxSize()
                .padding(paddingValues)
                .padding(MaterialTheme.dimension.md),
            verticalArrangement = Arrangement.SpaceBetween,
        ) {
            Column(
                modifier = Modifier.fillMaxWidth(),
                verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                ) {
                    Row {
                        CategoryImage(
                            categoryId = product.category.id,
                            shape = MaterialTheme.shapes.mediumStart,
                            modifier = Modifier.size(MaterialTheme.dimension.xxxl)
                        )
                        Column(
                            modifier = Modifier
                                .fillMaxWidth()
                                .padding(MaterialTheme.dimension.sm),
                            verticalArrangement = Arrangement.Center
                        ) {
                            Text(
                                text = product.name,
                                style = MaterialTheme.typography.headlineMedium
                            )
                            Text(
                                text = product.category.name,
                                style = MaterialTheme.typography.labelLarge
                            )
                        }
                    }
                }


                Card {
                    Column(
                        modifier = Modifier
                            .fillMaxWidth()
                            .padding(MaterialTheme.dimension.md),
                        verticalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.sm)
                    ) {
                        if (product.priceValue != null && product.priceCurrency != null) {
                            DisplayPair(
                                label = "${stringResource(id = R.string.price)}:",
                                value = "${"%.2f".format(Locale.US, product.priceValue)} ${product.priceCurrency.symbol}",
                                modifier = Modifier.fillMaxWidth()
                            )
                        }

                        DisplayPair(
                            label = "${stringResource(id = R.string.expiration_date)}:",
                            value = "${stringResource(product.expirationDateKind.toLocalizedResourceID())} ${product.expirationDateValue?.humanize() ?: ""}",
                            modifier = Modifier.fillMaxWidth()
                        )

                        DisplayPair(
                            label = "${stringResource(id = R.string.category)}:",
                            value = product.category.name,
                            modifier = Modifier.fillMaxWidth()
                        )

                        DisplayPair(
                            label = "${stringResource(id = R.string.current_amount)}:",
                            value = "${"%.2f".format(Locale.US, product.amountCurrentValue)} ${product.amountUnit.abbreviation}",
                            modifier = Modifier.fillMaxWidth()
                        )

                        DisplayPair(
                            label = "${stringResource(id = R.string.max_amount)}:",
                            value = "${"%.2f".format(Locale.US, product.amountMaxValue)} ${product.amountUnit.abbreviation}",
                            modifier = Modifier.fillMaxWidth()
                        )

                        DisplayPair(
                            label = "${stringResource(id = R.string.added_at)}:",
                            value = product.addedDateTime.humanize(TimeZone.getTimeZone(user.profile.timeZoneId)),
                            modifier = Modifier.fillMaxWidth()
                        )

                        OutlinedTextField(
                            value = product.description,
                            onValueChange = {},
                            label = { Text(text = stringResource(id = R.string.description)) },
                            modifier = Modifier.fillMaxWidth(),
                            minLines = 3,
                            readOnly = true,
                            enabled = false,
                            colors = TextFieldDefaults.colors().let {
                                it.copy(
                                    disabledTextColor = it.unfocusedTextColor,
                                    disabledContainerColor = it.unfocusedContainerColor,
                                    disabledIndicatorColor = it.unfocusedIndicatorColor,
                                    disabledLeadingIconColor = it.unfocusedLeadingIconColor,
                                    disabledTrailingIconColor = it.unfocusedTrailingIconColor,
                                    disabledLabelColor = it.unfocusedLabelColor,
                                    disabledPlaceholderColor = it.unfocusedPlaceholderColor,
                                    disabledSupportingTextColor = it.unfocusedSupportingTextColor,
                                    disabledPrefixColor = it.unfocusedPrefixColor,
                                    disabledSuffixColor = it.unfocusedSuffixColor
                                )
                            }
                        )

                        if (product.disposedStateValue) {
                            DisplayPair(
                                label = "${stringResource(id = R.string.disposed_at)}:",
                                value = product.disposedStateDateTime!!.humanize(TimeZone.getTimeZone(user.profile.timeZoneId)),
                                modifier = Modifier.fillMaxWidth()
                            )
                        }
                    }
                }
            }

            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.spacedBy(MaterialTheme.dimension.md)
            ) {
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
                    Text(text = stringResource(id = R.string.back))
                }

                Button(
                    onClick = {
                        handleNavigationEvent(NavigationEvent.NavigateToEditProduct(productId = product.id))
                    },
                    modifier = Modifier.weight(0.5f),
                    shape = MaterialTheme.shapes.extraSmall,
                ) {
                    Text(text = stringResource(id = R.string.edit))
                }
            }
        }
    }
}

@Composable
@BiteRightPreview
fun ProductDetailsScreenPreview() {
    ProductDetailsScreenContent(
        state = ProductDetailsScreenState(
            product = DetailedProductDto(
                UUID.randomUUID(),
                "Product name",
                "Product description",
                10.0,
                CurrencyDto(
                    UUID.randomUUID(),
                    "Złoty",
                    "zł",
                    "PLN"
                ),
                ExpirationDateKindDto.BestBefore,
                LocalDate.now(),
                CategoryDto(
                    UUID.randomUUID(),
                    "Category name"
                ),
                Instant.now(),
                10.0,
                100.0,
                UnitDto(
                    UUID.randomUUID(),
                    "Kilogram",
                    "kg",
                    UnitSystemDto.Metric
                ),
                false,
                null
            ),
            user = UserDto(
                UUID.randomUUID(),
                "test",
                "test",
                "test",
                Instant.now(),
                ProfileDto(
                    currency = CurrencyDto(
                        UUID.randomUUID(),
                        "Złoty",
                        "zł",
                        "PLN"
                    ),
                    timeZoneId = "Europe/Warsaw"
                )
            )
        )
    )
}