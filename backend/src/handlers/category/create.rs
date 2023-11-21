use std::sync::Arc;
use axum::{Extension, Json};
use sqlx::PgPool;
use crate::config::AppConfig;
use crate::db::category::{create_category_for_user, exists_category_for_user_by_name};
use crate::errors::api::ApiError;
use crate::models::dtos::category::{CategoryDto, CreateRequest, CreateResponse};
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::category::CreateCategoryForUserQuery;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
) -> Result<Json<CreateResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let exists = exists_category_for_user_by_name(&mut tx, claims.sub, &create_request.name)
        .await?;

    if exists {
        return Err(ApiError::bad_request("Category already exists"));
    }

    let category = create_category_for_user(
        &mut tx,
        CreateCategoryForUserQuery {
            user_id: claims.sub,
            name: create_request.name.to_lowercase(),
            photo_id: None,
        },
    )
        .await?;

    tx.commit().await?;

    Ok(Json(CreateResponse {
        category: CategoryDto::from_query_result(category, &app_config.assets),
    }))
}