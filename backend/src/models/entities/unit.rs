use sqlx::types::chrono::NaiveDateTime;

pub struct Unit {
    pub unit_id: i32,
    pub name: String,
    pub abbreviation: String,
    pub created_at: NaiveDateTime,
    pub updated_at: NaiveDateTime,
}
