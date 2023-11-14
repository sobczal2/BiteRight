use crate::config::AppConfig;
use crate::db::{refresh_token, user};
use crate::errors::api::ApiError;
use crate::models::dtos::user::{SignUpRequest, SignUpResponse};
use crate::models::query_objects::refresh_token::CreateRefreshTokenQuery;
use crate::models::query_objects::user::CreateUserQuery;
use crate::utils::password::hash_password;
use crate::utils::token::{generate_jwt, generate_refresh_token};
use axum::{Extension, Json};
use sqlx::types::chrono::Utc;
use sqlx::PgPool;
use std::sync::Arc;
use std::time::Duration;
use crate::models::dtos::common::ValidatedJson;

pub async fn sign_up(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(sign_up_request): ValidatedJson<SignUpRequest>,
) -> Result<Json<SignUpResponse>, ApiError> {
    let mut tx = pool.begin().await.map_err(|_| ApiError::internal_error())?;

    let exists = user::exists_by_email(&mut *tx, sign_up_request.email.clone())
        .await
        .map_err(|_| ApiError::internal_error())?;
    if exists {
        return Err(ApiError::bad_request("Email already exists"));
    }

    let exists = user::exists_by_name(&mut *tx, sign_up_request.name.clone())
        .await
        .map_err(|_| ApiError::internal_error())?;
    if exists {
        return Err(ApiError::bad_request("Name already exists"));
    }

    let hashed_password =
        hash_password(&sign_up_request.password).map_err(|_| ApiError::internal_error())?;

    let user = user::create(
        &mut *tx,
        CreateUserQuery {
            email: sign_up_request.email.clone(),
            name: sign_up_request.name.clone(),
            password_hash: hashed_password,
        },
    )
    .await
    .map_err(|_| ApiError::internal_error())?;

    let jwt =
        generate_jwt(user.user_id, &app_config.token).map_err(|_| ApiError::internal_error())?;

    let refresh_token = generate_refresh_token(&app_config.token);

    let expiration =
        Utc::now() + Duration::from_secs(app_config.token.refresh_token_expiration_seconds);
    let refresh_token = refresh_token::create(
        &mut *tx,
        CreateRefreshTokenQuery {
            user_id: user.user_id,
            token: refresh_token,
            expiration: expiration.naive_utc(),
        },
    )
    .await
    .map_err(|_| ApiError::internal_error())?;

    tx.commit().await.map_err(|_| ApiError::internal_error())?;

    Ok(Json(SignUpResponse {
        user_id: user.user_id,
        jwt,
        refresh_token: refresh_token.token,
    }))
}
