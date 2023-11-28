use sqlx::{PgConnection, query, query_as, query_as_unchecked};
use crate::models::query_objects::template::{CreateTemplateForUserQuery, FetchTemplateQueryResult, ListTemplatesForUserQuery};
use crate::utils::db::get_skip_and_take;

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
        created_at: result.created_at,
        updated_at: result.updated_at,
        can_modify: true,
    })
}

pub async fn exists_template_for_user_by_name(
    conn: &mut PgConnection,
    user_id: i32,
    name: &String,
) -> Result<bool, sqlx::Error> {
    let result = query!(
r#"
SELECT EXISTS (SELECT 1
               FROM templates t
                        LEFT JOIN user_templates ut ON t.template_id = ut.template_id
                        LEFT JOIN system_templates st ON t.template_id = st.template_id
               WHERE (ut.user_id = $1
                   OR st.system_template_id IS NOT NULL)
                 AND t.name = $2
                   FETCH FIRST ROW ONLY)
        "#,
        user_id,
        name,
    )
        .fetch_one(&mut *conn)
        .await?;

    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn list_templates_for_user(
    conn: &mut PgConnection,
    list_templates_for_user_query: ListTemplatesForUserQuery,
) -> Result<(Vec<FetchTemplateQueryResult>, i32), sqlx::Error> {
    let (skip, take) = get_skip_and_take(list_templates_for_user_query.page, list_templates_for_user_query.per_page);
    let templates = query_as_unchecked!(
        FetchTemplateQueryResult,
        r#"
SELECT t.template_id,
       t.name,
       t.expiration_span,
       t.amount,
       t.unit_id,
       t.price,
       t.currency_id,
       t.category_id,
       t.created_at,
       t.updated_at,
       CASE
           WHEN ut.user_id IS NOT NULL THEN true
           ELSE false
           END AS can_modify
FROM templates t
         LEFT JOIN user_templates ut ON t.template_id = ut.template_id
         LEFT JOIN system_templates st ON t.template_id = st.template_id
WHERE ut.user_id = $1
   OR st.system_template_id IS NOT NULL
ORDER BY t.name
OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY
        "#,
        list_templates_for_user_query.user_id,
        skip,
        take,
    )
        .fetch_all(&mut *conn)
        .await?;

    let count = query!(
        r#"
SELECT COUNT(*)::INT as count
FROM templates t
         LEFT JOIN user_templates ut ON t.template_id = ut.template_id
         LEFT JOIN system_templates st ON t.template_id = st.template_id
WHERE ut.user_id = $1
    OR st.system_template_id IS NOT NULL
        "#,
        list_templates_for_user_query.user_id,
    )
        .fetch_one(&mut *conn)
        .await?
        .count
        .unwrap_or(0);

    Ok((templates, count))
}
