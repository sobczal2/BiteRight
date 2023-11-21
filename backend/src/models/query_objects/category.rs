use sqlx::types::chrono::NaiveDateTime;

pub struct ListCategoriesForUserQuery {
    pub user_id: i32,
    pub page: i32,
    pub per_page: i32,
}

pub struct FetchCategoryQueryResult {
    pub category_id: i32,
    pub name: String,
    pub photo_name: Option<String>,
    pub created_at: NaiveDateTime,
    pub updated_at: NaiveDateTime,
}

pub struct CreateCategoryForUserQuery {
    pub name: String,
    pub photo_id: Option<i32>,
    pub user_id: i32,
}