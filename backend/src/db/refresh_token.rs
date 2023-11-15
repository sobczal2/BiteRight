use crate::models::entities::refresh_token::RefreshToken;
use sqlx::{PgConnection, query_as};
use sqlx::query;
use crate::models::query_objects::refresh_token::CreateRefreshTokenQuery;

pub async fn create_refresh_token(
    conn: &mut PgConnection,
    create_refresh_token_query: CreateRefreshTokenQuery,
) -> Result<RefreshToken, sqlx::Error>
{
    let result = query_as!(
        RefreshToken,
        r#"
                INSERT INTO refresh_tokens (user_id, token, expiration)
                VALUES ($1, $2, $3)
                RETURNING refresh_token_id, user_id, token, expiration
            "#,
        create_refresh_token_query.user_id,
        create_refresh_token_query.token,
        create_refresh_token_query.expiration
    )
    .fetch_one(&mut *conn)
    .await?;

    Ok(RefreshToken {
        refresh_token_id: result.refresh_token_id,
        user_id: result.user_id,
        token: result.token,
        expiration: result.expiration,
    })
}

pub async fn delete_refresh_tokens_for_user(conn: &mut PgConnection, user_id: i32) -> Result<(), sqlx::Error>
{
    query!(
        r#"
                DELETE FROM refresh_tokens
                WHERE user_id = $1
            "#,
        user_id
    )
    .execute(&mut *conn)
    .await?;

    Ok(())
}
