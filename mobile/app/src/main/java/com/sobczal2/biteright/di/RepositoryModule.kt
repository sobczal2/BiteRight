package com.sobczal2.biteright.di

import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CurrenciesApi
import com.sobczal2.biteright.data.api.abstractions.ProductsApi
import com.sobczal2.biteright.data.api.abstractions.UsersApi
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.implementations.CurrencyRepositoryImpl
import com.sobczal2.biteright.repositories.implementations.ProductRepositoryImpl
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
        usersApi: UsersApi,
        gson: Gson
    ): UserRepository {
        return UserRepositoryImpl(usersApi, gson)
    }

    @Singleton
    @Provides
    fun provideProductRepository(
        productsApi: ProductsApi,
        gson: Gson
    ): ProductRepository {
        return ProductRepositoryImpl(productsApi, gson)
    }

    @Singleton
    @Provides
    fun provideCurrencyRepository(
        currenciesApi: CurrenciesApi,
        gson: Gson
    ): CurrencyRepository {
        return CurrencyRepositoryImpl(currenciesApi, gson)
    }
}