use axum::response::{IntoResponse, Response};
use serde_json::json;
use thiserror::Error;

#[derive(Debug, Error)]
pub enum AuthError {
    #[error("Invalid token")]
    InvalidToken,
    #[error("Unknown")]
    Unknown,
}

impl IntoResponse for AuthError {
    fn into_response(self) -> Response {
        let (status, error_message) = match self {
            Self::InvalidToken => (axum::http::StatusCode::UNAUTHORIZED, "Invalid token".to_string()),
            Self::Unknown => (axum::http::StatusCode::INTERNAL_SERVER_ERROR, "Internal Server Error".to_string()),
        };

        (status, axum::Json(json!({"error": error_message}))).into_response()
    }
}