use std::env;
use chrono::Duration;
use errors::config::ConfigError;

const TOKEN_SECRET: &str = "TOKEN_SECRET";
const TOKEN_JWT_VALID_FOR_SECONDS: &str = "TOKEN_JWT_VALID_FOR_SECONDS";
const TOKEN_REFRESH_TOKEN_VALID_FOR_SECONDS: &str = "TOKEN_REFRESH_TOKEN_VALID_FOR_SECONDS";
const TOKEN_REFRESH_TOKEN_LENGTH: &str = "TOKEN_REFRESH_TOKEN_LENGTH";

pub struct TokenConfig {
    pub secret: String,
    pub jwt_valid_for: Duration,
    pub refresh_token_valid_for: Duration,
    pub refresh_token_length: usize,
}

impl TokenConfig {
    pub fn from_env() -> Result<Self, ConfigError> {
        Ok(TokenConfig {
            secret: get_token_secret()?,
            jwt_valid_for: get_token_jwt_valid_for_seconds()?,
            refresh_token_valid_for: get_token_refresh_token_valid_for_seconds()?,
            refresh_token_length: get_token_refresh_token_length()?,
        })
    }
}

fn get_token_secret() -> Result<String, ConfigError> {
    env::var(TOKEN_SECRET)
        .map_err(|_| ConfigError::MissingEnvVar(TOKEN_SECRET.to_string()))
}

fn get_token_jwt_valid_for_seconds() -> Result<Duration, ConfigError> {
    let jwt_valid_for_str = env::var(TOKEN_JWT_VALID_FOR_SECONDS)
        .map_err(|_| ConfigError::MissingEnvVar(TOKEN_JWT_VALID_FOR_SECONDS.to_string()))?;

    let duration_seconds = jwt_valid_for_str.parse()
        .map_err(|_| ConfigError::InvalidEnvVar(TOKEN_JWT_VALID_FOR_SECONDS.to_string()))?;

    Ok(Duration::seconds(duration_seconds))
}

fn get_token_refresh_token_valid_for_seconds() -> Result<Duration, ConfigError> {
    let refresh_token_valid_for_str = env::var(TOKEN_REFRESH_TOKEN_VALID_FOR_SECONDS)
        .map_err(|_| ConfigError::MissingEnvVar(TOKEN_REFRESH_TOKEN_VALID_FOR_SECONDS.to_string()))?;

    let duration_seconds = refresh_token_valid_for_str.parse()
        .map_err(|_| ConfigError::InvalidEnvVar(TOKEN_REFRESH_TOKEN_VALID_FOR_SECONDS.to_string()))?;

    Ok(Duration::seconds(duration_seconds))
}

fn get_token_refresh_token_length() -> Result<usize, ConfigError> {
    let refresh_token_length_str = env::var(TOKEN_REFRESH_TOKEN_LENGTH)
        .map_err(|_| ConfigError::MissingEnvVar(TOKEN_REFRESH_TOKEN_LENGTH.to_string()))?;

    refresh_token_length_str.parse()
        .map_err(|_| ConfigError::InvalidEnvVar(TOKEN_REFRESH_TOKEN_LENGTH.to_string()))
}
