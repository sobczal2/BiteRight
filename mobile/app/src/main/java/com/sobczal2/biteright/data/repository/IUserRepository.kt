package com.sobczal2.biteright.data.repository

import com.sobczal2.biteright.dto.user.UserDto

interface IUserRepository {
    fun me(): UserDto
    fun signIn(email: String, password: String)
    fun signUp(email: String, password: String, name: String)
}
