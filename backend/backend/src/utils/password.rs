pub fn hash_password(&self, password: &str) -> Result<String, PasswordServiceError> {
    let argon = Argon2::default();
    let salt = SaltString::generate(&mut OsRng);

    Ok(
        argon.hash_password(password.as_bytes(), &salt)
            .map_err(|_| PasswordServiceError::PasswordHashError)?
            .to_string()
    )
}