package com.sobczal2.biteright.di

import android.content.Context
import com.auth0.android.Auth0
import com.sobczal2.biteright.data.repository.UserRepositoryImpl
import com.sobczal2.biteright.domain.repository.UserRepository
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object RepositoriesModule {
    @Singleton
    @Provides
    fun provideUserRepository(
    ): UserRepository {
        return UserRepositoryImpl()
    }
}