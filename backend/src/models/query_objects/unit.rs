use sqlx::types::chrono::NaiveDateTime;

pub struct CreateUnitForUserQuery {
    pub name: String,
    pub abbreviation: String,
    pub user_id: i32,
}

pub struct ListUnitsForUserQuery {
    pub user_id: i32,
    pub page: i32,
    pub per_page: i32,
}

pub struct FetchUnitQueryResult {
    pub unit_id: i32,
    pub name: String,
    pub abbreviation: String,
    pub can_modify: bool,
    pub created_at: NaiveDateTime,
    pub updated_at: NaiveDateTime,
}
