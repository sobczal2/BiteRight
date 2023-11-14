use crate::config::AppConfig;
use crate::routes::user::create_user_router;

use axum::Router;
use std::sync::Arc;

mod user;

pub fn create_router() -> Router {
    Router::new().nest("/user", create_user_router())
}
