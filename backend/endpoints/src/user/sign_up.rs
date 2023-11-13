use std::sync::Arc;
use axum::{Extension, Json};
use sqlx::PgPool;
use config::AppConfig;
use domain::user::User;
use dtos::user::sign_up::{SignUpRequestDto, SignUpResponseDto};
use errors::user::sign_up::SignUpError;
use persistence::refresh_token_repository::RefreshTokenRepository;
use persistence::user_repository::UserRepository;
use services::password_service::PasswordService;
use services::token_service::TokenService;
use crate::common::validate_extractor::ValidatedJson;

pub async fn sign_up(
    Extension(pool): Extension<PgPool>,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(sign_up_request_dto): ValidatedJson<SignUpRequestDto>,
) -> Result<Json<SignUpResponseDto>, SignUpError> {
    let mut tx = pool.begin().await.map_err(|_| SignUpError::Unknown)?;

    let user_repository = UserRepository::new();

    if user_repository.exists_by_email(&mut *tx, sign_up_request_dto.email.clone())
        .await
        .map_err(|_| { SignUpError::Unknown })?
    {
        return Err(SignUpError::EmailTaken);
    }

    if user_repository.exists_by_name(&mut *tx, sign_up_request_dto.name.clone())
        .await
        .map_err(|_| { SignUpError::Unknown })?
    {
        return Err(SignUpError::NameTaken);
    }

    let password_service = PasswordService::new();

    let hashed_password = password_service.hash_password(&sign_up_request_dto.password).map_err(|_| SignUpError::Unknown)?;

    let user = user_repository.create(
        &mut *tx,
        User::new(
            sign_up_request_dto.name.clone(),
            sign_up_request_dto.email.clone(),
            hashed_password,
        ),
    )
        .await
        .map_err(|_| SignUpError::Unknown)?;

    let token_service = TokenService::new(&app_config.token_config);

    let jwt = token_service.generate_jwt(user.user_id).map_err(|_| SignUpError::Unknown)?;

    let refresh_token = token_service.generate_refresh_token(user.user_id);

    let refresh_token_repository = RefreshTokenRepository::new();

    let refresh_token = refresh_token_repository.create(
        &mut *tx,
        refresh_token,
    )
        .await
        .map_err(|_| SignUpError::Unknown)?;

    tx.commit().await.map_err(|_| SignUpError::Unknown)?;

    Ok(
        Json(
            SignUpResponseDto {
                user_id: user.user_id,
                jwt,
                refresh_token: refresh_token.token,
            }
        )
    )
}
