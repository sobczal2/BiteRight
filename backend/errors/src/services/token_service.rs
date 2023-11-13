use thiserror::Error;

#[derive(Debug, Error)]
pub enum TokenServiceError {
    #[error("JWT generation error")]
    JwtGenerationError,
}