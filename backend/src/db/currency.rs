use sqlx::{PgConnection, query, query_as, query_as_unchecked};
use crate::models::query_objects::currency::{FetchCurrencyQueryResult, ListCurrenciesForUserQuery};
use crate::utils::db::get_skip_and_take;

pub async fn list_currencies_for_user(
    conn: &mut PgConnection,
    list_currencies_for_user_query: ListCurrenciesForUserQuery,
) -> Result<(Vec<FetchCurrencyQueryResult>, i32), sqlx::Error> {
    let (skip, take) = get_skip_and_take(list_currencies_for_user_query.page, list_currencies_for_user_query.per_page);
    let currencies = query_as_unchecked!(
        FetchCurrencyQueryResult,
        r#"
SELECT c.currency_id,
         c.name,
         c.code,
         c.symbol,
         c.created_at,
         c.updated_at,
         CASE
           WHEN uc.user_id IS NOT NULL THEN true
           ELSE false
           END AS can_modify
FROM currencies c
            LEFT JOIN user_currencies uc ON c.currency_id = uc.currency_id
            LEFT JOIN system_currencies sc ON c.currency_id = sc.currency_id
WHERE uc.user_id = $1
    OR sc.system_currency_id IS NOT NULL
ORDER BY c.name
OFFSET $2 ROWS FETCH NEXT $3 ROWS ONLY
        "#,
        list_currencies_for_user_query.user_id,
        skip,
        take,
    )
        .fetch_all(&mut *conn)
        .await?;

    let count = query!(
        r#"
SELECT COUNT(*)::INT as count
FROM currencies c
            LEFT JOIN user_currencies uc ON c.currency_id = uc.currency_id
            LEFT JOIN system_currencies sc ON c.currency_id = sc.currency_id
WHERE uc.user_id = $1
    OR sc.system_currency_id IS NOT NULL
        "#,
        list_currencies_for_user_query.user_id,
    )
        .fetch_one(&mut *conn)
        .await?
        .count
        .unwrap_or(0);

    Ok((currencies, count))
}