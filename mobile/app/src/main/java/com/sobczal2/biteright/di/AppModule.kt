package com.sobczal2.biteright.di

import JwtInterceptor
import android.content.Context
import androidx.core.content.ContextCompat.getString
import com.auth0.android.Auth0
import com.auth0.android.authentication.AuthenticationAPIClient
import com.auth0.android.authentication.storage.CredentialsManager
import com.auth0.android.authentication.storage.SharedPreferencesStorage
import com.fasterxml.jackson.databind.ObjectMapper
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.remote.UserApiDataSource
import com.sobczal2.biteright.domain.repository.UserRepository
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
    fun provideOkHttpClient(credentialsManager: CredentialsManager): OkHttpClient {
        return OkHttpClient.Builder()
            .addInterceptor(JwtInterceptor(credentialsManager))
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
    fun provideUserApiDataSource(retrofit: Retrofit): UserApiDataSource {
        return retrofit.create(UserApiDataSource::class.java)
    }

    @Singleton
    @Provides
    fun provideAuth0(@ApplicationContext context: Context): Auth0 {
        return Auth0(
            getString(context, R.string.com_auth0_client_id),
            getString(context, R.string.com_auth0_domain)
        )
    }

    @Singleton
    @Provides
    fun provideCredentialsManager(@ApplicationContext context: Context, auth0: Auth0): CredentialsManager {
        val apiClient = AuthenticationAPIClient(auth0)
        return CredentialsManager(apiClient, SharedPreferencesStorage(context))
    }

    @Singleton
    @Provides
    fun provideObjectMapper(): ObjectMapper {
        return ObjectMapper()
    }
}