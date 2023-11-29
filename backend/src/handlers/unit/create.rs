use crate::db::unit::{
    create_unit_for_user, exists_unit_for_user_by_abbreviation, exists_unit_for_user_by_name,
};
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::unit::{CreateRequest, CreateResponse};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::unit::CreateUnitForUserQuery;
use axum::{Extension, Json};
use axum::http::StatusCode;
use sqlx::PgPool;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
) -> Result<(StatusCode, Json<CreateResponse>), ApiError> {
    let mut tx = pool.begin().await?;

    let exists =
        exists_unit_for_user_by_name(&mut tx, claims.sub, &create_request.name.to_lowercase())
            .await?;
    if exists {
        return Err(ApiError::bad_request("Unit already exists"));
    }

    let exists =
        exists_unit_for_user_by_abbreviation(&mut tx, claims.sub, &create_request.abbreviation)
            .await?;
    if exists {
        return Err(ApiError::bad_request("Unit already exists"));
    }

    let unit = create_unit_for_user(
        &mut tx,
        CreateUnitForUserQuery {
            user_id: claims.sub,
            name: create_request.name.to_lowercase(),
            abbreviation: create_request.abbreviation,
        },
    )
        .await?;

    tx.commit().await?;

    Ok(
        (
            StatusCode::CREATED,
            Json(
                CreateResponse {
                    unit: unit.into()
                }
            )
        )
    )
}
