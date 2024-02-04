package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R
import com.sobczal2.biteright.util.ResourceIdOrString

class UnknownRepositoryError : RepositoryError {
    override val message: ResourceIdOrString
        get() = ResourceIdOrString(R.string.unknown_error)
}