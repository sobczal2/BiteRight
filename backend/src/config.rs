use std::env;
use config::{Config, Environment};
use dotenvy::dotenv;
use serde::Deserialize;
use crate::errors::config::ConfigError;

#[derive(Debug, Deserialize)]
pub struct AppConfig {
    pub database: DatabaseConfig,
    pub token: TokenConfig,
    pub host: HostConfig,
}

#[derive(Debug, Deserialize)]
pub struct DatabaseConfig {
    pub url: String,
    pub max_connections: u32,
}

#[derive(Debug, Deserialize)]
pub struct TokenConfig {
    pub jwt_secret: String,
    pub jwt_expiration_seconds: u64,
    pub refresh_token_length: usize,
    pub refresh_token_expiration_seconds: u64,
}

#[derive(Debug, Deserialize)]
pub struct HostConfig {
    pub address: String,
    pub port: u16,
}

impl AppConfig {
    pub fn new() -> Result<Self, ConfigError> {
        dotenv().ok().ok_or(ConfigError::DotenvFileNotFound)?;

        let cfg = Config::builder()
            .add_source(Environment::default()
                .prefix("APP")
                .try_parsing(true)
                .separator("__"))
            .set_override(
                "database.url",
                env::var("DATABASE_URL").map_err(|_| ConfigError::DatabaseUrlMissing)?,
            )?
            .build()
            .map_err(ConfigError::ConfigError)?;

        cfg.try_deserialize()
            .map_err(ConfigError::ConfigError)
    }
}