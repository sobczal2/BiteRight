use axum::{Extension, Json};
use sqlx::PgPool;
use crate::errors::api::ApiError;
use crate::models::dtos::common::ValidatedJson;
use crate::models::dtos::unit::{CreateRequest, CreateResponse};
use crate::models::dtos::user::ClaimsDto;

pub async fn create(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedJson(create_request): ValidatedJson<CreateRequest>,
) -> Result<Json<CreateResponse>, ApiError> {
    unimplemented!()
}