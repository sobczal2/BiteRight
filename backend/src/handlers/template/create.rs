use axum::{Extension, Json};
use axum::http::StatusCode;
use sqlx::PgPool;
use sqlx::postgres::types::{PgInterval, PgMoney};
use crate::db::currency::exists_currency_for_user;
use crate::db::template::{create_template_for_user, exists_template_for_user_by_name};
use crate::db::unit::exists_unit_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::template::{CreateRequest, CreateResponse};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::template::CreateTemplateForUserQuery;
use crate::utils::regex::PRICE_REGEX;

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

    let expiration_span = PgInterval::try_from(create_request.expiration_span)
        .map_err(|_| ApiError::bad_request("Invalid expiration span"))?;

    let price: Option<PgMoney> = match (create_request.price, create_request.currency_id) {
        (Some(price), Some(currency_id)) => {
            let exists = exists_currency_for_user(
                &mut tx,
                claims.sub,
                currency_id,
            )
                .await?;
            if !exists {
                return Err(ApiError::bad_request("Currency does not exist"));
            }

            if !PRICE_REGEX.is_match(&price) {
                return Err(ApiError::bad_request("Invalid price"));
            }

            Some(PgMoney::try_from((price.parse::<f64>().unwrap() * 100.0) as i64)
                .map_err(|_| ApiError::bad_request("Invalid price"))?)
        },
        (None, None) => None,
        _ => return Err(ApiError::bad_request("Price and currency ID must be both present or both absent")),
    };

    let template = create_template_for_user(
        &mut tx,
        CreateTemplateForUserQuery {
            user_id: claims.sub,
            name: create_request.name,
            expiration_span,
            amount: create_request.amount,
            unit_id: create_request.unit_id,
            price,
            currency_id: create_request.currency_id,
            category_id: create_request.category_id,
        }
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