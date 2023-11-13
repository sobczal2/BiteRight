use std::sync::Arc;
use axum::{async_trait, RequestPartsExt, TypedHeader};
use axum::extract::FromRequestParts;
use axum::headers::Authorization;
use axum::headers::authorization::Bearer;
use axum::http::request::Parts;
use chrono::{Duration, Utc};
use jsonwebtoken::{DecodingKey, encode, EncodingKey, Header};
use rand::Rng;
use serde::{Deserialize, Serialize};
use config::AppConfig;
use errors::services::token_service::TokenServiceError;
use config::token_config::TokenConfig;
use domain::refresh_token::RefreshToken;
use errors::user::common::AuthError;

pub struct TokenService {
    secret: String,
    jwt_valid_for: Duration,
    refresh_token_valid_for: Duration,
    refresh_token_length: usize,
}

impl TokenService {
    pub fn new(
        token_config: &TokenConfig
    ) -> Self {
        Self {
            secret: token_config.secret.clone(),
            jwt_valid_for: token_config.jwt_valid_for.clone(),
            refresh_token_valid_for: token_config.refresh_token_valid_for.clone(),
            refresh_token_length: token_config.refresh_token_length.clone(),
        }
    }

    pub fn generate_jwt(&self, user_id: i32) -> Result<String, TokenServiceError> {
        let expiration = Utc::now() + self.jwt_valid_for;

        let claims = Claims {
            sub: user_id,
            exp: expiration.timestamp() as usize,
            nbf: Utc::now().timestamp() as usize,
        };

        let header = Header::default();

        let token = encode(&header, &claims, &EncodingKey::from_secret(self.secret.as_bytes())).map_err(|_| TokenServiceError::JwtGenerationError)?;
        Ok(token)
    }

    pub fn generate_refresh_token(&self, user_id: i32) -> RefreshToken {
        let expiration = Utc::now() + self.refresh_token_valid_for;
        let token = rand::thread_rng()
            .sample_iter(&rand::distributions::Alphanumeric)
            .take(self.refresh_token_length)
            .map(char::from)
            .collect::<String>();

        RefreshToken::new(user_id, token, expiration.naive_utc())
    }
}

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
            &DecodingKey::from_secret(app_config.token_config.secret.as_bytes()),
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
