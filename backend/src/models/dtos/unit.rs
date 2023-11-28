use crate::models::dtos::common::PaginatedDto;
use crate::models::query_objects::unit::FetchUnitQueryResult;
use serde::{Deserialize, Serialize};
use validator::Validate;

#[derive(Debug, Serialize, Deserialize)]
pub struct UnitDto {
    pub unit_id: i32,
    pub name: String,
    pub abbreviation: String,
    pub can_modify: bool,
}

impl From<FetchUnitQueryResult> for UnitDto {
    fn from(unit: FetchUnitQueryResult) -> Self {
        Self {
            unit_id: unit.unit_id,
            name: unit.name,
            abbreviation: unit.abbreviation,
            can_modify: unit.can_modify,
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
    pub units: PaginatedDto<UnitDto>,
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateRequest {
    #[validate(length(min = 1, max = 64))]
    pub name: String,
    #[validate(length(min = 1, max = 16))]
    pub abbreviation: String,
}

#[derive(Debug, Serialize)]
pub struct CreateResponse {
    pub unit: UnitDto,
}
