use sqlx::{query, PgConnection, query_as_unchecked};
use crate::models::query_objects::unit::{CreateUnitForUserQuery, FetchUnitQueryResult, ListUnitsForUserQuery};

pub async fn list_units_for_user(
    conn: &mut PgConnection,
    list_units_for_user_query: ListUnitsForUserQuery,
) -> Result<(Vec<FetchUnitQueryResult>, i32), sqlx::Error>
{
    let units = query_as_unchecked!(
        FetchUnitQueryResult,
        r#"
SELECT u.unit_id,
       u.name,
       u.abbreviation,
       CASE
           WHEN uu.user_id IS NOT NULL THEN TRUE
           ELSE FALSE
           END AS can_modify,
       u.created_at,
       u.updated_at
FROM units u
         LEFT JOIN user_units uu ON u.unit_id = uu.unit_id
         LEFT JOIN system_units su ON u.unit_id = su.unit_id
WHERE uu.user_id = $1
   OR su.system_unit_id IS NOT NULL
ORDER BY unit_id
OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY
        "#,
        list_units_for_user_query.user_id,
        (list_units_for_user_query.page * list_units_for_user_query.per_page) as i32,
        list_units_for_user_query.per_page as i32,
    )
        .fetch_all(&mut *conn)
        .await?;

    let count = query!(
        r#"
SELECT COUNT(*)::INT as total_count
FROM units u
         LEFT JOIN user_units uu ON u.unit_id = uu.unit_id
         LEFT JOIN system_units su ON u.unit_id = su.unit_id
WHERE uu.user_id = $1
   OR su.system_unit_id IS NOT NULL
        "#,
        list_units_for_user_query.user_id,
    )
        .fetch_one(&mut *conn)
        .await?
        .total_count
        .unwrap_or(0);

    Ok((units, count))
}

pub async fn create_unit_for_user(
    conn: &mut PgConnection,
    create_unit_for_user_query: CreateUnitForUserQuery,
) -> Result<FetchUnitQueryResult, sqlx::Error>
{
    let result = query!(
        r#"
INSERT INTO units (name, abbreviation)
VALUES ($1, $2)
RETURNING unit_id, name, abbreviation, created_at, updated_at
        "#,
        create_unit_for_user_query.name,
        create_unit_for_user_query.abbreviation,
    )
        .fetch_one(&mut *conn)
        .await?;

    query!(
        r#"
INSERT INTO user_units (user_id, unit_id)
VALUES ($1, $2)
        "#,
        create_unit_for_user_query.user_id,
        result.unit_id,
    )
        .execute(&mut *conn)
        .await?;

    Ok(FetchUnitQueryResult {
        unit_id: result.unit_id,
        name: result.name,
        abbreviation: result.abbreviation,
        can_modify: true,
        created_at: result.created_at,
        updated_at: result.updated_at,
    })
}

pub async fn exists_unit_for_user_by_name(
    conn: &mut PgConnection,
    user_id: i32,
    name: &String,
) -> Result<bool, sqlx::Error>
{
    let result = query!(
        r#"
SELECT EXISTS (
           SELECT 1
           FROM units u
                    LEFT JOIN user_units uu ON u.unit_id = uu.unit_id
                    LEFT JOIN system_units su ON u.unit_id = su.unit_id
           WHERE (uu.user_id = $1
              OR su.system_unit_id IS NOT NULL)
               AND u.name = $2
               FETCH FIRST ROW ONLY
)
        "#,
        user_id,
        name,
    )
        .fetch_one(&mut *conn)
        .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn exists_unit_for_user_by_abbreviation(
    conn: &mut PgConnection,
    user_id: i32,
    abbreviation: &String,
) -> Result<bool, sqlx::Error>
{
    let result = query!(
        r#"
SELECT EXISTS (SELECT 1
               FROM units u
                        LEFT JOIN user_units uu ON u.unit_id = uu.unit_id
                        LEFT JOIN system_units su ON u.unit_id = su.unit_id
               WHERE (uu.user_id = $1
                  OR su.system_unit_id IS NOT NULL)
                   AND u.abbreviation = $2
                   FETCH FIRST ROW ONLY)
        "#,
        user_id,
        abbreviation,
    )
        .fetch_one(&mut *conn)
        .await?;

    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn exists_user_unit(
    conn: &mut PgConnection,
    user_id: i32,
    unit_id: i32,
) -> Result<bool, sqlx::Error>
{
    let result = query!(
        r#"
SELECT EXISTS (SELECT 1
               FROM user_units
               WHERE user_id = $1
                 AND unit_id = $2
                   FETCH FIRST ROW ONLY)
        "#,
        user_id,
        unit_id,
    )
        .fetch_one(&mut *conn)
        .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn delete_unit_for_user(
    conn: &mut PgConnection,
    user_id: i32,
    unit_id: i32,
) -> Result<(), sqlx::Error>
{
    query!(
        r#"
DELETE
FROM user_units
WHERE user_id = $1
  AND unit_id = $2
        "#,
        user_id,
        unit_id,
    )
        .execute(&mut *conn)
        .await?;

    query!(
        r#"
DELETE
FROM units
WHERE unit_id = $1
        "#,
        unit_id,
    )
        .execute(&mut *conn)
        .await?;

    Ok(())
}