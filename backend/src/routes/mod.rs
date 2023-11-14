use crate::config::AppConfig;
use crate::routes::user::create_user_router;
use axum::body::HttpBody;
use axum::Router;
use std::sync::Arc;

mod user;

pub fn create_router() -> Router<Arc<AppConfig>> {
    Router::new().nest("/user", create_user_router())
}
