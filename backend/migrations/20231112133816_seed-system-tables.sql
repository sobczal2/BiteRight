-- Add migration script here
INSERT INTO units (name, abbreviation)
VALUES ('Kilogram', 'kg'),
       ('Liter', 'L'),
       ('Piece', 'pcs'),
       ('Gram', 'g');


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
VALUES ('Dairy'),
       ('Bakery'),
       ('Fruits'),
       ('Vegetables'),
       ('Meats');


INSERT INTO system_categories (category_id)
VALUES ((SELECT category_id FROM categories WHERE name = 'Dairy')),
       ((SELECT category_id FROM categories WHERE name = 'Bakery')),
       ((SELECT category_id FROM categories WHERE name = 'Fruits')),
       ((SELECT category_id FROM categories WHERE name = 'Vegetables')),
       ((SELECT category_id FROM categories WHERE name = 'Meats'));

INSERT INTO product_templates (name, expiration_span, amount, unit_id, price, currency_id, category_id)
VALUES ('Milk', INTERVAL '10 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'L'), 2.50,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'Dairy')),
       ('Bread', INTERVAL '5 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'pcs'), 3.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'Bakery')),
       ('Apples', INTERVAL '20 days', 1.0, (SELECT unit_id FROM units WHERE abbreviation = 'kg'), 4.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'Fruits')),
       ('Chicken Breast', INTERVAL '7 days', 0.5, (SELECT unit_id FROM units WHERE abbreviation = 'kg'), 5.00,
        (SELECT currency_id FROM currencies WHERE abbreviation = 'USD'),
        (SELECT category_id FROM categories WHERE name = 'Meats'));

INSERT INTO system_product_templates (product_template_id)
VALUES ((SELECT product_template_id FROM product_templates WHERE name = 'Milk')),
       ((SELECT product_template_id FROM product_templates WHERE name = 'Bread')),
       ((SELECT product_template_id FROM product_templates WHERE name = 'Apples')),
       ((SELECT product_template_id FROM product_templates WHERE name = 'Chicken Breast'));
