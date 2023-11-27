-- Add migration script here
INSERT INTO units (name, abbreviation)
VALUES ('kilogram', 'kg'),
       ('liter', 'L'),
       ('piece', 'pcs'),
       ('gram', 'g');


INSERT INTO system_units (unit_id)
VALUES ((SELECT unit_id FROM units WHERE abbreviation = 'kg')),
       ((SELECT unit_id FROM units WHERE abbreviation = 'L')),
       ((SELECT unit_id FROM units WHERE abbreviation = 'pcs')),
       ((SELECT unit_id FROM units WHERE abbreviation = 'g'));


INSERT INTO currencies (name, abbreviation)
VALUES ('US Dollar', 'USD'),
       ('Euro', 'EUR'),
       ('British Pound', 'GBP'),
       ('Japanese Yen', 'JPY'),
       ('Polish Zloty', 'PLN');

INSERT INTO system_currencies (currency_id)
VALUES ((SELECT currency_id FROM currencies WHERE abbreviation = 'USD')),
       ((SELECT currency_id FROM currencies WHERE abbreviation = 'EUR')),
       ((SELECT currency_id FROM currencies WHERE abbreviation = 'GBP')),
       ((SELECT currency_id FROM currencies WHERE abbreviation = 'JPY')),
       ((SELECT currency_id FROM currencies WHERE abbreviation = 'PLN'));

INSERT INTO categories (name)
VALUES ('dairy'),
       ('bakery'),
       ('fruits'),
       ('vegetables'),
       ('meats');


INSERT INTO system_categories (category_id)
VALUES ((SELECT category_id FROM categories WHERE name = 'dairy')),
       ((SELECT category_id FROM categories WHERE name = 'bakery')),
       ((SELECT category_id FROM categories WHERE name = 'fruits')),
       ((SELECT category_id FROM categories WHERE name = 'vegetables')),
       ((SELECT category_id FROM categories WHERE name = 'meats'));

INSERT INTO templates (name, expiration_span, amount, unit_id, price, currency_id, category_id)
VALUES ('Milk', INTERVAL '10 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'L'), 2.50,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'dairy')),
       ('Bread', INTERVAL '5 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'pcs'), 3.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'bakery')),
       ('Apples', INTERVAL '20 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'kg'), 4.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'fruits')),
       ('Chicken Breast', INTERVAL '7 days', 0.5, (SELECT unit_id FROM units WHERE abbreviation = 'kg'), 5.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'meats'));

INSERT INTO system_templates (template_id)
VALUES ((SELECT template_id FROM templates WHERE name = 'Milk')),
       ((SELECT template_id FROM templates WHERE name = 'Bread')),
       ((SELECT template_id FROM templates WHERE name = 'Apples')),
       ((SELECT template_id FROM templates WHERE name = 'Chicken Breast'));
