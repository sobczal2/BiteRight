use chrono::{DateTime, Duration, NaiveDateTime, Utc};
use jsonwebtoken::{encode, EncodingKey, Header};
use rand::Rng;
use errors::services::token_service::TokenServiceError;
use serde::{Deserialize, Serialize};
use config::token_config::TokenConfig;
use domain::refresh_token::RefreshToken;

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
struct Claims {
    sub: i32,
    exp: usize,
    nbf: usize,
}
