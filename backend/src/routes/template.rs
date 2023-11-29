use axum::Router;
use crate::handlers::template::create::create;
use crate::handlers::template::list::list;

pub fn create_template_router() -> Router {
    Router::new()
        .route("/create", axum::routing::post(create))
        .route("/list", axum::routing::get(list))
}
