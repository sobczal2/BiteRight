use serde::{Deserialize, Serialize};
use validator::Validate;

#[derive(Deserialize, Validate)]
pub struct SignInRequestDto {
    #[validate(email(message = "Invalid email"))]
    pub email: String,
    #[validate(length(min = 8, max = 128, message = "Password must be between 8 and 128 characters"))]
    pub password: String,
}

#[derive(Serialize)]
pub struct SignInResponseDto {
    pub user_id: i32,
    pub jwt: String,
    pub refresh_token: String,
}
