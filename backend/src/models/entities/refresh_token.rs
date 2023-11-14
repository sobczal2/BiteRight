use sqlx::types::chrono::NaiveDateTime;

pub struct RefreshToken {
    pub refresh_token_id: i32,
    pub user_id: i32,
    pub token: String,
    pub expiration: NaiveDateTime,
}
