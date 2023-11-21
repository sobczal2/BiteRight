use axum::Router;
use crate::handlers::category::create::create;
use crate::handlers::category::list::list;
use crate::handlers::category::delete::delete;

pub fn create_category_router() -> Router {
    Router::new()
        .route("/list", axum::routing::get(list))
        .route("/create", axum::routing::post(create))
        .route("/delete/:category_id", axum::routing::delete(delete))
}