use axum::{async_trait, Json, RequestExt};
use axum::extract::{FromRequest, FromRequestParts, Query};
use axum::http::{Request, StatusCode};
use axum::response::IntoResponse;
use serde::{Deserialize, Serialize};
use serde::de::DeserializeOwned;
use serde_json::json;
use validator::Validate;
use crate::errors::api::ApiError;

#[derive(Debug, Serialize, Deserialize)]
pub struct PaginatedDto<T> {
    pub items: Vec<T>,
    pub total: i32,
    pub page: i32,
    pub per_page: i32,
    pub total_pages: i32,
}

impl<T> PaginatedDto<T> {
    pub fn new(vec: Vec<T>, total: i32, page: i32, per_page: i32) -> Self {
        let total_pages = if total % per_page == 0 {
            total / per_page
        } else {
            total / per_page + 1
        };

        Self {
            items: vec,
            total,
            page,
            per_page,
            total_pages,
        }
    }
}

pub struct ValidatedJson<T>(pub T);

#[async_trait]
impl<S, B, T> FromRequest<S, B> for ValidatedJson<T>
    where
        B: Send + 'static,
        S: Send + Sync,
        T: Validate + DeserializeOwned + Send + 'static,
        Json<T>: FromRequest<(), B>,
{
    type Rejection = ApiError;

    async fn from_request(req: Request<B>, _state: &S) -> Result<Self, Self::Rejection> {
        let Json(data) = req
            .extract::<Json<T>, _>()
            .await
            .map_err(|_| ApiError::new(StatusCode::BAD_REQUEST, "Invalid JSON"))?;

        data.validate().map_err(|err| {
            ApiError::new_with_json(StatusCode::BAD_REQUEST, json!({ "errors": err }))
        })?;
        Ok(Self(data))
    }
}

pub struct ValidatedQuery<T>(pub T);

#[async_trait]
impl<S, T> FromRequestParts<S> for ValidatedQuery<T>
    where
        S: Send + Sync,
        T: Validate + DeserializeOwned + Send + 'static,
{
    type Rejection = ApiError;

    async fn from_request_parts(parts: &mut axum::http::request::Parts, _state: &S) -> Result<Self, Self::Rejection> {
        let query = parts
            .uri
            .query()
            .ok_or(ApiError::new(StatusCode::BAD_REQUEST, "Invalid query"))?;

        let query = serde_urlencoded::from_str::<T>(query)
            .map_err(|_| ApiError::new(StatusCode::BAD_REQUEST, "Invalid query"))?;

        query.validate().map_err(|err| {
            ApiError::new_with_json(StatusCode::BAD_REQUEST, json!({ "errors": err }))
        })?;
        Ok(Self(query))
    }
}