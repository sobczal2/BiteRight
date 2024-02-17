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
import com.sobczal2.biteright.util.StringProvider
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
        stringProvider: StringProvider,
        gson: Gson
    ): UserRepository {
        return UserRepositoryImpl(usersApi, stringProvider, gson)
    }

    @Singleton
    @Provides
    fun provideProductRepository(
        productsApi: ProductsApi,
        stringProvider: StringProvider,
        gson: Gson
    ): ProductRepository {
        return ProductRepositoryImpl(productsApi, stringProvider, gson)
    }

    @Singleton
    @Provides
    fun provideCurrencyRepository(
        currenciesApi: CurrenciesApi,
        stringProvider: StringProvider,
        gson: Gson
    ): CurrencyRepository {
        return CurrencyRepositoryImpl(currenciesApi, stringProvider, gson)
    }
}