package com.sobczal2.biteright.data.local

interface UserSPDataSource {
    fun save(userId: Int, jwt: String, refreshToken: String)
    fun getUserId(): Int?
    fun getJwt(): String?
    fun getRefreshToken(): String?
}