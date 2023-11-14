use axum::body::HttpBody;
use axum::Router;
use crate::routes::user::create_user_router;

mod user;

pub fn create_router<S, B>() -> Router<S, B>
    where
        B: HttpBody + Send + 'static,
        S: Clone + Send + Sync + 'static
{
    Router::new()
        .nest("/user", create_user_router())
}