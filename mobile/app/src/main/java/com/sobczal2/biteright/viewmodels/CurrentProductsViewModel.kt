package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.ImageRequest
import com.sobczal2.biteright.data.api.requests.products.ListCurrentRequest
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.events.CurrentProductsScreenEvent
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.state.CurrentProductsScreenState
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.receiveAsFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import java.util.UUID
import javax.inject.Inject

@HiltViewModel
class CurrentProductsViewModel @Inject constructor(
    private val productRepository: ProductRepository,
    imageRequestBuilder: ImageRequest.Builder
) : ViewModel() {
    private val _state = MutableStateFlow(CurrentProductsScreenState(
        imageRequestBuilder = imageRequestBuilder
    ))
    val state = _state.asStateFlow()

    private val _events = Channel<CurrentProductsScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
            launch { fetchCurrentProducts() }
        }
    }

    fun sendEvent(event: CurrentProductsScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: CurrentProductsScreenEvent) {
        when (event) {
            is CurrentProductsScreenEvent.OnProductDispose -> disposeProduct(event.productId)
        }
    }

    private suspend fun fetchCurrentProducts() {
        _state.update {
            it.copy(
                 globalLoading = true
            )
        }

        val productsResult = productRepository.listCurrent(
            ListCurrentRequest(
                sortingStrategy = ProductSortingStrategy.ExpirationDateAsc
            )
        )

        productsResult.fold(
            { response ->
                _state.update {
                    it.copy(
                        currentProducts = response.products
                    )
                }
            },
            { repositoryError ->
                _state.update {
                    it.copy(
                        globalError = repositoryError.message
                    )
                }
            }
        )

        _state.update {
            it.copy(
                globalLoading = false
            )
        }
    }

    private fun disposeProduct(productId: UUID) {
        _state.update {
            it.copy(
                currentProducts = it.currentProducts.filter { product ->
                    product.id != productId
                }
            )
        }
    }
}