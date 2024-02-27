package com.sobczal2.biteright.util

import android.os.Bundle
import java.util.UUID

fun Bundle.getUUID(key: String): UUID {
    return UUID.fromString(getString(key))
}