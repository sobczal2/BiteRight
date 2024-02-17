package com.sobczal2.biteright.events

sealed class CreateProductScreenEvent {
    data class OnNameChange(val value: String) : CreateProductScreenEvent()
    data class OnDescriptionChange(val value: String) : CreateProductScreenEvent()
}