use crate::errors::config::ConfigError;
use config::{Config, Environment};
use dotenvy::dotenv;
use serde::Deserialize;
use std::env;
use std::path::PathBuf;

#[derive(Debug, Deserialize)]
pub struct AppConfig {
    pub database: DatabaseConfig,
    pub token: TokenConfig,
    pub host: HostConfig,
    pub photo: PhotoConfig,
    pub assets: AssetsConfig,
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

#[derive(Debug, Deserialize)]
pub struct PhotoConfig {
    pub extension: String,
    pub width: u32,
    pub height: u32,
}

#[derive(Debug, Deserialize)]
pub struct AssetsConfig {
    pub path: PathBuf,
}

impl AssetsConfig {
    pub fn get_photo_url(&self, photo_name: Option<String>) -> String {
        let photo_dir = PathBuf::from("/assets/photos");
        match photo_name {
            Some(photo_name) => photo_dir
                .clone()
                .join(photo_name)
                .to_str()
                .unwrap()
                .to_string(),
            None => photo_dir.join("default.webp").to_str().unwrap().to_string(),
        }
    }

    pub fn get_photo_dir(&self) -> PathBuf {
        self.path.join("photos")
    }
}

impl AppConfig {
    pub fn new() -> Result<Self, ConfigError> {
        dotenv().ok().ok_or(ConfigError::DotenvFileNotFound)?;

        let cfg = Config::builder()
            .add_source(
                Environment::default()
                    .prefix("APP")
                    .try_parsing(true)
                    .separator("__"),
            )
            .set_override(
                "database.url",
                env::var("DATABASE_URL").map_err(|_| ConfigError::DatabaseUrlMissing)?,
            )?
            .build()
            .map_err(ConfigError::Config)?;

        cfg.try_deserialize().map_err(ConfigError::Config)
    }
}
