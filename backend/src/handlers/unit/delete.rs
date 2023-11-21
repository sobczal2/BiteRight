use crate::db::unit::{delete_unit_for_user, exists_user_unit};
use crate::errors::api::ApiError;
use crate::models::dtos::common::EmptyResponse;
use crate::models::dtos::user::ClaimsDto;
use axum::extract::Path;
use axum::{Extension, Json};
use sqlx::PgPool;

pub async fn delete(
    Extension(pool): Extension<PgPool>,
    Path(unit_id): Path<i32>,
    claims: ClaimsDto,
) -> Result<Json<EmptyResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let exists = exists_user_unit(&mut tx, claims.sub, unit_id).await?;

    if !exists {
        return Err(ApiError::not_found("Unit not found"));
    }

    delete_unit_for_user(&mut tx, claims.sub, unit_id).await?;

    tx.commit().await?;

    Ok(Json(EmptyResponse {}))
}
