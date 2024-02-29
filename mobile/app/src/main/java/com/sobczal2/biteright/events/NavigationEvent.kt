package com.sobczal2.biteright.events

import com.sobczal2.biteright.routing.Routes
import java.util.UUID

sealed class NavigationEvent(val route: String?) {
    data object NavigateBack : NavigationEvent(null)
    data object NavigateToWelcome : NavigationEvent(Routes.WELCOME)
    data object NavigateToStart : NavigationEvent(Routes.START)
    data object NavigateToCurrentProducts : NavigationEvent(Routes.CURRENT_PRODUCTS)
    data object NavigateToAllProducts : NavigationEvent(Routes.ALL_PRODUCTS)
    data object NavigateToTemplates : NavigationEvent(Routes.TEMPLATES)
    data object NavigateToProfile : NavigationEvent(Routes.PROFILE)
    data object NavigateToCreateProduct : NavigationEvent(Routes.CREATE_PRODUCT)
    data object NavigateToEditProfile : NavigationEvent(Routes.EDIT_PROFILE)
    data class NavigateToProductDetails(val productId: UUID) : NavigationEvent(Routes.productDetails(productId))
    data class NavigateToEditProduct(val productId: UUID) : NavigationEvent(Routes.editProduct(productId))
}