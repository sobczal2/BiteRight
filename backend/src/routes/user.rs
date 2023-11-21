use axum::Router;

use crate::handlers::user::me::me;
use crate::handlers::user::refresh::refresh;
use crate::handlers::user::sign_in::sign_in;
use crate::handlers::user::sign_up::sign_up;

pub fn create_user_router() -> Router {
    Router::new()
        .route("/sign-up", axum::routing::post(sign_up))
        .route("/sign-in", axum::routing::post(sign_in))
        .route("/me", axum::routing::get(me))
        .route("/refresh", axum::routing::post(refresh))
}
