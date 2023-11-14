use sqlx::types::chrono::NaiveDateTime;

pub struct CreateRefreshTokenQuery {
    pub user_id: i32,
    pub token: String,
    pub expiration: NaiveDateTime,
}
