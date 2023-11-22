package com.sobczal2.biteright.di

import android.content.Context
import androidx.core.content.ContextCompat.getString
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.local.UserSPDataSource
import com.sobczal2.biteright.data.local.UserSPDataSourceImpl
import com.sobczal2.biteright.data.remote.UserApiDataSource
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppModule {

    @Provides
    @Singleton
    fun provideRetrofit(@ApplicationContext context: Context): Retrofit {
        return Retrofit.Builder()
            .baseUrl(
                getString(context, R.string.api_url)
            )
            .addConverterFactory(GsonConverterFactory.create())
            .build()
    }

    @Provides
    @Singleton
    fun provideUserApiDataSource(retrofit: Retrofit): UserApiDataSource {
        return retrofit.create(UserApiDataSource::class.java)
    }

    @Provides
    fun provideUserSPDataSource(@ApplicationContext context: Context): UserSPDataSource {
        return UserSPDataSourceImpl(context)
    }
}