use axum::{Extension, Json};
use axum::extract::Path;
use sqlx::PgPool;
use crate::db::unit::{delete_unit_for_user, exists_user_unit};
use crate::errors::api::ApiError;
use crate::models::dtos::unit::DeleteResponse;
use crate::models::dtos::user::ClaimsDto;

pub async fn delete(
    Extension(pool): Extension<PgPool>,
    Path(unit_id): Path<i32>,
    claims: ClaimsDto,
) -> Result<Json<DeleteResponse>, ApiError> {
    let mut tx = pool.begin().await.map_err(|_| ApiError::internal_error())?;

    let exists = exists_user_unit(&mut tx, claims.sub, unit_id)
        .await
        .map_err(|_| ApiError::internal_error())?;

    if !exists {
        return Err(ApiError::not_found("Unit not found"));
    }

    delete_unit_for_user(&mut tx, claims.sub, unit_id)
        .await
        .map_err(|_| ApiError::internal_error())?;

    tx.commit().await.map_err(|_| ApiError::internal_error())?;

    Ok(Json(DeleteResponse {}))
}