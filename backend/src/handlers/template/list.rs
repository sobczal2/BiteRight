use axum::{Extension, Json};
use sqlx::PgPool;
use crate::db::template::list_templates_for_user;
use crate::errors::api::ApiError;
use crate::models::dtos::common::{PaginatedDto, ValidatedQuery};
use crate::models::dtos::template::{ListRequest, ListResponse, TemplateDto};
use crate::models::dtos::user::ClaimsDto;
use crate::models::query_objects::template::ListTemplatesForUserQuery;

pub async fn list(
    Extension(pool): Extension<PgPool>,
    claims: ClaimsDto,
    ValidatedQuery(list_request): ValidatedQuery<ListRequest>,
) -> Result<Json<ListResponse>, ApiError> {
    let mut tx = pool.begin().await?;

    let (templates, count) = list_templates_for_user(
        &mut tx,
        ListTemplatesForUserQuery {
            user_id: claims.sub,
            page: list_request.page,
            per_page: list_request.per_page,
        },
    )
    .await?;

    let templates = templates
        .into_iter()
        .map(|t| TemplateDto::try_from(t))
        .collect::<Result<Vec<TemplateDto>, ()>>()
        .map_err(|_| ApiError::internal_error())?;

    tx.commit().await?;

    Ok(Json(ListResponse {
        templates: PaginatedDto::new(
            templates,
            count,
            list_request.page,
            list_request.per_page,
        ),
    }))
}
