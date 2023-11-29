use axum::{Extension, Json};
use sqlx::PgPool;
use crate::db::currency::list_currencies_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::{PaginatedDto, ValidatedQuery};
use crate::models::dtos::currency::{CurrencyDto, ListRequest, ListResponse};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::currency::ListCurrenciesForUserQuery;

pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let (currencies, count) = list_currencies_for_user(
        &mut tx,
        ListCurrenciesForUserQuery {
            user_id: claims.sub,
            page: list_request.page,
            per_page: list_request.per_page,
        },
    )
        .await?;

    tx.commit().await?;

    let currencies = currencies
        .into_iter()
        .map(|c| CurrencyDto::from(c))
        .collect();
    
    Ok(Json(ListResponse {
        currencies: PaginatedDto::new(
            currencies,
            count,
            list_request.page,
            list_request.per_page,
        ),
    }))
}