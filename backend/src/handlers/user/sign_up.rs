use std::sync::Arc;
use axum::{Extension, Json};
use axum::extract::State;
use sqlx::PgPool;
use crate::config::AppConfig;
use crate::errors::api::ApiError;
use crate::models::dtos::user::{SignUpRequest, SignUpResponse};
use crate::models::dtos::ValidatedJson;

pub async fn sign_up(
    Extension(pool): Extension<PgPool>,
    State(app_config): State<Arc<AppConfig>>,
    ValidatedJson(sign_up_request): ValidatedJson<SignUpRequest>,
) -> Result<Json<SignUpResponse>, ApiError> {
    Ok()
}