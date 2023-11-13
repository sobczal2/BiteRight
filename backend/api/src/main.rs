use std::sync::Arc;
use axum::{Extension, Router};
use axum::routing::get;
use tower_http::trace::TraceLayer;
use config::AppConfig;
use endpoints::user::create_user_router;
use errors::user::common::AuthError;
use services::token_service::Claims;
use utils::db::create_pg_pool;

#[tokio::main]
async fn main() -> anyhow::Result<()> {
    let config = Arc::new(AppConfig::from_env()?);

    tracing_subscriber::fmt::init();

    let pool = create_pg_pool(config.database_url.clone()).await?;
    let addr = config.app_addr.clone();

    let app = Router::new()
        .nest("/user", create_user_router())
        .route("/test", get(protected))
        .layer(Extension(config))
        .layer(Extension(pool))
        .layer(TraceLayer::new_for_http());

    tracing::debug!("Listening on {}", addr);

    axum::Server::bind(&addr)
        .serve(app.into_make_service())
        .await?;

    Ok(())
}

async fn protected(claims: Claims) -> Result<String, AuthError> {
    Ok(format!(
        "Welcome to the protected area :)\nYour data:\n{:?}",
        claims
    ))
}