use axum::{Extension, Json};
use sqlx::PgPool;

use crate::db::user;
use crate::errors::api::ApiError;
use crate::models::dtos::user::{ClaimsDto, MeResponse};

pub async fn me(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
) -> Result<Json<MeResponse>, ApiError> {
    let user = user::find_by_id(&pool, claims.sub)
        .await
        .map_err(|_| ApiError::internal_error())?;

    let user = match user {
        Some(user) => user,
        None => return Err(ApiError::unauthorized()),
    };

    Ok(Json(MeResponse {
        user: user.into(),
    }))
}
