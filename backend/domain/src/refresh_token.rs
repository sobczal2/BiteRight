use chrono::{NaiveDateTime};

pub struct RefreshToken {
    pub refresh_token_id: i32,
    pub user_id: i32,
    pub token: String,
    pub expiration: NaiveDateTime,
}

impl RefreshToken {
    pub fn new(user_id: i32, token: String, expiration: NaiveDateTime) -> Self {
        Self {
            refresh_token_id: 0,
            user_id,
            token,
            expiration,
        }
    }
}