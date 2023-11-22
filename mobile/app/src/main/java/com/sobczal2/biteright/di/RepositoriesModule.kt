package com.sobczal2.biteright.di

import com.sobczal2.biteright.data.local.UserSPDataSource
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
    @Provides
    @Singleton
    fun provideUserRepository(
        userApiDataSource: UserApiDataSource,
        userSPDataSource: UserSPDataSource
    ): UserRepository {
        return UserRepositoryImpl(userApiDataSource, userSPDataSource)
    }
}