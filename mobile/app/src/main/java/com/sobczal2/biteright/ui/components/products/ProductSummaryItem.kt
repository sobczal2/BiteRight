package com.sobczal2.biteright.ui.components.products

import android.content.res.Configuration
import androidx.compose.foundation.Image
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.foundation.layout.width
import androidx.compose.material3.Card
import androidx.compose.material3.CircularProgressIndicator
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import coil.compose.AsyncImage
import com.sobczal2.biteright.R
import com.sobczal2.biteright.ui.theme.BiteRightTheme
import com.sobczal2.biteright.util.humanizePeriod
import com.sobczal2.biteright.util.truncateString
import java.time.LocalDate
import java.time.Period

data class ProductSummaryItemState(
    val name: String,
    val expirationDate: LocalDate,
    val categoryImageUri: String?,
    val consumption: Double
)

@Composable
fun ProductSummaryItem(
    productSummaryItemState: ProductSummaryItemState,
    modifier: Modifier = Modifier,
    onClick: () -> Unit = {},
    inPreview: Boolean = false
) {
    Card(
        modifier = modifier
            .clickable(onClick = onClick)
            .fillMaxWidth()
    ) {
        Row {
            if (inPreview) {
                Image(
                    painter = painterResource(id = R.drawable.default_category),
                    contentDescription = "test",
                    modifier = Modifier.size(128.dp)
                )
            } else {
                AsyncImage(
                    model = productSummaryItemState.categoryImageUri,
                    contentDescription = "Category image",
                    fallback = painterResource(id = R.drawable.default_category),
                    modifier = Modifier.size(128.dp)
                )
            }
            Column(
                modifier = Modifier
                    .weight(1f)
                    .padding(16.dp)
            ) {
                Text(
                    text = truncateString(productSummaryItemState.name, 24),
                    style = MaterialTheme.typography.headlineSmall
                )
                ProductExpirationIndicator(expirationDate = productSummaryItemState.expirationDate)
            }
            ProductConsumptionIndicator(consumption = productSummaryItemState.consumption)
        }
    }
}

@Composable
@Preview
@Preview("Dark Theme", uiMode = Configuration.UI_MODE_NIGHT_YES)
fun ProductSummaryItemPreview() {
    BiteRightTheme {
        ProductSummaryItem(
            productSummaryItemState = ProductSummaryItemState(
                name = "Product name Product name",
                expirationDate = LocalDate.now().plusDays(5),
                categoryImageUri = null,
                consumption = 0.2,
            ),
            inPreview = true
        )
    }
}