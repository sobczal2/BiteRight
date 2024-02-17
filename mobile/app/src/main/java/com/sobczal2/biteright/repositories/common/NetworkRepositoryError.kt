package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R

class NetworkRepositoryError : RepositoryError {
    override val message: String
        get() = R.string.network_error.toString() // TODO fix this
}