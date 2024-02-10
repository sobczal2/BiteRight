package com.sobczal2.biteright.util

fun truncateString(string: String, maxLength: Int): String {
    return if (string.length > maxLength) {
        string.substring(0, maxLength) + "..."
    } else {
        string
    }
}