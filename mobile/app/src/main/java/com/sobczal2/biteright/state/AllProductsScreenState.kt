package com.sobczal2.biteright.state

import coil.request.ImageRequest
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.products.SimpleProductDto

data class AllProductsScreenState(
    val products: PaginatedList<SimpleProductDto> = emptyPaginatedList(),
    val imageRequestBuilder: ImageRequest.Builder? = null,
    override val globalLoading: Boolean = false,
    override val globalError: String? = null,
) : ScreenState
