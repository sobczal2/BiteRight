package com.sobczal2.biteright.di

import com.auth0.android.authentication.storage.CredentialsManager
import com.fasterxml.jackson.databind.ObjectMapper
import com.sobczal2.biteright.data.remote.UserApiDataSource
import com.sobczal2.biteright.data.repository.UserRepositoryImpl
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoriesModule {
    @Singleton
    @Provides
    fun provideUserRepository(
        credentialsManager: CredentialsManager,
        userApiDataSource: UserApiDataSource,
        objectMapper: ObjectMapper
    ): UserRepository {
        return UserRepositoryImpl(credentialsManager, userApiDataSource, objectMapper)
    }
}