use crate::db::unit::list_units_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::{PaginatedDto, ValidatedQuery};
use crate::models::dtos::unit::{ListRequest, ListResponse};
use crate::models::dtos::user::ClaimsDto;
use axum::{debug_handler, Extension, Json};
use sqlx::PgPool;

#[debug_handler]
pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let mut conn = pool.acquire().await.map_err(|_| ApiError::internal_error())?;

    let (units, total_count) =
        list_units_for_user(&mut *conn, claims.sub, list_request.page, list_request.per_page)
            .await
            .map_err(|_| ApiError::internal_error())?;

    let units = units.into_iter().map(|u| u.into()).collect();

    Ok(Json(ListResponse {
        units: PaginatedDto::new(units, total_count, list_request.page, list_request.per_page),
    }))
}
