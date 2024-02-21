package com.sobczal2.biteright.viewmodels

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.ImageRequest
import com.sobczal2.biteright.data.api.requests.products.DisposeRequest
import com.sobczal2.biteright.data.api.requests.products.FilteringParams
import com.sobczal2.biteright.data.api.requests.products.RestoreRequest
import com.sobczal2.biteright.data.api.requests.products.SearchRequest
import com.sobczal2.biteright.dto.common.PaginatedList
import com.sobczal2.biteright.dto.common.PaginationParams
import com.sobczal2.biteright.dto.common.emptyPaginatedList
import com.sobczal2.biteright.dto.products.ProductSortingStrategy
import com.sobczal2.biteright.dto.products.SimpleProductDto
import com.sobczal2.biteright.events.AllProductsScreenEvent
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.state.AllProductsScreenState
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
class AllProductsViewModel @Inject constructor(
    private val productRepository: ProductRepository,
    imageRequestBuilder: ImageRequest.Builder
) : ViewModel() {
    private val _state = MutableStateFlow(
        AllProductsScreenState(
            imageRequestBuilder = imageRequestBuilder
        )
    )
    val state = _state.asStateFlow()

    private val _events = Channel<AllProductsScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
            launch { fetchProductsForNewQuery() }
        }
    }

    fun sendEvent(event: AllProductsScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: AllProductsScreenEvent) {
        when (event) {
            is AllProductsScreenEvent.OnProductDispose -> {
                viewModelScope.launch {
                    disposeProduct(event.productId)
                }
            }

            is AllProductsScreenEvent.OnProductRestore -> {
                viewModelScope.launch {
                    restoreProduct(event.productId)
                }
            }

            AllProductsScreenEvent.ReloadProducts -> {
                viewModelScope.launch {
                    fetchProductsForNewQuery()
                }
            }
        }
    }

    private suspend fun fetchProductsForNewQuery() {
        val products = searchProducts(
            "",
            emptyList(),
            ProductSortingStrategy.AddedDateTimeAsc,
            PaginationParams.Default
        )
        _state.update {
            it.copy(products = products)
        }
    }

    private suspend fun searchProducts(
        query: String,
        categoryIds: List<UUID>,
        sortingStrategy: ProductSortingStrategy,
        paginationParams: PaginationParams
    ): PaginatedList<SimpleProductDto> {
        val productsResult = productRepository.search(
            SearchRequest(
                query = query,
                filteringParams = FilteringParams(categoryIds = categoryIds),
                sortingStrategy = sortingStrategy,
                paginationParams = paginationParams
            )
        )

        productsResult.fold(
            { response -> return response.products },
            { repositoryError ->
                _state.update {
                    it.copy(globalError = repositoryError.message)
                }
            }
        )
        return emptyPaginatedList()
    }

    private suspend fun disposeProduct(productId: UUID) {
        val disposeResult = productRepository.dispose(
            DisposeRequest(
                productId = productId
            )
        )
        disposeResult.fold(
            { _ ->
                handleEvent(AllProductsScreenEvent.ReloadProducts) // TODO: This is a workaround, we should update the state instead
            },
            { repositoryError ->
                _state.update {
                    it.copy(globalError = repositoryError.message)
                }
            }
        )
    }

    private suspend fun restoreProduct(productId: UUID) {
        val restoreResult = productRepository.restore(
            RestoreRequest(
                productId = productId
            )
        )
        restoreResult.fold(
            { _ ->
                handleEvent(AllProductsScreenEvent.ReloadProducts) // TODO: This is a workaround, we should update the state instead
            },
            { repositoryError ->
                _state.update {
                    it.copy(globalError = repositoryError.message)
                }
            }
        )
    }
}