use std::sync::Arc;

use axum::extract::FromRequestParts;
use axum::headers::authorization::Bearer;
use axum::headers::Authorization;
use axum::http::request::Parts;
use axum::{async_trait, RequestPartsExt, TypedHeader};
use jsonwebtoken::{decode, DecodingKey};
use serde::{Deserialize, Serialize};
use sqlx::types::chrono::Utc;
use validator::Validate;

use crate::config::AppConfig;
use crate::errors::api::ApiError;
use crate::models::query_objects::user::FetchUserQueryResult;

#[derive(Debug, Deserialize, Serialize)]
pub struct ClaimsDto {
    pub sub: i32,
    pub exp: usize,
    pub nbf: usize,
}

#[async_trait]
impl<S> FromRequestParts<S> for ClaimsDto
where
    S: Send + Sync,
{
    type Rejection = ApiError;

    async fn from_request_parts(parts: &mut Parts, _state: &S) -> Result<Self, Self::Rejection> {
        let TypedHeader(Authorization(bearer)) = parts
            .extract::<TypedHeader<Authorization<Bearer>>>()
            .await
            .map_err(|_| ApiError::unauthorized())?;

        let app_config = parts
            .extensions
            .get::<Arc<AppConfig>>()
            .ok_or(ApiError::internal_error())?;

        let token_data = decode::<ClaimsDto>(
            bearer.token(),
            &DecodingKey::from_secret(app_config.token.jwt_secret.as_bytes()),
            &jsonwebtoken::Validation::default(),
        )
        .map_err(|_| ApiError::unauthorized())?;

        let now = Utc::now().timestamp() as usize;

        if token_data.claims.exp < now {
            return Err(ApiError::unauthorized());
        }

        if token_data.claims.nbf > now {
            return Err(ApiError::unauthorized());
        }

        Ok(token_data.claims)
    }
}

#[derive(Debug, Deserialize, Serialize)]
pub struct UserDto {
    pub user_id: i32,
    pub email: String,
    pub name: String,
}

impl From<FetchUserQueryResult> for UserDto {
    fn from(user: FetchUserQueryResult) -> Self {
        Self {
            user_id: user.user_id,
            email: user.email,
            name: user.name,
        }
    }
}

#[derive(Debug, Deserialize, Validate)]
pub struct SignUpRequest {
    #[validate(email(message = "Invalid email"))]
    pub email: String,
    #[validate(length(
        min = 5,
        max = 32,
        message = "Name must be between 5 and 32 characters"
    ))]
    pub name: String,
    #[validate(length(
        min = 8,
        max = 128,
        message = "Password must be between 8 and 128 characters"
    ))]
    pub password: String,
}

#[derive(Debug, Serialize)]
pub struct SignUpResponse {
    pub user_id: i32,
    pub jwt: String,
    pub refresh_token: String,
}

#[derive(Debug, Deserialize, Validate)]
pub struct SignInRequest {
    #[validate(email(message = "Invalid email"))]
    pub email: String,
    #[validate(length(
        min = 8,
        max = 128,
        message = "Password must be between 8 and 128 characters"
    ))]
    pub password: String,
}

#[derive(Debug, Serialize)]
pub struct SignInResponse {
    pub user_id: i32,
    pub jwt: String,
    pub refresh_token: String,
}

#[derive(Debug, Serialize)]
pub struct MeResponse {
    pub user: UserDto,
}

#[derive(Debug, Deserialize, Validate)]
pub struct RefreshRequest {
    #[validate(length(min = 64, max = 64, message = "Invalid refresh token"))]
    pub refresh_token: String,
    pub user_id: i32,
}

#[derive(Debug, Serialize)]
pub struct RefreshResponse {
    pub user_id: i32,
    pub jwt: String,
    pub refresh_token: String,
}