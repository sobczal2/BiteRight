use axum::{Extension, Json};
use axum::extract::Path;
use sqlx::PgPool;
use crate::db::category::{delete_category_for_user, exists_user_category};
use crate::errors::api::ApiError;
use crate::models::dtos::common::EmptyResponse;
use crate::models::dtos::user::ClaimsDto;

pub async fn delete(
    Extension(pool): Extension<PgPool>,
    Path(category_id): Path<i32>,
    claims: ClaimsDto,
) -> Result<Json<EmptyResponse>, ApiError> {
    let mut tx = pool.begin().await.map_err(|_| ApiError::internal_error())?;

    let exists = exists_user_category(&mut tx, claims.sub, category_id)
        .await
        .map_err(|_| ApiError::internal_error())?;

    if !exists {
        return Err(ApiError::not_found("Category not found"));
    }

    // TODO: delete photo

    delete_category_for_user(&mut tx, claims.sub, category_id)
        .await
        .map_err(|_| ApiError::internal_error())?;

    tx.commit().await.map_err(|_| ApiError::internal_error())?;

    Ok(Json(EmptyResponse {}))
}