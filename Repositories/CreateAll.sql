CREATE SCHEMA map;
GO

CREATE SCHEMA production;
go

CREATE SCHEMA sales;
go
CREATE TABLE map.locations(
	location_id INT IDENTITY(1,1) PRIMARY KEY,
	location_name VARCHAR(255),
	x_cord decimal not null,
	y_cord decimal not null,
	rsid int not null,
);
-- create tables
CREATE TABLE production.categories (
	category_id INT IDENTITY (1, 1) PRIMARY KEY,
	category_name VARCHAR (255) NOT NULL
);

CREATE TABLE production.products (
	product_id INT IDENTITY (1, 1) PRIMARY KEY,
	product_name VARCHAR (255) NOT NULL,
	image_name VARCHAR(255) NOT NULL,
	weigth DECIMAL NOT NULL,
	category_id INT NOT NULL,
	model_year SMALLINT,
	list_price DECIMAL (10, 2) NOT NULL,
	FOREIGN KEY (category_id) REFERENCES production.categories (category_id) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE sales.couriers(
	courier_id INT IDENTITY(1,1) PRIMARY KEY,
	company_name VARCHAR(255) NOT NULL,
	image_name VARCHAR(255)
);
CREATE TABLE sales.customers (
	customer_id INT IDENTITY (1, 1) PRIMARY KEY,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	image_name VARCHAR(255),
	phone VARCHAR (25),
	email VARCHAR (255) NOT NULL
);

CREATE TABLE sales.stores (
	store_id INT IDENTITY (1, 1) PRIMARY KEY,
	store_name VARCHAR (255) NOT NULL,
	image_name VARCHAR(255),
	location_id INT NOT NULL,
	CONSTRAINT FK_Stores_To_Locations FOREIGN KEY(location_id) REFERENCES map.locations (location_id)
);

CREATE TABLE sales.orders (
	order_id INT IDENTITY (1, 1) PRIMARY KEY,
	customer_id INT NOT NULL,
	order_status tinyint NOT NULL,
	-- Order status: 1 = Pending; 2 = Processing; 3 = Rejected; 4 = Completed
	expected_delivery_price MONEY NOT NULL,
	order_date DATE NOT NULL,
	expected_date DATE NOT NULL,
	shipped_date DATE,
	store_id INT NOT NULL,
	FOREIGN KEY (customer_id) REFERENCES sales.customers (customer_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (store_id) REFERENCES sales.stores (store_id) ON DELETE CASCADE ON UPDATE CASCADE,
);

CREATE TABLE map.ordered_locations(
	ordered_location_id int identity(1,1) primary key,
	order_id int not null,
	location_id_from int not null,
	location_id_to int not null,
	FOREIGN KEY (location_id_from) REFERENCES map.locations (location_id),
	FOREIGN KEY (location_id_to) REFERENCES map.locations (location_id),
	FOREIGN KEY (order_id) REFERENCES sales.orders (order_id)
);

CREATE TABLE sales.order_items (
	order_id INT,
	item_id INT,
	product_id INT NOT NULL,
	quantity INT NOT NULL,
	list_price DECIMAL (10, 2) NOT NULL,
	discount DECIMAL (4, 2) NOT NULL DEFAULT 0,
	PRIMARY KEY (order_id, item_id),
	FOREIGN KEY (order_id) REFERENCES sales.orders (order_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (product_id) REFERENCES production.products (product_id) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE sales.offers (
	offer_id INT IDENTITY (1, 1) PRIMARY KEY,
	courier_id INT NOT NULL,
	order_id INT NOT NULL,
	order_status tinyint NOT NULL,
	-- Order status: 1 = Pending; 2 = Processing; 3 = Rejected; 4 = Completed
	offer_date DATE NOT NULL,
	expected_delivery_date DATE NOT NULL,
	expected_delivery_price MONEY NOT NULL,
	FOREIGN KEY (order_id) REFERENCES sales.orders (order_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (courier_id) REFERENCES sales.couriers (courier_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE production.stocks (
	store_id INT,
	product_id INT,
	quantity INT,
	PRIMARY KEY (store_id, product_id),
	FOREIGN KEY (store_id) REFERENCES sales.stores (store_id) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (product_id) REFERENCES production.products (product_id) ON DELETE CASCADE ON UPDATE CASCADE
);