CREATE DATABASE dapper_test;

USE dapper_test;

CREATE TABLE shops
(
  id INT PRIMARY KEY,
  name varchar(50)
);

CREATE TABLE products
(
  id INT PRIMARY KEY,
  name varchar(50),
  unit_price decimal(18,2),
  shop_id INT,
  FOREIGN KEY(shop_id) REFERENCES shops(id)
);

CREATE TABLE sales
(
   id INT PRIMARY KEY,
   product_id INT,
   quantity INT,
   FOREIGN KEY (product_id) REFERENCES products(id)
);


INSERT INTO shops(id,name) VALUES(1, 'Bimbo S.A DE C.V');
INSERT INTO shops(id,name) VALUES(2,'Soriana de México S.A. de C.V.');

INSERT INTO products(id, name, unit_price, shop_id) VALUES(1,'Donitas', 18.5,1);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(2,'Nito', 13.5,1);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(3,'Chocorroles', 16,1);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(4,'Pay de Piña', 20,1);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(5,'Pay de Nuez', 22,1);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(6,'Escoba', 45.1,2);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(7,'Trapeador', 89,2);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(8,'Limpiador Brasso', 35,2);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(9,'Papel Higienico', 50,2);
INSERT INTO products(id, name, unit_price, shop_id) VALUES(10,'Almohada', 60,2);

INSERT INTO sales(id, product_id, quantity) VALUES (1,1,20);
INSERT INTO sales(id, product_id, quantity) VALUES (2,1,15);
INSERT INTO sales(id, product_id, quantity) VALUES (3,2,10);
INSERT INTO sales(id, product_id, quantity) VALUES (4,2,8);
INSERT INTO sales(id, product_id, quantity) VALUES (5,3,7);
INSERT INTO sales(id, product_id, quantity) VALUES (6,3,6);
INSERT INTO sales(id, product_id, quantity) VALUES (7,4,4);
INSERT INTO sales(id, product_id, quantity) VALUES (8,4,5);
INSERT INTO sales(id, product_id, quantity) VALUES (9,5,5);
INSERT INTO sales(id, product_id, quantity) VALUES (10,5,8);
INSERT INTO sales(id, product_id, quantity) VALUES (11,6,9);
INSERT INTO sales(id, product_id, quantity) VALUES (12,6,3);
INSERT INTO sales(id, product_id, quantity) VALUES (13,7,67);
INSERT INTO sales(id, product_id, quantity) VALUES (14,7,12);
INSERT INTO sales(id, product_id, quantity) VALUES (15,8,56);
INSERT INTO sales(id, product_id, quantity) VALUES (16,8,36);
INSERT INTO sales(id, product_id, quantity) VALUES (17,9,46);
INSERT INTO sales(id, product_id, quantity) VALUES (18,9,76);
INSERT INTO sales(id, product_id, quantity) VALUES (19,10,36);
INSERT INTO sales(id, product_id, quantity) VALUES (20,10,56);




