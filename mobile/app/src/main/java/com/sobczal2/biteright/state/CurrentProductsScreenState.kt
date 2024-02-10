package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.util.ResourceIdOrString

data class CurrentProductsScreenState(
    val currentProducts: List<SimpleProductDto> = emptyList(),
    val loading: Boolean = false,
    val error: ResourceIdOrString? = null
)