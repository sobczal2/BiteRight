package com.sobczal2.biteright.di

import android.content.Context
import androidx.core.content.ContextCompat.getString
import coil.request.ImageRequest
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import com.sobczal2.biteright.AuthManager
import com.sobczal2.biteright.R
import com.sobczal2.biteright.data.api.common.ExclusionAnnotationStrategy
import com.sobczal2.biteright.data.api.common.LanguageInterceptor
import com.sobczal2.biteright.data.api.common.LocalDateTimeTypeAdapter
import com.sobczal2.biteright.data.api.common.LocalDateTypeAdapter
import com.sobczal2.biteright.data.api.common.TokenInterceptor
import com.sobczal2.biteright.data.api.common.UnauthorizedInterceptor
import com.sobczal2.biteright.util.StringProvider
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.android.qualifiers.ApplicationContext
import dagger.hilt.components.SingletonComponent
import kotlinx.coroutines.runBlocking
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.time.LocalDate
import java.time.LocalDateTime
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppModule {
    @Singleton
    @Provides
    fun provideAuthManager(@ApplicationContext context: Context): AuthManager {
        return AuthManager(context)
    }

    @Singleton
    @Provides
    fun provideOkHttpClient(authManager: AuthManager): OkHttpClient {
        return OkHttpClient.Builder()
            .addInterceptor(TokenInterceptor(authManager))
            .addInterceptor(LanguageInterceptor())
            .addInterceptor(
                HttpLoggingInterceptor().apply {
                    level = HttpLoggingInterceptor.Level.BODY
                }
            )
            .addInterceptor(UnauthorizedInterceptor(authManager))
            .build()
    }

    @Provides
    @Singleton
    fun provideRetrofit(
        @ApplicationContext context: Context,
        okHttpClient: OkHttpClient,
        gson: Gson
    ): Retrofit {
        return Retrofit.Builder()
            .baseUrl(
                getString(context, R.string.api_url)
            )
            .client(okHttpClient)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
    }

    @Provides
    @Singleton
    fun provideGson(): Gson {
        val gson = GsonBuilder()
            .registerTypeAdapter(LocalDate::class.java, LocalDateTypeAdapter())
            .registerTypeAdapter(LocalDateTime::class.java, LocalDateTimeTypeAdapter())
            .addDeserializationExclusionStrategy(ExclusionAnnotationStrategy())
            .addSerializationExclusionStrategy(ExclusionAnnotationStrategy())
            .create()

        return gson
    }

    @Provides
    @Singleton
    fun provideStringProvider(@ApplicationContext context: Context): StringProvider {
        return StringProvider(context)
    }

    @Singleton
    @Provides
    fun provideImageRequestBuilder(
        @ApplicationContext context: Context,
        authManager: AuthManager
    ): ImageRequest.Builder {
        val jwt = runBlocking {
            authManager.getJwt()
        }

        return ImageRequest
            .Builder(context)
            .addHeader("Authorization", "Bearer $jwt")
    }
}