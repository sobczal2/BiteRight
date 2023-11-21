use thiserror::Error;

#[derive(Debug, Error)]
pub enum ConfigError {
    #[error("Dotenv file not found error")]
    DotenvFileNotFound,
    #[error("Config error: {0}")]
    Config(#[from] config::ConfigError),
    #[error("DATABASE_URL missing")]
    DatabaseUrlMissing,
}
