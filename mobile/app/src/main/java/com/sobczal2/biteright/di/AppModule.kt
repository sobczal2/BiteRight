package com.sobczal2.biteright.di

import JwtInterceptor
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
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppModule {

    @Provides
    @Singleton
    fun provideRetrofit(@ApplicationContext context: Context, okHttpClient: OkHttpClient): Retrofit {
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

    @Provides
    @Singleton
    fun provideUserSPDataSource(@ApplicationContext context: Context): UserSPDataSource {
        return UserSPDataSourceImpl(context)
    }

    @Singleton
    @Provides
    fun provideOkHttpClient(userSPDataSource: UserSPDataSource): OkHttpClient {
        return OkHttpClient.Builder()
            .addInterceptor(JwtInterceptor(userSPDataSource))
            .build()
    }
}