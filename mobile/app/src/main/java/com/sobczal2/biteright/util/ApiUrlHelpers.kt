package com.sobczal2.biteright.util

import androidx.compose.runtime.Composable
import androidx.compose.ui.res.stringResource
import com.sobczal2.biteright.R
import java.util.UUID

@Composable
fun getCategoryPhotoUrl(categoryId: UUID): String {
    return stringResource(id = R.string.api_url) + "categories/${categoryId}/photo"
}
