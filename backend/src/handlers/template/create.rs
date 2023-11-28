use axum::{Extension, Json};
use axum::http::StatusCode;
use sqlx::PgPool;
use crate::db::template::{create_template_for_user, exists_template_for_user_by_name};
use crate::db::unit::exists_unit_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::template::{CreateRequest, CreateResponse};
use crate::models::dtos::user::ClaimsDto;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
) -> Result<(StatusCode, Json<CreateResponse>), ApiError>
{
    let mut tx = pool.begin().await?;

    let exists =
        exists_template_for_user_by_name(&mut tx, claims.sub, &create_request.name)
            .await?;
    if exists {
        return Err(ApiError::bad_request("Template already exists"));
    }
    
    let unit_exists = exists_unit_for_user(
        &mut tx,
        claims.sub,
        create_request.unit_id,
    )
        .await?;
    if !unit_exists {
        return Err(ApiError::bad_request("Unit does not exist"));
    }
    
    // TODO
    // match (create_request.price, create_request.currency_id) { 
    //     (Some(price), Some(currency_id)) => {
    //         let price = price.parse::<f64>().map_err(|_| ApiError::bad_request("Invalid price"))?;
    //         let currency_exists = exists_currency_for_user(
    //             &mut tx,
    //             claims.sub,
    //             currency_id,
    //         )
    //             .await?;
    //         if !currency_exists {
    //             return Err(ApiError::bad_request("Currency does not exist"));
    //         }
    //     },
    //     (Some(_), None) => {
    //         return Err(ApiError::bad_request("Currency ID must be specified if price is specified"));
    //     },
    //     (None, Some(_)) => {
    //         return Err(ApiError::bad_request("Price must be specified if currency ID is specified"));
    //     },
    //     (None, None) => {},
    // }

    let template = create_template_for_user(
        &mut tx,
        create_request.into_create_query(claims.sub).map_err(|_| ApiError::internal_error())?,
    )
        .await?;

    let template = template
        .try_into()
        .map_err(|_| ApiError::internal_error())?;

    tx.commit().await?;

    Ok(
        (
            StatusCode::CREATED,
            Json(
                CreateResponse {
                    template
                }
            )
        )
    )
}