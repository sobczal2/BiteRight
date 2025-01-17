package com.sobczal2.biteright.viewmodels

import androidx.compose.material3.SnackbarHostState
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import coil.request.ImageRequest
import com.sobczal2.biteright.data.api.requests.products.ChangeAmountRequest
import com.sobczal2.biteright.data.api.requests.products.DisposeRequest
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
    lateinit var snackbarHostState: SnackbarHostState

    private val _state = MutableStateFlow(
        CurrentProductsScreenState(
            imageRequestBuilder = imageRequestBuilder
        )
    )
    val state = _state.asStateFlow()

    private val _events = Channel<CurrentProductsScreenEvent>()
    val events = _events.receiveAsFlow()

    init {
        viewModelScope.launch {
            launch { events.collect { event -> handleEvent(event) } }
        }
    }

    fun sendEvent(event: CurrentProductsScreenEvent) {
        viewModelScope.launch {
            _events.send(event)
        }
    }

    private fun handleEvent(event: CurrentProductsScreenEvent) {
        when (event) {
            is CurrentProductsScreenEvent.OnProductDispose -> {
                viewModelScope.launch {
                    disposeProduct(event.productId)
                }
            }

            is CurrentProductsScreenEvent.OnProductLongClick -> {
                _state.update {
                    it.copy(
                        changeAmountDialogTargetId = event.productId
                    )
                }
            }

            is CurrentProductsScreenEvent.OnChangeAmountDialogDismiss -> {
                _state.update {
                    it.copy(
                        changeAmountDialogTargetId = null
                    )
                }
            }

            is CurrentProductsScreenEvent.OnChangeAmountDialogConfirm -> {
                viewModelScope.launch {
                    changeProductAmount(event.productId, event.newAmount)
                }
            }
        }
    }

    fun fetchCurrentProducts() {
        viewModelScope.launch {
            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions + CurrentProductsViewModel::fetchCurrentProducts.name,
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
                    snackbarHostState.showSnackbar(
                        message = repositoryError.message,
                    )
                }
            )

            _state.update {
                it.copy(
                    ongoingLoadingActions = it.ongoingLoadingActions - CurrentProductsViewModel::fetchCurrentProducts.name,
                )
            }
        }
    }

    private suspend fun disposeProduct(productId: UUID) {

        val product = _state.value.currentProducts.find { it.id == productId } ?: return

        if (product.disposed) return

        product.disposed = true


        val disposeResponse = productRepository.dispose(
            DisposeRequest(
                productId = productId
            )
        )

        disposeResponse.fold(
            { _ ->
                _state.update {
                    it.copy(
                        currentProducts = it.currentProducts.filter { product ->
                            product.id != productId
                        }
                    )
                }
            },
            { repositoryError ->
                product.disposed = false
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
            }
        )
    }

    private suspend fun changeProductAmount(productId: UUID, newAmount: Double) {
        _state.update {
            it.copy(
                changeAmountDialogLoading = true
            )
        }

        val changeAmountResponse = productRepository.changeAmount(
            ChangeAmountRequest(
                productId = productId,
                amount = newAmount
            )
        )

        changeAmountResponse.fold(
            { _ ->
                _state.update {
                    it.copy(
                        currentProducts = it.currentProducts.map { product ->
                            if (product.id == productId) {
                                product.copy(
                                    currentAmount = newAmount
                                )
                            } else {
                                product
                            }
                        },
                    )
                }
            },
            { repositoryError ->
                snackbarHostState.showSnackbar(
                    message = repositoryError.message,
                )
            }
        )

        _state.update {
            it.copy(
                changeAmountDialogTargetId = null,
                changeAmountDialogLoading = false
            )
        }
    }
}