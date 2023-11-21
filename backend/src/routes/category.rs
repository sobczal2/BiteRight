use axum::Router;
use crate::handlers::category::create::create;
use crate::handlers::category::list::list;

pub fn create_category_router() -> Router {
    Router::new()
        .route("/list", axum::routing::get(list))
        .route("/create", axum::routing::post(create))
}