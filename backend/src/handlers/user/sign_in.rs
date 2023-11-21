use crate::config::AppConfig;
use crate::db::refresh_token::{create_refresh_token, delete_refresh_tokens_for_user};
use crate::db::user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::user::{SignInRequest, SignInResponse};
use crate::models::query_objects::refresh_token::CreateRefreshTokenQuery;
use crate::utils::password::verify_password;
use crate::utils::token::{generate_jwt, generate_refresh_token};
use axum::{Extension, Json};
use sqlx::types::chrono::Utc;
use sqlx::PgPool;
use std::sync::Arc;
use std::time::Duration;

pub async fn sign_in(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(sign_in_request): ValidatedJson<SignInRequest>,
) -> Result<Json<SignInResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let user = user::find_user_by_email(&mut tx, sign_in_request.email.clone()).await?;

    let user = match user {
        Some(user) => user,
        None => return Err(ApiError::unauthorized()),
    };

    let is_valid_password = verify_password(&user.password_hash, &sign_in_request.password)
        .map_err(|_| ApiError::internal_error())?;

    if !is_valid_password {
        return Err(ApiError::unauthorized());
    }

    delete_refresh_tokens_for_user(&mut tx, user.user_id).await?;

    let jwt =
        generate_jwt(user.user_id, &app_config.token).map_err(|_| ApiError::internal_error())?;

    let refresh_token = generate_refresh_token(&app_config.token);

    let expiration =
        Utc::now() + Duration::from_secs(app_config.token.refresh_token_expiration_seconds);

    let refresh_token = create_refresh_token(
        &mut tx,
        CreateRefreshTokenQuery {
            user_id: user.user_id,
            token: refresh_token,
            expiration: expiration.naive_utc(),
        },
    )
    .await?;

    tx.commit().await?;

    Ok(Json(SignInResponse {
        user_id: user.user_id,
        jwt,
        refresh_token: refresh_token.token,
    }))
}
