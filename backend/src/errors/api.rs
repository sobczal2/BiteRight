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

    pub fn new_with_json(status: StatusCode, message: serde_json::Value) -> Self {
        ApiError { status, message }
    }
}

impl IntoResponse for ApiError {
    fn into_response(self) -> Response {
        let body = Json(self.message).into_response();
        (self.status, body).into_response()
    }
}
