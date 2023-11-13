use argon2::{Argon2, PasswordHash, PasswordHasher, PasswordVerifier};
use argon2::password_hash::rand_core::OsRng;
use argon2::password_hash::SaltString;
use errors::services::password_service::PasswordServiceError;

pub struct PasswordService;

impl PasswordService {
    pub fn new() -> Self {
        Self {}
    }

    pub fn hash_password(&self, password: &str) -> Result<String, PasswordServiceError> {
        let argon = Argon2::default();
        let salt = SaltString::generate(&mut OsRng);

        Ok(
            argon.hash_password(password.as_bytes(), &salt)
                .map_err(|_| PasswordServiceError::PasswordHashError)?
                .to_string()
        )
    }

    pub fn verify_password(&self, password: &str, password_hash: &str) -> Result<bool, PasswordServiceError> {
        let argon = Argon2::default();
        let password_hash = PasswordHash::new(password_hash).map_err(|_| PasswordServiceError::PasswordVerifyError)?;

        Ok(
            argon.verify_password(password.as_bytes(), &password_hash)
                .is_ok()
        )
    }
}