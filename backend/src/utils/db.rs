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
