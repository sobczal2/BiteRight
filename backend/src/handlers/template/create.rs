use std::sync::Arc;
use axum::Extension;
use sqlx::PgPool;
use crate::config::AppConfig;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::user::ClaimsDto;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
)