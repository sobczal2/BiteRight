package com.sobczal2.biteright

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import com.sobczal2.biteright.routing.Router
import dagger.hilt.android.AndroidEntryPoint

@AndroidEntryPoint
class BiteRightActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            App {
                Router()
            }
        }
    }
}