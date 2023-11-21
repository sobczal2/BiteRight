
use crate::routes::user::create_user_router;

use axum::Router;
use crate::config::AppConfig;
use crate::routes::assets::create_assets_router;
use crate::routes::category::create_category_router;
use crate::routes::unit::create_unit_router;

mod user;
mod unit;
mod assets;
mod category;

pub fn create_router(app_config: &AppConfig) -> Router {
    Router::new()
        .nest("/user", create_user_router())
        .nest("/unit", create_unit_router())
        .nest("/category", create_category_router())
        .nest("/assets", create_assets_router(&app_config.assets))
}
