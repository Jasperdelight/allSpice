CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE
recipes(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL,
  instructions VARCHAR(255),
  img VARCHAR(700) NOT NULL,
  category ENUM('Pasta', 'Soup', 'Fish', 'Salads', 'Beef', 'Burgers', 'Misc', 'Cheese', 'Italian', 'Mexican', 'Specialty Coffee' ) DEFAULT 'misc',
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

CREATE TABLE
ingredients(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';


CREATE TABLE
favorites(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  accountId VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,
  FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
)default charset utf8 COMMENT '';

   SELECT * FROM recipes WHERE Id = 1;

   INSERT INTO favorites(id, accountId, recipeId)
   VALUES (2, jhkahreah21, 53);
  
  DROP TABLE recipes;