use thiserror::Error;

#[derive(Debug, Error)]
pub enum UtilError {
    #[error("Failed to create pool")]
    CreatePool,
    #[error("Password hasing error")]
    PasswordHashing,
}