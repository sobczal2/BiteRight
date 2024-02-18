package com.sobczal2.biteright.ui.components.products

import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import com.sobczal2.biteright.ui.components.common.forms.FormFieldState

data class ExpirationDateFormFieldState(
    override val value: String = "",
    override val error: String? = null,
) : FormFieldState<String>

@Composable
fun ExpirationDateFormField(
    state: ExpirationDateFormFieldState,
    onChange: (String) -> Unit,
    modifier: Modifier = Modifier,
) {

}