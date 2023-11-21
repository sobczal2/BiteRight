use crate::routes::user::create_user_router;

use crate::config::AppConfig;
use crate::routes::assets::create_assets_router;
use crate::routes::category::create_category_router;
use crate::routes::unit::create_unit_router;
use axum::Router;

mod assets;
mod category;
mod unit;
mod user;

pub fn create_router(app_config: &AppConfig) -> Router {
    Router::new()
        .nest("/user", create_user_router())
        .nest("/unit", create_unit_router())
        .nest("/category", create_category_router())
        .nest("/assets", create_assets_router(&app_config.assets))
}
