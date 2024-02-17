package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R
import com.sobczal2.biteright.util.StringProvider

class NetworkRepositoryError(
    private val stringProvider: StringProvider
) : RepositoryError {
    override val message: String
        get() = stringProvider.getString(R.string.network_error)
}