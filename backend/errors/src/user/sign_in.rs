use axum::http::StatusCode;
use axum::Json;
use axum::response::{IntoResponse, Response};
use serde_json::json;
use thiserror::Error;

#[derive(Debug, Error)]
pub enum SignInError {
    #[error("Unknown error")]
    Unknown,
    #[error("Invalid credentials")]
    InvalidCredentials,
}

impl IntoResponse for SignInError {
    fn into_response(self) -> Response {
        let (status, error_message) = match self {
            Self::Unknown => (
                StatusCode::INTERNAL_SERVER_ERROR,
                "Internal Server Error".to_string(),
            ),
            Self::InvalidCredentials => (StatusCode::BAD_REQUEST, "Invalid credentials".to_string()),
        };

        (status, Json(json!({"error": error_message}))).into_response()
    }
}
