package com.sobczal2.biteright.di

import com.sobczal2.biteright.data.api.abstractions.CategoriesApi
import com.sobczal2.biteright.data.api.abstractions.CurrenciesApi
import com.sobczal2.biteright.data.api.abstractions.ProductsApi
import com.sobczal2.biteright.data.api.abstractions.UnitsApi
import com.sobczal2.biteright.data.api.abstractions.UsersApi
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import retrofit2.Retrofit
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object BackendDataModule {
    @Singleton
    @Provides
    fun provideUsersApi(retrofit: Retrofit): UsersApi {
        return retrofit.create(UsersApi::class.java)
    }

    @Singleton
    @Provides
    fun provideProductsApi(retrofit: Retrofit): ProductsApi {
        return retrofit.create(ProductsApi::class.java)
    }

    @Singleton
    @Provides
    fun provideCategoriesApi(retrofit: Retrofit): CategoriesApi {
        return retrofit.create(CategoriesApi::class.java)
    }

    @Singleton
    @Provides
    fun provideCurrenciesApi(retrofit: Retrofit): CurrenciesApi {
        return retrofit.create(CurrenciesApi::class.java)
    }

    @Singleton
    @Provides
    fun provideUnitsApi(retrofit: Retrofit): UnitsApi {
        return retrofit.create(UnitsApi::class.java)
    }
}