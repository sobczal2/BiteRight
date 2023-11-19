use axum::{debug_handler, Extension, Json};
use sqlx::PgPool;
use crate::db::unit::{create_unit_for_user, exists_unit_for_user_by_abbreviation, exists_unit_for_user_by_name};
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::unit::{CreateRequest, CreateResponse};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::unit::CreateUnitForUserQuery;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
) -> Result<Json<CreateResponse>, ApiError> {
    let mut tx = pool.begin().await.map_err(|_| ApiError::internal_error())?;

    let exists = exists_unit_for_user_by_name(&mut *tx, claims.sub, &create_request.name)
        .await
        .map_err(|_| ApiError::internal_error())?;
    if exists {
        return Err(ApiError::bad_request("Unit already exists"));
    }

    let exists = exists_unit_for_user_by_abbreviation(&mut *tx, claims.sub, &create_request.abbreviation)
        .await
        .map_err(|_| ApiError::internal_error())?;
    if exists {
        return Err(ApiError::bad_request("Unit already exists"));
    }

    let unit = create_unit_for_user(
        &mut *tx,
        CreateUnitForUserQuery {
            user_id: claims.sub,
            name: create_request.name,
            abbreviation: create_request.abbreviation,
        },
    )
        .await
        .map_err(|_| ApiError::internal_error())?;

    tx.commit().await.map_err(|_| ApiError::internal_error())?;

    Ok(Json(CreateResponse {
        unit: unit.into(),
    }))
}