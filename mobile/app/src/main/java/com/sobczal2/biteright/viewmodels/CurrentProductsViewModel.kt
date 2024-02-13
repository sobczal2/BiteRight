package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.common.ApiRepositoryError
import com.sobczal2.biteright.state.CurrentProductsScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class CurrentProductsViewModel @Inject constructor(
    private val productRepository: ProductRepository,
) : ViewModel() {
    private val _state = MutableStateFlow(CurrentProductsScreenState())
    val state = _state.asStateFlow()

    suspend fun fetchCurrentProducts() {
        _state.update {
            it.copy(
                loading = true
            )
        }

        val productsResult = productRepository.listCurrent(
            ListCurrentRequest(
                sortingStrategy = ProductSortingStrategy.ExpirationDateAsc
            )
        )

        productsResult.fold(
            { products ->
                _state.update {
                    it.copy(
                        loading = false,
                        currentProducts = products
                    )
                }
            },
            { repositoryError ->
                _state.update {
                    it.copy(
                        loading = false,
                        error = repositoryError.message
                    )
                }
            }
        )
    }

    fun disposeProduct(productId: UUID) {
        _state.update {
            it.copy(
                currentProducts = it.currentProducts.filter { product ->
                    product.id != productId
                }
            )
        }
    }
}