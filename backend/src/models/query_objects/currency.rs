use sqlx::types::chrono::NaiveDateTime;

pub struct ListCurrenciesForUserQuery {
    pub user_id: i32,
    pub page: i32,
    pub per_page: i32,
}

pub struct FetchCurrencyQueryResult {
    pub currency_id: i32,
    pub name: String,
    pub code: String,
    pub symbol: String,
    pub created_at: NaiveDateTime,
    pub updated_at: NaiveDateTime,
    pub can_modify: bool,
}