package com.sobczal2.biteright.util

import android.content.res.Configuration
import androidx.compose.ui.tooling.preview.Preview

@Retention(AnnotationRetention.BINARY)
@Target(
    AnnotationTarget.ANNOTATION_CLASS,
    AnnotationTarget.FUNCTION
)
@Preview(apiLevel = 33, name = "Light")
@Preview(apiLevel = 33, name = "Dark", uiMode = Configuration.UI_MODE_NIGHT_YES)
@Preview(apiLevel = 33, name = "Light", fontScale = 2f)
annotation class BiteRightPreview