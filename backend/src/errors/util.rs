use thiserror::Error;

#[derive(Debug, Error)]
pub enum UtilError {
    #[error("Failed to create pool")]
    CreatePool,
    #[error("Password hash error")]
    PasswordHash,
    #[error("Token generation error")]
    TokenGeneration,
}
