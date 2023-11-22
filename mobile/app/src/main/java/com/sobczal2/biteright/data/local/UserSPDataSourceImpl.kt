package com.sobczal2.biteright.data.local

import android.content.Context
import android.content.SharedPreferences
import javax.inject.Inject


class UserSPDataSourceImpl @Inject constructor(
    private val context: Context
) : UserSPDataSource {
    private val sharedPreferences: SharedPreferences =
        context.getSharedPreferences("user", Context.MODE_PRIVATE)
    override fun save(userId: Int, jwt: String, refreshToken: String) {
        sharedPreferences.edit()
            .putInt("userId", userId)
            .putString("jwt", jwt)
            .putString("refreshToken", refreshToken)
            .apply()
    }

    override fun getUserId(): Int? {
        return sharedPreferences.getInt("userId", -1).takeIf { it != -1 }
    }

    override fun getJwt(): String? {
        return sharedPreferences.getString("jwt", null)
    }

    override fun getRefreshToken(): String? {
        return sharedPreferences.getString("refreshToken", null)
    }
}