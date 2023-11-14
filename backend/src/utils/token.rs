use crate::config::TokenConfig;
use crate::errors::util::UtilError;
use crate::models::dtos::user::ClaimsDto;
use jsonwebtoken::{encode, EncodingKey, Header};
use rand::{thread_rng, Rng};
use sqlx::types::chrono::Utc;
use std::time::Duration;

pub fn generate_jwt(user_id: i32, token_config: &TokenConfig) -> Result<String, UtilError> {
    let expiration = Utc::now() + Duration::from_secs(token_config.jwt_expiration_seconds);

    let claims = ClaimsDto {
        sub: user_id,
        exp: expiration.timestamp() as usize,
        nbf: Utc::now().timestamp() as usize,
    };

    let header = Header::default();

    let token = encode(
        &header,
        &claims,
        &EncodingKey::from_secret(token_config.jwt_secret.as_bytes()),
    )
    .map_err(|_| UtilError::TokenGeneration)?;

    Ok(token)
}

pub fn generate_refresh_token(token_config: &TokenConfig) -> String {
    thread_rng()
        .sample_iter(&rand::distributions::Alphanumeric)
        .take(token_config.refresh_token_length)
        .map(char::from)
        .collect::<String>()
}
