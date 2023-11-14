use std::sync::Arc;
use axum::{Extension, Json};
use sqlx::PgPool;
use config::AppConfig;
use dtos::user::sign_in::{SignInRequestDto, SignInResponseDto};
use errors::user::sign_in::SignInError;
use persistence::refresh_token_repository::RefreshTokenRepository;
use persistence::user_repository::UserRepository;
use services::password_service::PasswordService;
use services::token_service::TokenService;
use crate::common::validate_extractor::ValidatedJson;

pub async fn sign_in(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(sign_in_request_dto): ValidatedJson<SignInRequestDto>,
) -> Result<Json<SignInResponseDto>, SignInError> {
    let mut tx = pool.begin().await.map_err(|_| SignInError::Unknown)?;

    let user_repository = UserRepository::new();

    let user = user_repository.find_by_email(&mut *tx, sign_in_request_dto.email.clone())
        .await
        .map_err(|_| SignInError::Unknown)?;

    let user = match user {
        Some(user) => user,
        None => return Err(SignInError::InvalidCredentials),
    };

    let password_service = PasswordService::new();

    if !password_service.verify_password(&sign_in_request_dto.password, &user.password_hash)
        .map_err(|_| SignInError::Unknown)?
    {
        return Err(SignInError::InvalidCredentials);
    }

    let token_service = TokenService::new(&app_config.token_config);

    let jwt = token_service.generate_jwt(user.user_id).map_err(|_| SignInError::Unknown)?;

    let refresh_token = token_service.generate_refresh_token(user.user_id);

    let refresh_token_repository = RefreshTokenRepository::new();

    refresh_token_repository.delete_for_user(&mut *tx, user.user_id)
        .await
        .map_err(|_| SignInError::Unknown)?;

    let refresh_token = refresh_token_repository.create(
        &mut *tx,
        refresh_token
    )
        .await
        .map_err(|_| SignInError::Unknown)?;

    tx.commit().await.map_err(|_| SignInError::Unknown)?;

    Ok(Json(SignInResponseDto {
        user_id: user.user_id,
        jwt,
        refresh_token: refresh_token.token,
    }))
}