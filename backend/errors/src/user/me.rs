use axum::http::StatusCode;
use axum::Json;
use axum::response::{IntoResponse, Response};
use serde_json::json;
use thiserror::Error;

#[derive(Debug, Error)]
pub enum MeError{
    #[error("Unknown error")]
    Unknown,
}

impl IntoResponse for MeError {
    fn into_response(self) -> Response {
        let (status, error_message) = match self {
            Self::Unknown => (
                StatusCode::INTERNAL_SERVER_ERROR,
                "Internal Server Error".to_string(),
            ),
        };

        (status, Json(json!({"error": error_message}))).into_response()
    }
}
