package com.sobczal2.biteright.state

import com.sobczal2.biteright.dto.products.DetailedProductDto

data class EditProductScreenState(
    val product: DetailedProductDto? = null,
    override val globalLoading: Boolean = true,
    override val globalError: String? = null,
) : ScreenState