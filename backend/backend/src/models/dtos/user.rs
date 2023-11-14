use std::sync::Arc;
use axum::{async_trait, RequestPartsExt, TypedHeader};
use axum::extract::FromRequestParts;
use axum::headers::Authorization;
use axum::headers::authorization::Bearer;
use axum::http::request::Parts;
use jsonwebtoken::DecodingKey;
use serde::{Deserialize, Serialize};
use sqlx::types::chrono::Utc;
use crate::config::AppConfig;
use crate::errors::models::AuthError;


#[derive(Debug, Deserialize, Serialize)]
pub struct Claims {
    pub sub: i32,
    pub exp: usize,
    pub nbf: usize,
}

#[async_trait]
impl<S> FromRequestParts<S> for Claims
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

        let token_data = jsonwebtoken::decode::<Claims>(
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
