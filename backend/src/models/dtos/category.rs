use crate::config::AssetsConfig;
use crate::models::dtos::common::PaginatedDto;
use crate::models::query_objects::category::FetchCategoryQueryResult;
use serde::{Deserialize, Serialize};
use validator::Validate;

#[derive(Debug, Serialize, Deserialize)]
pub struct CategoryDto {
    pub category_id: i32,
    pub name: String,
    pub photo_url: String,
    pub can_modify: bool,
}

impl CategoryDto {
    pub fn from_query_result(
        fetch_category_query_result: FetchCategoryQueryResult,
        assets_config: &AssetsConfig,
    ) -> Self {
        Self {
            category_id: fetch_category_query_result.category_id,
            name: fetch_category_query_result.name,
            photo_url: assets_config.get_photo_url(fetch_category_query_result.photo_name),
            can_modify: fetch_category_query_result.can_modify,
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
    pub categories: PaginatedDto<CategoryDto>,
}

#[derive(Debug, Deserialize, Validate)]
pub struct CreateRequest {
    #[validate(length(min = 1, max = 64))]
    pub name: String,
}

#[derive(Debug, Serialize)]
pub struct CreateResponse {
    pub category: CategoryDto,
}