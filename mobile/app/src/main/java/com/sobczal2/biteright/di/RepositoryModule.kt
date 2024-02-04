package com.sobczal2.biteright.di

import com.fasterxml.jackson.databind.ObjectMapper
import com.sobczal2.biteright.data.api.UserApi
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.implementations.UserRepositoryImpl
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoryModule {
    @Singleton
    @Provides
    fun provideUserRepository(
        userApi: UserApi,
        objectMapper: ObjectMapper
    ): UserRepository {
        return UserRepositoryImpl(userApi, objectMapper)
    }
}