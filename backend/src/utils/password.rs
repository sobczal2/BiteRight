use argon2::{Argon2, PasswordHash, PasswordHasher, PasswordVerifier};
use argon2::password_hash::rand_core::OsRng;
use argon2::password_hash::SaltString;
use crate::errors::util::UtilError;

pub fn hash_password(password: &str) -> Result<String, UtilError> {
    let argon = Argon2::default();
    let salt = SaltString::generate(&mut OsRng);

    Ok(
        argon.hash_password(password.as_bytes(), &salt)
            .map_err(|_| UtilError::PasswordHash)?
            .to_string()
    )
}

pub fn verify_password(password_hash: &str, password: &str) -> Result<bool, UtilError> {
    let argon = Argon2::default();
    let password_hash = PasswordHash::new(password_hash).map_err(|_| UtilError::PasswordHash)?;

    Ok(
        argon.verify_password(password.as_bytes(), &password_hash)
            .is_ok()
    )
}