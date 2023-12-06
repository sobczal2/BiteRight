use std::collections::HashMap;
use axum::http::StatusCode;
use axum::response::{IntoResponse, Response};
use axum::Json;
use serde::Serialize;
use serde_json::json;
use validator::ValidationErrors;

pub struct ApiError {
    status: StatusCode,
    message: ApiErrorMessage,
}

#[derive(Debug, Serialize)]
pub struct ApiErrorMessage {
    pub errors: HashMap<String, Vec<String>>,
}

impl Into<ApiErrorMessage> for ValidationErrors {
    fn into(self) -> ApiErrorMessage {
        let mut error_map = HashMap::new();

        for (field, errors) in self.field_errors() {
            let mut field_errors = Vec::new();

            for error in errors {
                field_errors.push(error.message.clone());
            }

            error_map.insert(
                field.to_string(),
                field_errors
                    .iter()
                    .map(|e| {
                        match e {
                            Some(e) => e.to_string(),
                            None => String::from("Unknown error"),
                        }
                    }
                    )
                    .collect(),
            );
        }

        ApiErrorMessage { errors: error_map }
    }
}

impl ApiError {
    pub fn new(status: StatusCode, message: &str) -> Self {
        ApiError {
            status,
            message: ApiErrorMessage {
                errors: vec![(String::from("error"), vec![String::from(message)])]
                    .into_iter()
                    .collect(),
            },
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

    pub fn new_with_api_error_message(status: StatusCode, message: ApiErrorMessage) -> Self {
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
        let body = Json(json!(self.message));
        (self.status, body).into_response()
    }
}
