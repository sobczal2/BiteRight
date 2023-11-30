import androidx.compose.foundation.border
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.tooling.preview.Preview
import com.sobczal2.biteright.ui.theme.spacing

@Composable
fun BiteRightErrorLabel(
    enabled: Boolean,
    text: String,
    modifier: Modifier = Modifier
) {
    if (enabled) {
        Text(
            text = text,
            modifier = modifier
                .background(
                    color = MaterialTheme.colorScheme.error,
                    shape = RoundedCornerShape(MaterialTheme.spacing.small)
                )
                .padding(MaterialTheme.spacing.small),
            style = MaterialTheme.typography.bodySmall,
            color = MaterialTheme.colorScheme.onError
        )
    }
}

@Composable
@Preview
fun BiteRightErrorLabelPreview() {
    BiteRightErrorLabel(
        enabled = true,
        text = "Something went wrong"
    )
}
