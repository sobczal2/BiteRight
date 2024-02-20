package com.sobczal2.biteright.di

import com.google.gson.Gson
import com.sobczal2.biteright.data.api.abstractions.CategoriesApi
import com.sobczal2.biteright.data.api.abstractions.CurrenciesApi
import com.sobczal2.biteright.data.api.abstractions.ProductsApi
import com.sobczal2.biteright.data.api.abstractions.UnitsApi
import com.sobczal2.biteright.data.api.abstractions.UsersApi
import com.sobczal2.biteright.repositories.abstractions.CategoryRepository
import com.sobczal2.biteright.repositories.abstractions.CurrencyRepository
import com.sobczal2.biteright.repositories.abstractions.ProductRepository
import com.sobczal2.biteright.repositories.abstractions.UnitRepository
import com.sobczal2.biteright.repositories.abstractions.UserRepository
import com.sobczal2.biteright.repositories.implementations.CategoryRepositoryImpl
import com.sobczal2.biteright.repositories.implementations.CurrencyRepositoryImpl
import com.sobczal2.biteright.repositories.implementations.ProductRepositoryImpl
import com.sobczal2.biteright.repositories.implementations.UnitRepositoryImpl
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

    @Singleton
    @Provides
    fun provideCategoryRepository(
        categoriesApi: CategoriesApi,
        stringProvider: StringProvider,
        gson: Gson
    ): CategoryRepository {
        return CategoryRepositoryImpl(categoriesApi, stringProvider, gson)
    }

    @Singleton
    @Provides
    fun provideUnitRepository(
        unitsApi: UnitsApi,
        stringProvider: StringProvider,
        gson: Gson
    ): UnitRepository {
        return UnitRepositoryImpl(unitsApi, stringProvider, gson)
    }
}