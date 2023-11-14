use crate::config::AppConfig;
use crate::routes::create_router;
use crate::utils::db::create_pg_pool;
use axum::Extension;
use std::net::SocketAddr;
use std::sync::Arc;

mod config;
mod db;
mod errors;
mod handlers;
mod models;
mod routes;
mod utils;

#[tokio::main]
async fn main() -> anyhow::Result<()> {
    let app_config = Arc::new(AppConfig::new()?);

    tracing_subscriber::fmt::init();

    let pool = create_pg_pool(
        app_config.database.url.clone(),
        app_config.database.max_connections,
    )
    .await?;

    let router = create_router()
        .with_state(app_config.clone())
        .layer(Extension(pool));

    let socket_addr = SocketAddr::new(app_config.host.address.parse()?, app_config.host.port);

    tracing::info!("Listening on {}", &socket_addr);

    axum::Server::bind(&socket_addr)
        .serve(router.into_make_service())
        .await?;

    Ok(())
}
