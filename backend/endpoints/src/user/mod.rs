mod sign_up;

use axum::Router;
use axum::routing::post;
use crate::user::sign_up::sign_up;

pub fn create_user_router() -> Router {
    Router::new()
        .route("/sign-up", post(sign_up))
}