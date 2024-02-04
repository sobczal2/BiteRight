package com.sobczal2.biteright.di

import android.content.Context
import androidx.core.content.ContextCompat.getString
import com.auth0.android.authentication.storage.CredentialsManager
import com.fasterxml.jackson.databind.ObjectMapper
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.LanguageInterceptor
import com.sobczal2.biteright.data.api.TokenInterceptor
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppModule {
    @Singleton
    @Provides
    fun provideAuthManager(@ApplicationContext context: Context): AuthManager {
        return AuthManager(context)
    }

    @Singleton
    @Provides
    fun provideOkHttpClient(authManager: AuthManager): OkHttpClient {
        return OkHttpClient.Builder()
            .addInterceptor(TokenInterceptor(authManager))
            .addInterceptor(LanguageInterceptor())
            .build()
    }

    @Provides
    @Singleton
    fun provideRetrofit(
        @ApplicationContext context: Context,
        okHttpClient: OkHttpClient
    ): Retrofit {
        return Retrofit.Builder()
            .baseUrl(
                getString(context, R.string.api_url)
            )
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    @Provides
    @Singleton
    fun provideObjectMapper(): ObjectMapper {
        return ObjectMapper()
    }
}