use crate::models::entities::user::User;
use crate::models::query_objects::user::CreateUserQuery;
use sqlx::{query, query_as, Executor, Postgres};

pub async fn exists_by_email<'e, 'c: 'e, E>(executor: E, email: String) -> Result<bool, sqlx::Error>
where
    E: 'e + Executor<'c, Database = Postgres>,
{
    let result = query!(
        r#"
                SELECT EXISTS (
                    SELECT 1
                    FROM users
                    WHERE email = $1
                )
            "#,
        email
    )
    .fetch_one(executor)
    .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn exists_by_name<'e, 'c: 'e, E>(executor: E, name: String) -> Result<bool, sqlx::Error>
where
    E: 'e + Executor<'c, Database = Postgres>,
{
    let result = query!(
        r#"
                SELECT EXISTS (
                    SELECT 1
                    FROM users
                    WHERE name = $1
                )
            "#,
        name
    )
    .fetch_one(executor)
    .await?;
    result.exists.ok_or(sqlx::Error::RowNotFound)
}

pub async fn create<'e, 'c: 'e, E>(
    executor: E,
    create_user_query: CreateUserQuery,
) -> Result<User, sqlx::Error>
where
    E: 'e + Executor<'c, Database = Postgres>,
{
    let result = query_as!(
        User,
        r#"
                INSERT INTO users (name, email, password_hash)
                VALUES ($1, $2, $3)
                RETURNING user_id, name, email, password_hash, created_at
            "#,
        create_user_query.name,
        create_user_query.email,
        create_user_query.password_hash
    )
    .fetch_one(executor)
    .await?;

    Ok(User {
        user_id: result.user_id,
        name: result.name,
        email: result.email,
        password_hash: result.password_hash,
        created_at: result.created_at,
    })
}
