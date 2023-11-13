use sqlx::{Executor, Postgres, query, query_as};
use domain::refresh_token::RefreshToken;

pub struct RefreshTokenRepository;

impl RefreshTokenRepository {
    pub fn new() -> Self {
        Self {}
    }

    pub async fn create<'e, 'c: 'e, E>(&self, executor: E, refresh_token: RefreshToken) -> Result<RefreshToken, sqlx::Error>
        where E: 'e + Executor<'c, Database=Postgres>
    {
        let refresh_token = query_as!(
            RefreshToken,
            r#"
                INSERT INTO refresh_tokens (user_id, token, expiration)
                VALUES ($1, $2, $3)
                RETURNING refresh_token_id, user_id, token, expiration
            "#,
            refresh_token.user_id,
            refresh_token.token,
            refresh_token.expiration
        )
            .fetch_one(executor)
            .await?;

        Ok(refresh_token)
    }

    pub async fn delete_for_user<'e, 'c: 'e, E>(&self, executor: E, user_id: i32) -> Result<(), sqlx::Error>
        where E: 'e + Executor<'c, Database=Postgres>
    {
        query!(
            r#"
                DELETE FROM refresh_tokens
                WHERE user_id = $1
            "#,
            user_id
        )
            .execute(executor)
            .await?;

        Ok(())
    }
}
