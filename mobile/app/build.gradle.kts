plugins {
    id("com.android.application")
    id("org.jetbrains.kotlin.android")
    id("com.google.dagger.hilt.android")
    id("kotlin-kapt")
}

kotlin {
    jvmToolchain(17)
}

android {
    namespace = "com.sobczal2.biteright"
    compileSdk = 34

    defaultConfig {
        applicationId = "com.sobczal2.biteright"
        minSdk = 26
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"
        vectorDrawables {
            useSupportLibrary = true
        }

        manifestPlaceholders.apply {
            set("auth0Scheme", "@string/com_auth0_scheme")
            set("auth0Domain", "@string/com_auth0_domain")
        }
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
            resValue("string", "com_auth0_scheme", "app")
            resValue("string", "com_auth0_domain", "bite-right-dev.eu.auth0.com")
            resValue("string", "com_auth0_client_id", "fv8jOqzREbYBcsP9Sp1arZFw7ASfEsPI")
            signingConfig = signingConfigs.getByName("debug")
        }
        debug {
            resValue("string", "com_auth0_scheme", "app")
            resValue("string", "com_auth0_domain", "bite-right-dev.eu.auth0.com")
            resValue("string", "com_auth0_client_id", "fv8jOqzREbYBcsP9Sp1arZFw7ASfEsPI")
        }
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }

    buildFeatures {
        compose = true
    }
    composeOptions {
        kotlinCompilerExtensionVersion = "1.5.1"
    }

    packaging {
        resources {
            excludes += "/META-INF/{AL2.0,LGPL2.1}"
        }
    }

    kotlinOptions {
        jvmTarget = "17"
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_17
        targetCompatibility = JavaVersion.VERSION_17
    }
}

dependencies {

    implementation("androidx.core:core-ktx:1.12.0")
    implementation("androidx.lifecycle:lifecycle-runtime-ktx:2.7.0")
    implementation("androidx.activity:activity-compose:1.8.2")
    implementation(platform("androidx.compose:compose-bom:2024.02.01"))
    implementation("androidx.compose.ui:ui:1.6.2")
    implementation("androidx.compose.ui:ui-graphics:1.6.2")
    implementation("androidx.compose.ui:ui-tooling-preview:1.6.2")
    implementation("androidx.compose.material3:material3-android:1.2.0")
    implementation("androidx.lifecycle:lifecycle-runtime-compose:2.7.0")
    testImplementation("junit:junit:4.13.2")
    androidTestImplementation("androidx.test.ext:junit:1.1.5")
    androidTestImplementation("androidx.test.espresso:espresso-core:3.5.1")
    androidTestImplementation(platform("androidx.compose:compose-bom:2024.02.01"))
    androidTestImplementation("androidx.compose.ui:ui-test-junit4:1.6.2")
    debugImplementation("androidx.compose.ui:ui-tooling:1.6.2")
    debugImplementation("androidx.compose.ui:ui-test-manifest:1.6.2")

    // navigation
    implementation("androidx.navigation:navigation-compose:2.7.7")

    // hilt
    implementation("com.google.dagger:hilt-android:2.50")
    implementation("androidx.hilt:hilt-navigation-compose:1.2.0")
    kapt("com.google.dagger:hilt-android-compiler:2.50")

    // auth0
    implementation("com.auth0.android:auth0:2.10.2")
    implementation("com.auth0.android:jwtdecode:2.0.2")

    // arrow
    implementation("io.arrow-kt:arrow-core:1.2.1")

    // retrofit
    implementation("com.squareup.retrofit2:retrofit:2.9.0")
    implementation("com.squareup.retrofit2:converter-gson:2.9.0")

    // okhttp
    implementation("com.squareup.okhttp3:okhttp:4.12.0")
    implementation("com.squareup.okhttp3:logging-interceptor:4.12.0")

    // gson
    implementation("com.google.code.gson:gson:2.10.1")

    // coroutines
    implementation("org.jetbrains.kotlinx:kotlinx-coroutines-android:1.8.0")

    // coil
    implementation("io.coil-kt:coil-compose:2.5.0")

    // icons
    implementation("androidx.compose.material:material-icons-extended:1.6.2")
}

kapt {
    correctErrorTypes = true
}