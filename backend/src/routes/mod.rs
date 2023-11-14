use crate::routes::user::create_user_router;

use axum::Router;
use crate::routes::unit::create_unit_router;

mod user;
mod unit;

pub fn create_router() -> Router {
    Router::new()
        .nest("/user", create_user_router())
        .nest("/unit", create_unit_router())
}
