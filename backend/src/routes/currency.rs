use axum::Router;
use crate::handlers::currency::list::list;

pub fn create_currency_router() -> Router {
    Router::new()
        .route("/list", axum::routing::get(list))
}
