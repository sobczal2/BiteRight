use std::time::Duration;
use serde::Deserialize;
use validator::{Validate, ValidationError};
use crate::utils::regex::PRICE_REGEX;

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
    pub price: String,
    #[validate(range(min = 1, message = "Currency ID must be greater than or equal to 1"))]
    pub currency_id: i32,
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
