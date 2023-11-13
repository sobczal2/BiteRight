use chrono::NaiveDateTime;

pub struct User {
    pub user_id: i32,
    pub name: String,
    pub email: String,
    pub password_hash: String,
    pub created_at: NaiveDateTime,
}

impl User {
    pub fn new(name: String, email: String, password_hash: String) -> Self {
        Self {
            user_id: 0,
            name,
            email,
            password_hash,
            created_at: chrono::Utc::now().naive_utc(),
        }
    }
}