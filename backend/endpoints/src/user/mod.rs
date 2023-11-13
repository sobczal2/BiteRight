mod sign_up;
mod sign_in;
mod me;

use axum::Router;
use axum::routing::{get, post};
use crate::user::me::me;
use crate::user::sign_in::sign_in;
use crate::user::sign_up::sign_up;

pub fn create_user_router() -> Router {
    Router::new()
        .route("/sign-up", post(sign_up))
        .route("/sign-in", post(sign_in))
        .route("/me", get(me))
}