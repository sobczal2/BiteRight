use axum::http::{StatusCode};
use axum::Json;
use axum::response::{IntoResponse, Response};
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

    pub fn new_with_json(status: StatusCode, message: serde_json::Value) -> Self {
        ApiError {
            status,
            message,
        }
    }
}

impl IntoResponse for ApiError {
    fn into_response(self) -> Response {
        let body = Json(self.message).into_response();
        (self.status, body).into_response()
    }
}
