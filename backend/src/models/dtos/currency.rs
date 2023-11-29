use serde::{Deserialize, Serialize};
use validator::Validate;
use crate::models::dtos::common::PaginatedDto;
use crate::models::query_objects::currency::FetchCurrencyQueryResult;

#[derive(Debug, Serialize, Deserialize)]
pub struct CurrencyDto {
    pub currency_id: i32,
    pub name: String,
    pub code: String,
    pub symbol: String,
    pub can_modify: bool,
}

impl From<FetchCurrencyQueryResult> for CurrencyDto {
    fn from(currency: FetchCurrencyQueryResult) -> Self {
        Self {
            currency_id: currency.currency_id,
            name: currency.name,
            code: currency.code,
            symbol: currency.symbol,
            can_modify: currency.can_modify,
        }
    }
}

#[derive(Debug, Deserialize, Validate)]
pub struct ListRequest {
    #[validate(range(min = 0, message = "Page must be greater than or equal to 0"))]
    pub page: i32,
    #[validate(range(min = 1, max = 1000, message = "Per page must be between 1 and 1000"))]
    pub per_page: i32,
}

#[derive(Debug, Serialize)]
pub struct ListResponse {
    pub currencies: PaginatedDto<CurrencyDto>,
}