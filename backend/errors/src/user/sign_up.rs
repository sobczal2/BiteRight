use axum::http::StatusCode;
use axum::Json;
use axum::response::{IntoResponse, Response};
use serde_json::json;
use thiserror::Error;

#[derive(Debug, Error)]
pub enum SignUpError {
    #[error("Unknown error")]
    Unknown,
    #[error("Email already taken")]
    EmailTaken,
    #[error("Name already taken")]
    NameTaken,
}

impl IntoResponse for SignUpError {
    fn into_response(self) -> Response {
        let (status, error_message) = match self {
            Self::Unknown => (
                StatusCode::INTERNAL_SERVER_ERROR,
                "Internal Server Error".to_string(),
            ),
            Self::EmailTaken => (StatusCode::BAD_REQUEST, "Email already taken".to_string()),
            Self::NameTaken => (StatusCode::BAD_REQUEST, "Name already taken".to_string()),
        };

        (status, Json(json!({"error": error_message}))).into_response()
    }
}