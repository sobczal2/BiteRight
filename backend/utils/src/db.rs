use sqlx::{Pool, Postgres};
use sqlx::postgres::PgPoolOptions;

pub async fn create_pg_pool(database_url: String) -> Result<Pool<Postgres>, sqlx::Error> {
    PgPoolOptions::new()
        .max_connections(50)
        .connect(&database_url)
        .await
}
