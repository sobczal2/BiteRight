use serde::{Deserialize, Serialize};
use validator::{Validate};
use crate::models::dtos::common::PaginatedDto;
use crate::models::query_objects::template::{FetchTemplateQueryResult};

#[derive(Debug, Serialize, Deserialize)]
pub struct TemplateDto {
    pub template_id: i32,
    pub name: String,
    pub expiration_span_seconds: i64,
    pub amount: f64,
    pub unit_id: i32,
    pub price_cents: Option<i64>,
    pub currency_id: Option<i32>,
    pub category_id: i32,
    pub can_modify: bool,
}

impl From<FetchTemplateQueryResult> for TemplateDto {
    fn from(fetch_template_query_result: FetchTemplateQueryResult) -> Self {
        TemplateDto {
            template_id: fetch_template_query_result.template_id,
            name: fetch_template_query_result.name,
            expiration_span_seconds: fetch_template_query_result.expiration_span.microseconds / 1_000_000,
            amount: fetch_template_query_result.amount,
            unit_id: fetch_template_query_result.unit_id,
            price_cents: fetch_template_query_result.price.map(|p| p.0),
            currency_id: fetch_template_query_result.currency_id,
            category_id: fetch_template_query_result.category_id,
            can_modify: fetch_template_query_result.can_modify,
        }
    }
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateRequest {
    #[validate(length(min = 1, max = 64, message = "Name must be between 1 and 64 characters"))]
    pub name: String,
    #[validate(range(min = 0, message = "Expiration span must be greater than or equal to 0"))]
    pub expiration_span_seconds: i64,
    #[validate(range(min = 0.0, message = "Amount must be greater than or equal to 0"))]
    pub amount: f64,
    #[validate(range(min = 1, message = "Unit ID must be greater than or equal to 1"))]
    pub unit_id: i32,
    #[validate(range(min = 0.0, message = "Price must be greater than or equal to 0"))]
    pub price_cents: Option<i64>,
    #[validate(range(min = 1, message = "Currency ID must be greater than or equal to 1"))]
    pub currency_id: Option<i32>,
    #[validate(range(min = 1, message = "Category ID must be greater than or equal to 1"))]
    pub category_id: i32,
}

#[derive(Debug, Serialize)]
pub struct CreateResponse {
    pub template: TemplateDto,
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
    pub templates: PaginatedDto<TemplateDto>,
}