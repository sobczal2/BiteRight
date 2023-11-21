use crate::db::unit::list_units_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::{PaginatedDto, ValidatedQuery};
use crate::models::dtos::unit::{ListRequest, ListResponse};
use crate::models::dtos::user::ClaimsDto;
use axum::{Extension, Json};
use sqlx::PgPool;
use crate::models::query_objects::unit::ListUnitsForUserQuery;


pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let (units, total_count) =
        list_units_for_user(&mut tx,
                            ListUnitsForUserQuery {
                                user_id: claims.sub,
                                page: list_request.page,
                                per_page: list_request.per_page,
                            })
            .await?;

    let units = units.into_iter().map(|u| u.into()).collect();

    tx.commit().await?;

    Ok(Json(ListResponse {
        units: PaginatedDto::new(units, total_count, list_request.page, list_request.per_page),
    }))
}
