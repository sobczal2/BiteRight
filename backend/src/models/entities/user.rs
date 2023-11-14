use sqlx::types::chrono::NaiveDateTime;

pub struct User {
    pub user_id: i32,
    pub name: String,
    pub email: String,
    pub password_hash: String,
    pub created_at: NaiveDateTime,
}
