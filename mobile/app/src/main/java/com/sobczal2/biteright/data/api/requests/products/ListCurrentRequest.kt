package com.sobczal2.biteright.data.api.requests.products

import com.google.gson.annotations.SerializedName
import com.sobczal2.biteright.dto.products.ProductSortingStrategy

data class ListCurrentRequest(
    @SerializedName("sortingStrategy") val sortingStrategy: ProductSortingStrategy
)