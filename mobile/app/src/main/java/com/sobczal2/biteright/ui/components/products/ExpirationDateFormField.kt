package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.interaction.collectIsPressedAsState
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material.icons.filled.KeyboardArrowUp
import androidx.compose.material3.DatePicker
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.material3.rememberDatePickerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.tooling.preview.PreviewLightDark
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.toEpochMillis
import java.time.LocalDate

data class ExpirationDate(
    val expirationDateKind: ExpirationDateKindDto,
    val localDate: LocalDate?,
)

data class ExpirationDateFormFieldState(
    val value: ExpirationDate = ExpirationDate(
        expirationDateKind = ExpirationDateKindDto.Unknown,
        localDate = null,
    ),
    val expirationDateKindError: String? = null,
    val localDateError: String? = null,
)

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ExpirationDateFormField(
    state: ExpirationDateFormFieldState,
    onChange: (ExpirationDate) -> Unit,
    modifier: Modifier = Modifier,
) {
    var dropDownExpanded by remember { mutableStateOf(false) }
    val dropDownTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

    val datePickerState = rememberDatePickerState()

    LaunchedEffect(state.value) {
        if (state.value.localDate?.toEpochMillis() != datePickerState.selectedDateMillis) {
            datePickerState.selectedDateMillis = state.value.localDate?.toEpochMillis()
        }
    }

    LaunchedEffect(datePickerState.selectedDateMillis) {
        onChange(
            state.value.copy(
                localDate = datePickerState.selectedDateMillis?.let { LocalDate.ofEpochDay(it / (24 * 60 * 60 * 1000)) }
            )
        )
    }


    Column {
        Column {
            var textFieldSize by remember { mutableStateOf(Size.Zero) }

            val interactionSource = remember { MutableInteractionSource() }
            val isPressed: Boolean by interactionSource.collectIsPressedAsState()

            LaunchedEffect(isPressed) {
                if (isPressed) {
                    dropDownExpanded = true
                }
            }


            TextFormField(
                state = dropDownTextFieldState.copy(
                    value = stringResource(
                        id = ExpirationDateKindDto.toLocalizedResourceID(
                            state.value.expirationDateKind
                        )
                    ),
                    error = state.expirationDateKindError
                ),
                onChange = {},
                options = TextFormFieldOptions(
                    shape = MaterialTheme.shapes.extraSmall.copy(
                        topStart = CornerSize(0.dp),
                        bottomStart = CornerSize(0.dp),
                        bottomEnd = CornerSize(0.dp)
                    ),
                    trailingIcon = {
                        Icon(
                            imageVector = if (dropDownExpanded) {
                                Icons.Default.KeyboardArrowUp
                            } else {
                                Icons.Default.KeyboardArrowDown
                            },
                            contentDescription = null
                        )
                    },
                    readOnly = true,
                    interactionSource = interactionSource,
                    colors = TextFieldDefaults.colors()
                        .copy(
                            errorTextColor = MaterialTheme.colorScheme.error
                        ),
                ),
                modifier = Modifier
                    .fillMaxWidth()
                    .onGloballyPositioned {
                        textFieldSize = it.size.toSize()
                    }
            )
            DropdownMenu(
                expanded = dropDownExpanded,
                onDismissRequest = {
                    dropDownExpanded = false
                },
                modifier = Modifier
                    .width(with(LocalDensity.current) { textFieldSize.width.toDp() })
            ) {
                ExpirationDateKindDto.entries.forEach { expirationDateKind ->
                    DropdownMenuItem(
                        leadingIcon = if (expirationDateKind == state.value.expirationDateKind) {
                            {
                                Icon(Icons.Default.Done, contentDescription = null)
                            }
                        } else {
                            null
                        },
                        text = {
                            Text(
                                text = stringResource(
                                    id = ExpirationDateKindDto.toLocalizedResourceID(
                                        expirationDateKind
                                    )
                                ),
                            )
                        },
                        onClick = {
                            onChange(
                                state.value.copy(
                                    expirationDateKind = expirationDateKind,
                                )
                            )
                            dropDownExpanded = false
                        },
                    )
                }
            }
        }

        if (ExpirationDateKindDto.shouldIncludeDate(state.value.expirationDateKind)) {
            DatePicker(
                state = datePickerState,
                modifier = Modifier.fillMaxWidth(),
            )
        }
    }
}

@Composable
@PreviewLightDark
@Preview(apiLevel = 33)
@Preview(apiLevel = 33, uiMode = Configuration.UI_MODE_NIGHT_YES)
fun ExpirationDateFormFieldPreview() {
    BiteRightTheme {
        ExpirationDateFormField(
            state = ExpirationDateFormFieldState(),
            onChange = {},
        )
    }
}
