use std::sync::Arc;
use axum::{Extension, Json};
use sqlx::PgPool;
use crate::config::AppConfig;
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::user::{RefreshRequest, RefreshResponse};

pub async fn refresh(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(refresh_request): ValidatedJson<RefreshRequest>,
) -> Result<Json<RefreshResponse>, ApiError> {

}