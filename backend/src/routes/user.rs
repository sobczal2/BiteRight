use crate::config::AppConfig;
use crate::handlers::user::sign_up::sign_up;
use axum::routing::post;
use axum::Router;
use std::sync::Arc;

pub fn create_user_router() -> Router<Arc<AppConfig>> {
    Router::new().route("/sign-up", post(sign_up))
}
