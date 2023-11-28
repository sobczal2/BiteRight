use std::time::Duration;
use serde::{Deserialize, Serialize};
use sqlx::postgres::types::{PgInterval, PgMoney};
use validator::{Validate, ValidationError};
use crate::models::dtos::common::PaginatedDto;
use crate::models::query_objects::template::{CreateTemplateForUserQuery, FetchTemplateQueryResult};
use crate::utils::regex::PRICE_REGEX;

#[derive(Debug, Serialize, Deserialize)]
pub struct TemplateDto {
    pub template_id: i32,
    pub name: String,
    pub expiration_span: Duration,
    pub amount: f64,
    pub unit_id: i32,
    pub price: Option<String>,
    pub currency_id: Option<i32>,
    pub category_id: i32,
    pub can_modify: bool,
}

impl TryFrom<FetchTemplateQueryResult> for TemplateDto {
    type Error = ();

    fn try_from(template: FetchTemplateQueryResult) -> Result<Self, Self::Error> {
        Ok(Self {
            template_id: template.template_id,
            name: template.name,
            expiration_span: Duration::from_micros(template.expiration_span.microseconds as u64),
            amount: template.amount,
            unit_id: template.unit_id,
            price: template.price.map(|price| price.0.to_string()),
            currency_id: template.currency_id,
            category_id: template.category_id,
            can_modify: template.can_modify,
        })
    }
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateRequest {
    #[validate(length(min = 1, max = 64, message = "Name must be between 1 and 64 characters"))]
    pub name: String,
    #[validate(custom = "validate_expiration_span")]
    pub expiration_span: Duration,
    #[validate(range(min = 0.0, message = "Amount must be greater than or equal to 0"))]
    pub amount: f64,
    #[validate(range(min = 1, message = "Unit ID must be greater than or equal to 1"))]
    pub unit_id: i32,
    #[validate(custom = "validate_price")]
    pub price: Option<String>,
    #[validate(range(min = 1, message = "Currency ID must be greater than or equal to 1"))]
    pub currency_id: Option<i32>,
    #[validate(range(min = 1, message = "Category ID must be greater than or equal to 1"))]
    pub category_id: i32,
}

fn validate_expiration_span(expiration_span: &Duration) -> Result<(), ValidationError> {
    if expiration_span.as_secs() < 1 {
        return Err(ValidationError::new("Expiration span must be at least 1 second"));
    }

    if expiration_span.as_secs() > 60 * 60 * 24 * 365 * 100 {
        return Err(ValidationError::new("Expiration span must be less than 100 years"));
    }

    Ok(())
}

fn validate_price(price: &String) -> Result<(), ValidationError> {
    if price.len() > 64 {
        return Err(ValidationError::new("Price must be less than 64 characters"));
    }

    if PRICE_REGEX.is_match(price) {
        return Ok(());
    }

    Err(ValidationError::new("Price must be a valid price"))
}

impl CreateRequest {
    pub fn into_create_query(self, user_id: i32) -> Result<CreateTemplateForUserQuery, ()> {
        let expiration_span = PgInterval {
            months: 0,
            days: 0,
            microseconds: self.expiration_span.as_micros() as i64,
        };

        let price = match self.price {
            Some(price) => Some(
                PgMoney(
                    (price.parse::<f64>().map_err(|_| ())? * 100.0) as i64,
                ),
            ),
            None => None,
        };

        Ok(CreateTemplateForUserQuery {
            name: self.name.to_lowercase(),
            expiration_span,
            amount: self.amount,
            unit_id: self.unit_id,
            price,
            currency_id: self.currency_id,
            category_id: self.category_id,
            user_id,
        })
    }
}

#[derive(Debug, Serialize)]
pub struct CreateResponse {
    pub template: TemplateDto,
}

#[derive(Debug, Deserialize, Validate)]
pub struct ListRequest {
    #[validate(range(min = 0, max = 1000, message = "Page must be between 0 and 1000"))]
    pub page: i32,
    #[validate(range(min = 1, message = "Per page must be greater than or equal to 1"))]
    pub per_page: i32,
}

#[derive(Debug, Serialize)]
pub struct ListResponse {
    pub templates: PaginatedDto<TemplateDto>,
}