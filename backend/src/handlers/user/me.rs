use axum::{Extension, Json};
use sqlx::PgPool;

use crate::db::user::find_user_by_id;
use crate::errors::api::ApiError;
use crate::models::dtos::user::{ClaimsDto, MeResponse};

pub async fn me(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
) -> Result<Json<MeResponse>, ApiError> {
    let mut conn = pool.acquire().await?;

    let user = find_user_by_id(&mut conn, claims.sub).await?;

    let user = match user {
        Some(user) => user,
        None => return Err(ApiError::unauthorized()),
    };

    Ok(Json(MeResponse { user: user.into() }))
}
