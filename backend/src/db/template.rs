use sqlx::{PgConnection, query, query_as, query_unchecked};
use sqlx::postgres::types::{PgInterval, PgMoney};
use crate::models::query_objects::template::{CreateTemplateForUserQuery, FetchTemplateQueryResult};

pub async fn create_template_for_user(
    conn: &mut PgConnection,
    create_template_for_user_query: CreateTemplateForUserQuery,
) -> Result<FetchTemplateQueryResult, sqlx::Error> {

    let result = query!(
        r#"
INSERT INTO templates (name, expiration_span, amount, unit_id, price, currency_id, category_id)
VALUES ($1, $2, $3, $4, $5, $6, $7)
RETURNING template_id, name, expiration_span, amount, unit_id, price, currency_id, category_id, created_at, updated_at
        "#,
        create_template_for_user_query.name,
        create_template_for_user_query.expiration_span,
        create_template_for_user_query.amount,
        create_template_for_user_query.unit_id,
        create_template_for_user_query.price,
        create_template_for_user_query.currency_id,
        create_template_for_user_query.category_id,
    )
    .fetch_one(&mut *conn)
    .await?;

    query!(
        r#"
INSERT INTO user_templates (user_id, template_id)
VALUES ($1, $2)
        "#,
        create_template_for_user_query.user_id,
        result.template_id,
    )
    .execute(&mut *conn)
    .await?;

    Ok(FetchTemplateQueryResult {
        template_id: result.template_id,
        name: result.name,
        expiration_span: result.expiration_span,
        amount: result.amount,
        unit_id: result.unit_id,
        price: result.price,
        currency_id: result.currency_id,
        category_id: result.category_id,
        can_modify: true,
        created_at: result.created_at,
        updated_at: result.updated_at,
    })
}