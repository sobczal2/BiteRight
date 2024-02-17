package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R
import com.sobczal2.biteright.util.StringProvider

class UnknownRepositoryError(
    private val stringProvider: StringProvider
) : RepositoryError {
    override val message: String
        get() = stringProvider.getString(R.string.unknown_error)
}