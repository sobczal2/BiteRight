use axum::Router;
use axum::routing::get;
use crate::handlers::unit::list::list;

pub fn create_unit_router() -> Router {
    Router::new()
        .route("/", get(list))
}