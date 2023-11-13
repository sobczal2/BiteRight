use serde::{Deserialize, Serialize};
use validator::Validate;

#[derive(Deserialize, Validate)]
pub struct SignUpRequestDto {
    #[validate(length(min = 5, max = 64, message = "Name must be between 5 and 64 characters"))]
    pub name: String,
    #[validate(email(message = "Invalid email"))]
    pub email: String,
    #[validate(length(min = 8, max = 128, message = "Password must be between 8 and 128 characters"))]
    pub password: String,
}

#[derive(Serialize)]
pub struct SignUpResponseDto {
    pub user_id: i32,
    pub jwt: String,
    pub refresh_token: String,
}