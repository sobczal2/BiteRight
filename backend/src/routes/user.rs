use axum::routing::{get, post};
use axum::Router;

use crate::handlers::user::me::me;
use crate::handlers::user::sign_in::sign_in;
use crate::handlers::user::sign_up::sign_up;

pub fn create_user_router() -> Router {
    Router::new()
        .route("/", get(me))
        .route("/sign-up", post(sign_up))
        .route("/sign-in", post(sign_in))
}
