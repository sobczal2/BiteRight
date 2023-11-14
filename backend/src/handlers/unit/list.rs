use crate::db::unit::list_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::{PaginatedVecDto, ValidatedQuery};
use crate::models::dtos::unit::{ListRequest, ListResponse};
use crate::models::dtos::user::ClaimsDto;
use axum::{Extension, Json};
use sqlx::PgPool;

pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let (units, total_count) =
        list_for_user(&pool, claims.sub, list_request.page, list_request.per_page)
            .await
            .map_err(|_| ApiError::internal_error())?;

    let units = units.into_iter().map(|u| u.into()).collect();

    Ok(Json(ListResponse {
        units: PaginatedVecDto::new(units, total_count, list_request.page, list_request.per_page),
    }))
}
