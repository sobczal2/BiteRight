package com.sobczal2.biteright.ui.components.products

import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.material3.DatePicker
import androidx.compose.material3.ExperimentalMaterial3Api
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.RadioButton
import androidx.compose.material3.Surface
import androidx.compose.material3.Text
import androidx.compose.material3.rememberDatePickerState
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.window.Dialog
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.products.ExpirationDateKindDto
import com.sobczal2.biteright.ui.theme.dimension
import java.time.LocalDate

@OptIn(ExperimentalMaterial3Api::class)
@Composable
fun ExpirationDateDialog(
    onSelected: (LocalDate, ExpirationDateKindDto) -> Unit, onDismissRequest: () -> Unit
) {
    var selectedKind by remember { mutableStateOf<ExpirationDateKindDto?>(null) }
    val datePickerState = rememberDatePickerState()

    LaunchedEffect(datePickerState.selectedDateMillis) {
        if (datePickerState.selectedDateMillis != null && selectedKind != null && selectedKind != ExpirationDateKindDto.Unknown) {
            val selectedDateDays = datePickerState.selectedDateMillis!! / 86_400_000
            onSelected(LocalDate.ofEpochDay(selectedDateDays), selectedKind!!)
            onDismissRequest()
        }
    }

    Dialog(onDismissRequest = onDismissRequest) {
        Surface(
            shape = MaterialTheme.shapes.medium,
            modifier = Modifier.padding(MaterialTheme.dimension.lg)
        ) {
            Column(
                modifier = Modifier.padding(MaterialTheme.dimension.sm)
            ) {
                Text(stringResource(id = R.string.select_expiration_date))
                ExpirationDateKindSelector(selectedKind = selectedKind,
                    onSelected = { selectedKind = it })
                if (
                    selectedKind != null
                    && selectedKind != ExpirationDateKindDto.Unknown
                    && selectedKind != ExpirationDateKindDto.Infinite
                ) {
                    DatePicker(state = datePickerState)
                }
            }
        }
    }
}

@Composable
fun ExpirationDateKindSelector(
    selectedKind: ExpirationDateKindDto?, onSelected: (ExpirationDateKindDto) -> Unit
) {
    Column {
        ExpirationDateKindDto.entries.forEach { kind ->
            Row(
                verticalAlignment = Alignment.CenterVertically,
                modifier = Modifier.fillMaxWidth()
            ) {
                RadioButton(
                    selected = kind == selectedKind,
                    onClick = { onSelected(kind) },
                )
                Text(stringResource(id = ExpirationDateKindDto.toLocalizedResourceID(kind)),
                    modifier = Modifier.clickable { onSelected(kind) })
            }
        }
    }
}


@Composable
@Preview(apiLevel = 33)
fun ExpirationDateDialogPreview() {
    ExpirationDateDialog(onSelected = { _, _ -> }, onDismissRequest = { })
}