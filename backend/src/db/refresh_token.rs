use crate::models::entities::refresh_token::RefreshToken;
use crate::models::query_objects::refresh_token::CreateRefreshTokenQuery;
use sqlx::{query_as, Executor, Postgres};

pub async fn create<'e, 'c: 'e, E>(
    executor: E,
    create_user_query: CreateRefreshTokenQuery,
) -> Result<RefreshToken, sqlx::Error>
where
    E: 'e + Executor<'c, Database = Postgres>,
{
    let result = query_as!(
        RefreshToken,
        r#"
                INSERT INTO refresh_tokens (user_id, token, expiration)
                VALUES ($1, $2, $3)
                RETURNING refresh_token_id, user_id, token, expiration
            "#,
        create_user_query.user_id,
        create_user_query.token,
        create_user_query.expiration
    )
    .fetch_one(executor)
    .await?;

    Ok(RefreshToken {
        refresh_token_id: result.refresh_token_id,
        user_id: result.user_id,
        token: result.token,
        expiration: result.expiration,
    })
}
