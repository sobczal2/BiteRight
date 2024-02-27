package com.sobczal2.biteright.routing

import java.util.UUID

object Routes {
    const val WELCOME = "welcome"
    const val START = "start"
    const val CURRENT_PRODUCTS = "current_products"
    const val ALL_PRODUCTS = "all_products"
    const val TEMPLATES = "templates"
    const val PROFILE = "profile"
    const val CREATE_PRODUCT = "create_product"
    const val PRODUCT_DETAILS = "product_details/{productId}"
    const val EDIT_PRODUCT = "edit_product/{productId}"
    fun productDetails(productId: UUID) = "product_details/$productId"
    fun editProduct(productId: UUID) = "edit_product/$productId"
}