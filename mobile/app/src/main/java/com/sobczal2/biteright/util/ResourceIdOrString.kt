package com.sobczal2.biteright.util

import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource

class ResourceIdOrString {
    private var resourceId: Int? = null
    private var string: String? = null

    constructor(resourceId: Int) {
        this.resourceId = resourceId
    }

    constructor(string: String) {
        this.string = string
    }

    fun isResource(): Boolean {
        return resourceId != null
    }

    fun getResourceId(): Int? {
        return resourceId
    }

    fun getString(): String? {
        return string
    }
}

@Composable
fun ResourceIdOrString.asString(): String {
    return when {
        isResource() -> stringResource(id = getResourceId()!!)
        else -> getString() ?: ""
    }
}