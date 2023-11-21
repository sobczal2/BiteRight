use crate::config::AppConfig;
use crate::db::refresh_token::{
    create_refresh_token, delete_refresh_tokens_for_user, find_refresh_token_by_user_id,
};
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::user::{RefreshRequest, RefreshResponse};
use crate::models::query_objects::refresh_token::CreateRefreshTokenQuery;
use crate::utils::token::{generate_jwt, generate_refresh_token};
use axum::{Extension, Json};
use sqlx::types::chrono::Utc;
use sqlx::PgPool;
use std::sync::Arc;
use std::time::Duration;

pub async fn refresh(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(refresh_request): ValidatedJson<RefreshRequest>,
) -> Result<Json<RefreshResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let refresh_token_in_db =
        find_refresh_token_by_user_id(&mut tx, refresh_request.user_id).await?;

    let refresh_token_in_db = match refresh_token_in_db {
        Some(refresh_token_in_db) => refresh_token_in_db,
        None => return Err(ApiError::unauthorized()),
    };

    if refresh_token_in_db.token != refresh_request.refresh_token {
        return Err(ApiError::unauthorized());
    }

    if refresh_token_in_db.expiration < Utc::now().naive_utc() {
        return Err(ApiError::unauthorized());
    }

    delete_refresh_tokens_for_user(&mut tx, refresh_request.user_id).await?;

    let jwt = generate_jwt(refresh_request.user_id, &app_config.token)
        .map_err(|_| ApiError::internal_error())?;

    let refresh_token = generate_refresh_token(&app_config.token);

    let expiration =
        Utc::now() + Duration::from_secs(app_config.token.refresh_token_expiration_seconds);

    let refresh_token = create_refresh_token(
        &mut tx,
        CreateRefreshTokenQuery {
            user_id: refresh_request.user_id,
            token: refresh_token,
            expiration: expiration.naive_utc(),
        },
    )
    .await?;

    tx.commit().await?;

    Ok(Json(RefreshResponse {
        user_id: refresh_request.user_id,
        jwt,
        refresh_token: refresh_token.token,
    }))
}
