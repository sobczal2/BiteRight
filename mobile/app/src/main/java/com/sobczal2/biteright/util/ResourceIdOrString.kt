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
fun Map<ResourceIdOrString, List<ResourceIdOrString>>.asStringMap(): Map<String, List<String>> {
    val result = mutableMapOf<String, List<String>>()
    for ((key, value) in this) {
        result[key.asString()] = value.map { it.asString() }
    }
    return result
}

fun Map<String, List<String>>.asResourceIdOrStringMap(): Map<ResourceIdOrString, List<ResourceIdOrString>> {
    val result = mutableMapOf<ResourceIdOrString, List<ResourceIdOrString>>()
    for ((key, value) in this) {
        result[ResourceIdOrString(key)] = value.map { ResourceIdOrString(it) }
    }
    return result
}

@Composable
fun ResourceIdOrString.asString(): String {
    return when {
        isResource() -> stringResource(id = getResourceId()!!)
        else -> getString() ?: ""
    }
}

fun String.asResourceIdOrString(): ResourceIdOrString {
    return ResourceIdOrString(this)
}