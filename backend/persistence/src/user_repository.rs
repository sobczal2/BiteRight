use sqlx::{Executor, Postgres, query, query_as};
use domain::user::User;

pub struct UserRepository;

impl UserRepository {
    pub fn new() -> Self {
        Self {}
    }

    pub async fn exists_by_email<'e, 'c: 'e, E>(&self, executor: E, email: String) -> Result<bool, sqlx::Error>
        where E: 'e + Executor<'c, Database=Postgres>
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

    pub async fn exists_by_name<'e, 'c: 'e, E>(&self, executor: E, name: String) -> Result<bool, sqlx::Error>
        where E: 'e + Executor<'c, Database=Postgres>
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

    pub async fn create<'e, 'c: 'e, E>(&self, executor: E, user: User) -> Result<User, sqlx::Error>
        where E: 'e + Executor<'c, Database=Postgres>
    {
        let user = query_as!(User,
            r#"
                INSERT INTO users (name, email, password_hash)
                VALUES ($1, $2, $3)
                RETURNING user_id, name, email, password_hash, created_at
            "#,
            user.name,
            user.email,
            user.password_hash
        )
            .fetch_one(executor)
            .await?;

        Ok(user)
    }
}