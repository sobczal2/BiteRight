use sqlx::query;
use sqlx::{query_as, PgConnection};

use crate::models::query_objects::user::{CreateUserQuery, FetchUserQueryResult};

pub async fn exists_user_by_email(
    conn: &mut PgConnection,
    email: String,
) -> Result<bool, sqlx::Error> {
    let result = query!(
        r#"
SELECT EXISTS (SELECT 1
               FROM users
               WHERE email = $1)
            "#,
        email
    )
    .fetch_one(&mut *conn)
    .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn exists_user_by_name(
    conn: &mut PgConnection,
    name: String,
) -> Result<bool, sqlx::Error> {
    let result = query!(
        r#"
SELECT EXISTS (SELECT 1
               FROM users
               WHERE name = $1)
            "#,
        name
    )
    .fetch_one(&mut *conn)
    .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn create_user(
    conn: &mut PgConnection,
    create_user_query: CreateUserQuery,
) -> Result<FetchUserQueryResult, sqlx::Error> {
    let result = query_as!(
        FetchUserQueryResult,
        r#"
INSERT INTO users (name, email, password_hash)
VALUES ($1, $2, $3)
RETURNING user_id, name, email, password_hash, created_at
            "#,
        create_user_query.name,
        create_user_query.email,
        create_user_query.password_hash
    )
    .fetch_one(&mut *conn)
    .await?;

    Ok(FetchUserQueryResult {
        user_id: result.user_id,
        name: result.name,
        email: result.email,
        password_hash: result.password_hash,
        created_at: result.created_at,
    })
}

pub async fn find_user_by_email(
    conn: &mut PgConnection,
    email: String,
) -> Result<Option<FetchUserQueryResult>, sqlx::Error> {
    let result = query_as!(
        FetchUserQueryResult,
        r#"
SELECT user_id, name, email, password_hash, created_at
FROM users
WHERE email = $1
            "#,
        email
    )
    .fetch_optional(&mut *conn)
    .await?;

    Ok(result)
}

pub async fn find_user_by_id(
    conn: &mut PgConnection,
    user_id: i32,
) -> Result<Option<FetchUserQueryResult>, sqlx::Error> {
    let result = query_as!(
        FetchUserQueryResult,
        r#"
SELECT user_id, name, email, password_hash, created_at
FROM users
WHERE user_id = $1
            "#,
        user_id
    )
    .fetch_optional(&mut *conn)
    .await?;

    Ok(result)
}
