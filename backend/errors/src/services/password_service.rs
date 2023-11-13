use thiserror::Error;

#[derive(Debug, Error)]
pub enum PasswordServiceError {
    #[error("Password hash error")]
    PasswordHashError,
    #[error("Password verify error")]
    PasswordVerifyError,
}