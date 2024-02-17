package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R

class UnknownRepositoryError : RepositoryError {
    override val message: String
        get() = R.string.unknown_error.toString() // TODO fix this
}