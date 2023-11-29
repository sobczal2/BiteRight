use crate::errors::util::UtilError;
use sqlx::postgres::PgPoolOptions;
use sqlx::PgPool;

pub async fn create_pg_pool(
    database_url: String,
    max_connections: u32,
) -> Result<PgPool, UtilError> {
    let pool = PgPoolOptions::new()
        .max_connections(max_connections)
        .connect(&database_url)
        .await
        .map_err(|_| UtilError::CreatePool)?;
    Ok(pool)
}

pub fn get_skip_and_take(page: i32, per_page: i32) -> (i32, i32) {
    let skip = page * per_page;
    let take = per_page;
    (skip, take)
}