use crate::config::AppConfig;
use crate::db::category::list_categories_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::category::{CategoryDto, ListRequest, ListResponse};
use crate::models::dtos::common::{PaginatedDto, ValidatedQuery};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::category::ListCategoriesForUserQuery;
use axum::{Extension, Json};
use sqlx::PgPool;
use std::sync::Arc;

pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    Extension(app_config): Extension<Arc<AppConfig>>,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let (categories, total_count) = list_categories_for_user(
        &mut tx,
        ListCategoriesForUserQuery {
            user_id: claims.sub,
            page: list_request.page,
            per_page: list_request.per_page,
        },
    )
    .await?;

    let categories = categories
        .into_iter()
        .map(|c| CategoryDto::from_query_result(c, &app_config.assets))
        .collect();

    tx.commit().await?;

    Ok(Json(ListResponse {
        categories: PaginatedDto::new(
            categories,
            total_count,
            list_request.page,
            list_request.per_page,
        ),
    }))
}
