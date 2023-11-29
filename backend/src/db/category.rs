use crate::models::query_objects::category::{
    CreateCategoryForUserQuery, FetchCategoryQueryResult, ListCategoriesForUserQuery,
};
use sqlx::{query, query_as_unchecked, PgConnection};
use crate::utils::db::get_skip_and_take;

pub async fn list_categories_for_user(
    conn: &mut PgConnection,
    list_categories_for_user_query: ListCategoriesForUserQuery,
) -> Result<(Vec<FetchCategoryQueryResult>, i32), sqlx::Error> {
    let (skip, take) = get_skip_and_take(list_categories_for_user_query.page, list_categories_for_user_query.per_page);
    let categories = query_as_unchecked!(
        FetchCategoryQueryResult,
        r#"
SELECT c.category_id,
       c.name,
       p.name as photo_name,
       CASE
           WHEN uc.user_id IS NOT NULL THEN TRUE
           ELSE FALSE
           END as can_modify,
       c.created_at,
       c.updated_at
FROM categories c
         LEFT JOIN user_categories uc ON c.category_id = uc.category_id
         LEFT JOIN system_categories sc ON c.category_id = sc.category_id
         LEFT JOIN photos p ON c.photo_id = p.photo_id
WHERE uc.user_id = $1
   OR sc.system_category_id IS NOT NULL
ORDER BY name
OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY
            "#,
        list_categories_for_user_query.user_id,
        skip,
        take,
    )
    .fetch_all(&mut *conn)
    .await?;

    let count = query!(
        r#"
SELECT COUNT(*)::INT as total_count
FROM categories c
         LEFT JOIN user_categories uc ON c.category_id = uc.category_id
         LEFT JOIN system_categories sc ON c.category_id = sc.category_id
         LEFT JOIN photos p ON c.photo_id = p.photo_id
WHERE uc.user_id = $1
   OR sc.system_category_id IS NOT NULL
        "#,
        list_categories_for_user_query.user_id,
    )
    .fetch_one(&mut *conn)
    .await?
    .total_count
    .unwrap_or(0);

    Ok((categories, count))
}

pub async fn create_category_for_user(
    conn: &mut PgConnection,
    create_category_for_user_query: CreateCategoryForUserQuery,
) -> Result<FetchCategoryQueryResult, sqlx::Error> {
    let result = query!(
        r#"
INSERT INTO categories (name, photo_id)
VALUES ($1, $2)
RETURNING category_id, name, created_at, updated_at
        "#,
        create_category_for_user_query.name,
        create_category_for_user_query.photo_id,
    )
    .fetch_one(&mut *conn)
    .await?;

    query!(
        r#"
INSERT INTO user_categories (user_id, category_id)
VALUES ($1, $2)
        "#,
        create_category_for_user_query.user_id,
        result.category_id,
    )
    .execute(&mut *conn)
    .await?;

    Ok(FetchCategoryQueryResult {
        category_id: result.category_id,
        name: result.name,
        photo_name: None,
        created_at: result.created_at,
        updated_at: result.updated_at,
        can_modify: true,
    })
}

pub async fn exists_category_for_user_by_name(
    conn: &mut PgConnection,
    user_id: i32,
    name: &str,
) -> Result<bool, sqlx::Error> {
    let result = query!(
        r#"
SELECT EXISTS(
    SELECT 1
    FROM categories c
        LEFT JOIN user_categories uc ON c.category_id = uc.category_id
        LEFT JOIN system_categories sc ON c.category_id = sc.category_id
    WHERE (uc.user_id = $1 OR sc.system_category_id IS NOT NULL)
        AND c.name = $2
)
        "#,
        user_id,
        name,
    )
    .fetch_one(&mut *conn)
    .await?;

    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn exists_user_category(
    conn: &mut PgConnection,
    user_id: i32,
    category_id: i32,
) -> Result<bool, sqlx::Error> {
    let result = query!(
        r#"
SELECT EXISTS(SELECT 1
              FROM user_categories
              WHERE user_id = $1
                AND category_id = $2)
        "#,
        user_id,
        category_id,
    )
    .fetch_one(&mut *conn)
    .await?;

    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn delete_category_for_user(
    conn: &mut PgConnection,
    user_id: i32,
    category_id: i32,
) -> Result<(), sqlx::Error> {
    query!(
        r#"
DELETE
FROM user_categories
WHERE user_id = $1
  AND category_id = $2
        "#,
        user_id,
        category_id,
    )
    .execute(&mut *conn)
    .await?;

    Ok(())
}
