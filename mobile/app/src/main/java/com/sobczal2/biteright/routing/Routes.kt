package com.sobczal2.biteright.routing

import androidx.navigation.NavDestination
import java.util.UUID

sealed class Routes(open val route: String) {
    open val routeWithParams: String
        get() = route
    open class StartGraph(override val route: String = "start") : Routes(route) {
        data object Welcome : StartGraph("start/welcome")
        data object Onboard : StartGraph("start/onboard")
    }

    open class HomeGraph(override val route: String = "home") : Routes(route) {
        data object CurrentProducts : HomeGraph("home/current_products")
        data object AllProducts : HomeGraph("home/all_products")
        data object Templates : HomeGraph("home/templates")
        data object Profile : HomeGraph("home/profile")
    }

    data object CreateProduct : Routes("create_product")

    data class ProductDetails(val productId: UUID = UUID(0, 0)) : Routes("product_details/{productId}") {
        override val routeWithParams: String
            get() = "product_details/${productId}"
    }

    data class EditProduct(val productId: UUID = UUID(0, 0)) : Routes("edit_product/{productId}") {
        override val routeWithParams: String
            get() = "edit_product/${productId}"
    }

    data object EditProfile : Routes("edit_profile")
}

fun NavDestination?.toRoute(): Routes? {
    return when (this?.route) {
        Routes.StartGraph.Welcome.route -> Routes.StartGraph.Welcome
        Routes.StartGraph.Onboard.route -> Routes.StartGraph.Onboard
        Routes.HomeGraph.CurrentProducts.route -> Routes.HomeGraph.CurrentProducts
        Routes.HomeGraph.AllProducts.route -> Routes.HomeGraph.AllProducts
        Routes.HomeGraph.Templates.route -> Routes.HomeGraph.Templates
        Routes.HomeGraph.Profile.route -> Routes.HomeGraph.Profile
        Routes.CreateProduct.route -> Routes.CreateProduct
        Routes.ProductDetails().route -> Routes.ProductDetails()
        Routes.EditProduct().route -> Routes.EditProduct()
        Routes.EditProfile.route -> Routes.EditProfile
        else -> null
    }
}