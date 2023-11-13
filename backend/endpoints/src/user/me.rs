use axum::{Extension, Json};
use sqlx::PgPool;
use dtos::user::me::MeResponseDto;
use errors::user::me::MeError;
use persistence::user_repository::UserRepository;
use services::token_service::Claims;

pub async fn me(
    Extension(pool): Extension<PgPool>,
    claims: Claims,
) -> Result<Json<MeResponseDto>, MeError> {
    let mut tx = pool.begin().await.map_err(|_| MeError::Unknown)?;

    let user_repository = UserRepository::new();

    let user = user_repository.find_by_id(&mut *tx, claims.sub)
        .await
        .map_err(|_| MeError::Unknown)?;

    let user = match user {
        Some(user) => user,
        None => return Err(MeError::Unknown),
    };

    tx.commit().await.map_err(|_| MeError::Unknown)?;

    Ok(Json(MeResponseDto {
        user_id: user.user_id,
        name: user.name,
        email: user.email,
    }))
}
