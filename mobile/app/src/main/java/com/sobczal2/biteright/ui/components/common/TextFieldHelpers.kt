package com.sobczal2.biteright.ui.components.common

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.text2.input.InputTransformation
import androidx.compose.foundation.text2.input.TextFieldBuffer
import androidx.compose.foundation.text2.input.TextFieldCharSequence

@OptIn(ExperimentalFoundationApi::class)
open class DoubleOnlyTransformation(
    digitsAfterDot: Int = 2
) : InputTransformation {
    private val amountTypingRegex = Regex("^\\d+(\\.\\d{0,$digitsAfterDot})?$")
    override fun transformInput(
        originalValue: TextFieldCharSequence,
        valueWithChanges: TextFieldBuffer
    ) {
        val newValue = valueWithChanges.asCharSequence()

        if (amountTypingRegex.matches(newValue) || newValue.isEmpty()) {
            valueWithChanges.replace(0, valueWithChanges.length, newValue)
        }
    }
}