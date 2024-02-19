package com.sobczal2.biteright.ui.components.categories

import androidx.compose.foundation.interaction.MutableInteractionSource
import androidx.compose.foundation.interaction.collectIsPressedAsState
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.shape.CornerSize
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Done
import androidx.compose.material.icons.filled.KeyboardArrowDown
import androidx.compose.material.icons.filled.KeyboardArrowUp
import androidx.compose.material3.DropdownMenu
import androidx.compose.material3.DropdownMenuItem
import androidx.compose.material3.Icon
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.material3.TextFieldDefaults
import androidx.compose.runtime.Composable
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.geometry.Size
import androidx.compose.ui.layout.onGloballyPositioned
import androidx.compose.ui.platform.LocalDensity
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.toSize
import coil.request.ImageRequest
import com.sobczal2.biteright.R
import com.sobczal2.biteright.dto.categories.CategoryDto
import com.sobczal2.biteright.dto.categories.imageUri
import com.sobczal2.biteright.ui.components.common.forms.FormFieldState
import com.sobczal2.biteright.ui.components.common.forms.TextFormField
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldOptions
import com.sobczal2.biteright.ui.components.common.forms.TextFormFieldState
import com.sobczal2.biteright.util.BiteRightPreview
import java.util.UUID

data class CategoryFormFieldState(
    override val value: CategoryDto,
    override val error: String? = null,
    val availableCategories: List<CategoryDto> = emptyList(),
    val inPreview: Boolean = false
) : FormFieldState<CategoryDto?>

@Composable
fun CategoryFormField(
    state: CategoryFormFieldState,
    onChange: (CategoryDto) -> Unit,
    imageRequestBuilder: ImageRequest.Builder? = null
) {
    var dropDownExpanded by remember { mutableStateOf(false) }
    val dropDownTextFieldState by remember {
        mutableStateOf(
            TextFormFieldState(
                value = ""
            )
        )
    }

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
                value = state.value.name,
                error = state.error
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
            state.availableCategories.forEach { category ->
                DropdownMenuItem(
                    leadingIcon = if (category == state.value) {
                        {
                            Icon(Icons.Default.Done, contentDescription = null)
                        }
                    } else {
                        null
                    },
                    text = {
                        Row(
                            modifier = Modifier.fillMaxWidth(),
                            horizontalArrangement = Arrangement.spacedBy(8.dp),
                            verticalAlignment = Alignment.CenterVertically
                        ) {
                            CategoryImage(
                                imageUri = category.imageUri(),
                                imageRequestBuilder = imageRequestBuilder,
                            )
                            Text(text = category.name)
                        }
                    },
                    onClick = {
                        dropDownExpanded = false
                        onChange(
                            category
                        )
                    },
                )
            }
        }
    }
}

@Composable
@BiteRightPreview
fun CategoryFormFieldPreview() {
    val categories = listOf(
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Fruits"
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Vegetables"
        ),
        CategoryDto(
            id = UUID.randomUUID(),
            name = "Dairy"
        ),
    )
    val state = CategoryFormFieldState(
        value = categories.first(),
        availableCategories = categories,
        inPreview = true
    )
    CategoryFormField(
        state = state,
        onChange = {}
    )
}