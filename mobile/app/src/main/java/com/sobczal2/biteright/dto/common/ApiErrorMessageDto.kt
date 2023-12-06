package com.sobczal2.biteright.dto.common

data class ApiErrorMessageDto(
    val errors: HashMap<String, List<String>>
) {
    companion object {
        fun createForUnknownError(): ApiErrorMessageDto {
            val errors = hashMapOf<String, List<String>>()
            errors["unknown"] = listOf("An unknown error occurred")
            return ApiErrorMessageDto(errors)
        }
    }
}