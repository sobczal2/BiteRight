use crate::config::AssetsConfig;
use axum::Router;
use tower_http::services::ServeDir;

pub fn create_assets_router(assets_config: &AssetsConfig) -> Router {
    Router::new().nest_service("/photos", ServeDir::new(assets_config.get_photo_dir()))
}
