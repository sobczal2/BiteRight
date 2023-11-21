use crate::handlers::unit::create::create;
use crate::handlers::unit::delete::delete;
use crate::handlers::unit::list::list;
use axum::Router;

pub fn create_unit_router() -> Router {
    Router::new()
        .route("/list", axum::routing::get(list))
        .route("/create", axum::routing::post(create))
        .route("/delete/:unit_id", axum::routing::delete(delete))
}
