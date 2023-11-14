use thiserror::Error;

#[derive(Debug, Error)]
pub enum DbError {
    #[error("Failed to connect to database")]
    Connection,
}
