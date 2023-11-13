-- Add migration script here
CREATE TABLE users
(
    user_id       SERIAL PRIMARY KEY,
    name          VARCHAR(64) UNIQUE  NOT NULL,
    email         VARCHAR(320) UNIQUE NOT NULL,
    password_hash VARCHAR(128)        NOT NULL,
    created_at    TIMESTAMP           NOT NULL DEFAULT NOW()
);

CREATE TABLE units
(
    unit_id      SERIAL PRIMARY KEY,
    name         VARCHAR(64) NOT NULL,
    abbreviation VARCHAR(16) NOT NULL,
    created_at   TIMESTAMP   NOT NULL DEFAULT NOW(),
    updated_at   TIMESTAMP   NOT NULL DEFAULT NOW()
);

CREATE TABLE user_units
(
    user_id INTEGER REFERENCES users (user_id) NOT NULL,
    unit_id INTEGER REFERENCES units (unit_id) NOT NULL,
    PRIMARY KEY (user_id, unit_id)
);

CREATE TABLE system_units
(
    system_unit_id SERIAL PRIMARY KEY,
    unit_id        INTEGER REFERENCES units (unit_id) NOT NULL
);

CREATE TABLE currencies
(
    currency_id  SERIAL PRIMARY KEY,
    name         VARCHAR(64) NOT NULL,
    abbreviation VARCHAR(16) NOT NULL,
    created_at   TIMESTAMP   NOT NULL DEFAULT NOW(),
    updated_at   TIMESTAMP   NOT NULL DEFAULT NOW()
);

CREATE TABLE user_currencies
(
    user_id     INTEGER REFERENCES users (user_id)          NOT NULL,
    currency_id INTEGER REFERENCES currencies (currency_id) NOT NULL,
    PRIMARY KEY (user_id, currency_id)
);

CREATE TABLE system_currencies
(
    system_currency_id SERIAL PRIMARY KEY,
    currency_id        INTEGER REFERENCES currencies (currency_id) NOT NULL
);

CREATE TABLE categories
(
    category_id SERIAL PRIMARY KEY,
    name        VARCHAR(64)  NOT NULL,
    photo_url   VARCHAR(256) NULL,
    created_at  TIMESTAMP    NOT NULL DEFAULT NOW(),
    updated_at  TIMESTAMP    NOT NULL DEFAULT NOW()
);

CREATE TABLE user_categories
(
    user_id     INTEGER REFERENCES users (user_id)          NOT NULL,
    category_id INTEGER REFERENCES categories (category_id) NOT NULL,
    PRIMARY KEY (user_id, category_id)
);

CREATE TABLE system_categories
(
    system_category_id SERIAL PRIMARY KEY,
    category_id        INTEGER REFERENCES categories (category_id) NOT NULL
);

CREATE TABLE products
(
    product_id      SERIAL PRIMARY KEY,
    user_id         INTEGER REFERENCES users (user_id)          NOT NULL,
    name            VARCHAR(64)                                 NOT NULL,
    description     VARCHAR(1024)                               NOT NULL,
    expiration_date TIMESTAMP                                   NULL,
    created_at      TIMESTAMP                                   NOT NULL DEFAULT NOW(),
    updated_at      TIMESTAMP                                   NOT NULL DEFAULT NOW(),
    amount          FLOAT                                       NOT NULL,
    unit_id         INTEGER REFERENCES units (unit_id)          NOT NULL,
    price           MONEY                                       NULL,
    currency_id     INTEGER REFERENCES currencies (currency_id) NULL,
    amount_left     FLOAT                                       NOT NULL,
    category_id     INTEGER REFERENCES categories (category_id) NOT NULL
);

CREATE TABLE product_templates
(
    product_template_id SERIAL PRIMARY KEY,
    name                VARCHAR(64)                                 NOT NULL,
    expiration_span     INTERVAL                                    NOT NULL,
    amount              FLOAT                                       NOT NULL,
    unit_id             INTEGER REFERENCES units (unit_id)          NOT NULL,
    price               MONEY                                       NULL,
    currency_id         INTEGER REFERENCES currencies (currency_id) NULL,
    category_id         INTEGER REFERENCES categories (category_id) NOT NULL
);

CREATE TABLE user_product_templates
(
    user_id             INTEGER REFERENCES users (user_id)                         NOT NULL,
    product_template_id INTEGER REFERENCES product_templates (product_template_id) NOT NULL,
    PRIMARY KEY (user_id, product_template_id)
);

CREATE TABLE system_product_templates
(
    system_product_template_id SERIAL PRIMARY KEY,
    product_template_id        INTEGER REFERENCES product_templates (product_template_id) NOT NULL
);

