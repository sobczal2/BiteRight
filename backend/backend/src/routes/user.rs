use axum::body::HttpBody;
use axum::Router;

pub fn create_user_router<S, B>() -> Router<S, B>
    where
        B: HttpBody + Send + 'static,
        S: Clone + Send + Sync + 'static
{
    Router::new()
}