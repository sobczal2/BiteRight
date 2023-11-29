use crate::db::category::{delete_category_for_user, exists_user_category};
use crate::errors::api::ApiError;
use crate::models::dtos::user::ClaimsDto;
use axum::extract::Path;
use axum::Extension;
use sqlx::PgPool;

pub async fn delete(
    Extension(pool): Extension<PgPool>,
    Path(category_id): Path<i32>,
    claims: ClaimsDto,
) -> Result<(), ApiError> {
    let mut tx = pool.begin().await?;

    let exists = exists_user_category(&mut tx, claims.sub, category_id).await?;

    if !exists {
        return Err(ApiError::not_found("Category not found"));
    }

    // TODO: delete photo

    delete_category_for_user(&mut tx, claims.sub, category_id).await?;

    tx.commit().await?;

    Ok(())
}
