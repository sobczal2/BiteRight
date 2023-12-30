package com.sobczal2.biteright

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Surface
import androidx.compose.ui.Modifier
import com.sobczal2.biteright.core.Auth0Manager
import com.sobczal2.biteright.core.BiteRightRouter
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class BiteRightActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val auth0Manager = Auth0Manager(this)

        setContent {
            BiteRightTheme {
                Surface(
                    modifier = Modifier.fillMaxSize()
                ) {
                    BiteRightRouter(auth0Manager)
                }
            }
        }
    }
}