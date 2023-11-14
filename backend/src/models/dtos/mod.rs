use axum::{async_trait, Json, RequestExt};
use axum::extract::FromRequest;
use axum::http::{Request, StatusCode};
use serde_json::json;
use validator::Validate;
use crate::errors::api::ApiError;

pub mod user;
mod refresh_token;

pub struct ValidatedJson<J>(pub J);

#[async_trait]
impl<S, B, J> FromRequest<S, B> for ValidatedJson<J>
    where
        B: Send + 'static,
        S: Send + Sync,
        J: Validate + serde::de::DeserializeOwned + Send + 'static,
        Json<J>: FromRequest<(), B>,
{
    type Rejection = ApiError;

    async fn from_request(req: Request<B>, _state: &S) -> Result<Self, Self::Rejection> {
        let Json(data) = req
            .extract::<Json<J>, _>()
            .await
            .map_err(|_| ApiError::new(StatusCode::BAD_REQUEST, "Invalid JSON"))?;

        data.validate().map_err(|err| ApiError::new_with_json(
            StatusCode::BAD_REQUEST, json!({ "errors": err }),
        ))?;
        Ok(Self(data))
    }
}