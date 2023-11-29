use sqlx::postgres::types::{PgInterval, PgMoney};
use sqlx::types::chrono::NaiveDateTime;

pub struct CreateTemplateForUserQuery {
    pub name: String,
    pub expiration_span: PgInterval,
    pub amount: f64,
    pub unit_id: i32,
    pub price: Option<PgMoney>,
    pub currency_id: Option<i32>,
    pub category_id: i32,
    pub user_id: i32,
}

pub struct ListTemplatesForUserQuery {
    pub user_id: i32,
    pub page: i32,
    pub per_page: i32,
}

pub struct FetchTemplateQueryResult {
    pub template_id: i32,
    pub name: String,
    pub expiration_span: PgInterval,
    pub amount: f64,
    pub unit_id: i32,
    pub price: Option<PgMoney>,
    pub currency_id: Option<i32>,
    pub category_id: i32,
    pub created_at: NaiveDateTime,
    pub updated_at: NaiveDateTime,
    pub can_modify: bool,
}