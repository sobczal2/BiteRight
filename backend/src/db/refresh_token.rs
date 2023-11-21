use crate::models::query_objects::refresh_token::{
    CreateRefreshTokenQuery, FetchRefreshTokenQueryResult,
};
use sqlx::query;
use sqlx::{query_as, PgConnection};

pub async fn create_refresh_token(
    conn: &mut PgConnection,
    create_refresh_token_query: CreateRefreshTokenQuery,
) -> Result<FetchRefreshTokenQueryResult, sqlx::Error> {
    let result = query_as!(
        FetchRefreshTokenQueryResult,
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

    Ok(FetchRefreshTokenQueryResult {
        refresh_token_id: result.refresh_token_id,
        user_id: result.user_id,
        token: result.token,
        expiration: result.expiration,
    })
}

pub async fn delete_refresh_tokens_for_user(
    conn: &mut PgConnection,
    user_id: i32,
) -> Result<(), sqlx::Error> {
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

pub async fn find_refresh_token_by_user_id(
    conn: &mut PgConnection,
    user_id: i32,
) -> Result<Option<FetchRefreshTokenQueryResult>, sqlx::Error> {
    let result = query_as!(
        FetchRefreshTokenQueryResult,
        r#"
SELECT refresh_token_id, user_id, token, expiration
FROM refresh_tokens
WHERE user_id = $1
            "#,
        user_id
    )
    .fetch_optional(&mut *conn)
    .await?;

    Ok(result)
}
