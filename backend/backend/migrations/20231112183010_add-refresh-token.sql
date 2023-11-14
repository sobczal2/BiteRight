-- Add migration script here
CREATE TABLE refresh_tokens
(
    refresh_token_id SERIAL PRIMARY KEY,
    user_id          INTEGER REFERENCES users (user_id) NOT NULL,
    token            VARCHAR(64)                        NOT NULL,
    expiration       TIMESTAMP                          NOT NULL
)