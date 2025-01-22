-- Active: 1736978323269@@127.0.0.1@3306@rational_mermaid_e918_db
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(5000),
  img VARCHAR(1000) NOT NULL,
  category ENUM('breakfast', 'lunch', 'dinner', 'snack', 'dessert'),
  creator_id VARCHAR(255) NOT NULL,
  Foreign Key (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE recipes;

SELECT 
        recipes.*,
        accounts.*
        FROM recipes 
        JOIN accounts ON recipes.creator_id = accounts.id
        WHERE recipes.id = 2;


CREATE TABLE ingredients(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipe_id INT NOT NULL,
  Foreign Key (recipe_id) REFERENCES recipes(id) ON DELETE CASCADE
);

DROP TABLE ingredients;

INSERT INTO ingredients (quantity, recipe_id, name) VALUES ('2 pounds', 3, 'cheese');

SELECT * FROM ingredients WHERE recipe_id = 3;

SELECT accounts.id, accounts.name, recipes.id, recipes.title FROM recipes JOIN accounts ON recipes.creator_id = accounts.id;

DELETE FROM ingredients WHERE name = 'cheese';

CREATE TABLE favorites(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  recipe_id INT NOT NULL,
  account_id VARCHAR(255) NOT NULL,
  Foreign Key (recipe_id) REFERENCES recipes(id),
  Foreign Key (account_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE favorites;