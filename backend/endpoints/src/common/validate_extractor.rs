use axum::{
    async_trait,
    extract::FromRequest,
    Json, RequestExt,
    response::{IntoResponse, Response},
};
use hyper::{Request, StatusCode};
use serde::Serialize;
use serde_json::json;
use validator::{Validate, ValidationErrorsKind};

pub struct ValidatedJson<J>(pub J);

#[async_trait]
impl<S, B, J> FromRequest<S, B> for ValidatedJson<J>
    where
        B: Send + 'static,
        S: Send + Sync,
        J: Validate + serde::de::DeserializeOwned + Send + 'static,
        Json<J>: FromRequest<(), B>,
{
    type Rejection = JsonValidationError;

    async fn from_request(req: Request<B>, _state: &S) -> Result<Self, Self::Rejection> {
        let Json(data) = req
            .extract::<Json<J>, _>()
            .await
            .map_err(|_| JsonValidationError::new(StatusCode::BAD_REQUEST, "Invalid JSON"))?;

        data.validate().map_err(|err| JsonValidationError::new_with_json(
            StatusCode::BAD_REQUEST, json!({ "errors": err })
        ))?;
        Ok(Self(data))
    }
}

pub struct JsonValidationError {
    status: StatusCode,
    message: serde_json::Value,
}

impl JsonValidationError {
    pub fn new(status: StatusCode, message: &str) -> Self {
        JsonValidationError {
            status,
            message: json!({ "errors": { "error": message } }),
        }
    }

    pub fn new_with_json(status: StatusCode, message: serde_json::Value) -> Self {
        JsonValidationError {
            status,
            message,
        }
    }
}

impl IntoResponse for JsonValidationError {
    fn into_response(self) -> Response {
        let body = Json(self.message).into_response();
        (self.status, body).into_response()
    }
}
