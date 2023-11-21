use axum::http::StatusCode;
use axum::response::{IntoResponse, Response};
use axum::Json;
use serde_json::json;

pub struct ApiError {
    status: StatusCode,
    message: serde_json::Value,
}

impl ApiError {
    pub fn new(status: StatusCode, message: &str) -> Self {
        ApiError {
            status,
            message: json!({ "errors": { "error": message } }),
        }
    }

    pub fn internal_error() -> Self {
        ApiError::new(StatusCode::INTERNAL_SERVER_ERROR, "Internal Server Error")
    }

    pub fn bad_request(message: &str) -> Self {
        ApiError::new(StatusCode::BAD_REQUEST, message)
    }

    pub fn unauthorized() -> Self {
        ApiError::new(StatusCode::UNAUTHORIZED, "Unauthorized")
    }

    pub(crate) fn not_found(message: &str) -> ApiError {
        ApiError::new(StatusCode::NOT_FOUND, message)
    }

    pub fn new_with_json(status: StatusCode, message: serde_json::Value) -> Self {
        ApiError { status, message }
    }
}

impl From<sqlx::Error> for ApiError {
    fn from(err: sqlx::Error) -> Self {
        tracing::error!("Database error: {}", err);
        ApiError::internal_error()
    }
}

impl IntoResponse for ApiError {
    fn into_response(self) -> Response {
        let body = Json(self.message).into_response();
        (self.status, body).into_response()
    }
}
