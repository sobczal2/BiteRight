use std::sync::Arc;
use axum::{async_trait, RequestPartsExt, TypedHeader};
use axum::extract::FromRequestParts;
use axum::headers::Authorization;
use axum::headers::authorization::Bearer;
use axum::http::request::Parts;
use jsonwebtoken::{decode, DecodingKey};
use serde::{Deserialize, Serialize};
use sqlx::types::chrono::Utc;
use crate::config::AppConfig;
use crate::errors::models::AuthError;


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
    type Rejection = AuthError;

    async fn from_request_parts(parts: &mut Parts, _state: &S) -> Result<Self, Self::Rejection> {
        let TypedHeader(Authorization(bearer)) = parts
            .extract::<TypedHeader<Authorization<Bearer>>>()
            .await
            .map_err(|_| AuthError::InvalidToken)?;

        let app_config = parts.extensions.get::<Arc<AppConfig>>().ok_or(AuthError::Unknown)?;

        let token_data = decode::<ClaimsDto>(
            bearer.token(),
            &DecodingKey::from_secret(app_config.token.jwt_secret.as_bytes()),
            &jsonwebtoken::Validation::default(),
        ).map_err(|_| AuthError::InvalidToken)?;

        let now = Utc::now().timestamp() as usize;

        if token_data.claims.exp < now {
            return Err(AuthError::InvalidToken);
        }

        if token_data.claims.nbf > now {
            return Err(AuthError::InvalidToken);
        }

        Ok(token_data.claims)
    }
}

#[derive(Debug, Deserialize)]
pub struct SignUpRequest {
    pub email: String,
    pub password: String,
}

#[derive(Debug, Serialize)]
pub struct SignUpResponse {
    pub id: i32,
    pub jwt: String,
    pub refresh_token: String,
}