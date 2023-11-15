use crate::models::entities::unit::Unit;
use sqlx::{query, Postgres, Acquire, PgConnection};
use crate::models::query_objects::unit::CreateUnitForUserQuery;

pub async fn list_units_for_user(
    conn: &mut PgConnection,
    user_id: i32,
    page: i32,
    per_page: i32,
) -> Result<(Vec<Unit>, i32), sqlx::Error>
{
    let records = query!(
        r#"
WITH unit_data AS (
    SELECT u.unit_id,
           u.name,
           u.abbreviation,
           u.created_at,
           u.updated_at
    FROM units u
             LEFT JOIN user_units uu ON u.unit_id = uu.unit_id
             LEFT JOIN system_units su ON u.unit_id = su.unit_id
    WHERE uu.user_id = $1
       OR su.system_unit_id IS NOT NULL
),
unit_count AS (
    SELECT COUNT(*) as total_count FROM unit_data
)
SELECT
    unit_id,
    name,
    abbreviation,
    created_at,
    updated_at,
    (SELECT total_count FROM unit_count)::INT as total_count
FROM unit_data
ORDER BY unit_id
OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY
        "#,
        user_id,
        (page * per_page) as i32,
        per_page as i32,
    )
        .fetch_all(&mut *conn)
        .await?;

    let count = if let Some(first_unit) = records.first() {
        first_unit.total_count.unwrap_or(0)
    } else {
        0
    };

    let units = records
        .into_iter()
        .map(|r| Unit {
            unit_id: r.unit_id,
            name: r.name,
            abbreviation: r.abbreviation,
            created_at: r.created_at,
            updated_at: r.updated_at,
        })
        .collect();

    Ok((units, count))
}

pub async fn create_units_for_user(
    conn: &mut PgConnection,
    create_unit_for_user_query: CreateUnitForUserQuery,
) -> Result<Unit, sqlx::Error>
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

    Ok(Unit {
        unit_id: result.unit_id,
        name: result.name,
        abbreviation: result.abbreviation,
        created_at: result.created_at,
        updated_at: result.updated_at,
    })
}