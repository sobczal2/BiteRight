package com.sobczal2.biteright.data.local

interface UserSPDataSource {
    fun save(userId: String, jwt: String, refreshToken: String)
    fun getUserId(): String?
    fun getJwt(): String?
    fun getRefreshToken(): String?
}