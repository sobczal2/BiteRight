pub mod token_config;

use std::env;
use std::net::SocketAddr;
use dotenvy::dotenv;
use errors::config::ConfigError;
use crate::token_config::TokenConfig;

const DATABASE_URL: &str = "DATABASE_URL";
const APP_ADDR: &str = "APP_ADDR";

pub struct AppConfig {
    pub database_url: String,
    pub app_addr: SocketAddr,
    pub token_config: TokenConfig,
}

impl AppConfig {
    pub fn from_env() -> Result<Self, ConfigError> {
        dotenv().map_err(|_| ConfigError::MissingEnvFile)?;


        Ok(AppConfig {
            database_url: get_database_url()?,
            app_addr: get_app_addr()?,
            token_config: TokenConfig::from_env()?,
        })
    }
}

fn get_database_url() -> Result<String, ConfigError> {
    env::var(DATABASE_URL)
        .map_err(|_| ConfigError::MissingEnvVar(DATABASE_URL.to_string()))
}

fn get_app_addr() -> Result<SocketAddr, ConfigError> {
    let app_addr_str = env::var(APP_ADDR)
        .map_err(|_| ConfigError::MissingEnvVar(APP_ADDR.to_string()))?;

    app_addr_str.parse()
        .map_err(|_| ConfigError::InvalidEnvVar(APP_ADDR.to_string()))
}
