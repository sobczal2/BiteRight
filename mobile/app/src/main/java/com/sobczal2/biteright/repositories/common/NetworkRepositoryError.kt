package com.sobczal2.biteright.repositories.common

import com.sobczal2.biteright.R
import com.sobczal2.biteright.util.ResourceIdOrString

class NetworkRepositoryError : RepositoryError {
    override val message: ResourceIdOrString
        get() = ResourceIdOrString(R.string.network_error)
}