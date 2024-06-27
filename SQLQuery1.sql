create table tbEmployee(
 idEmployee int not null primary key identity(1,1),
 name nvarchar(200),
 phone varchar(11),
 address text,
 dob date,
 gender varchar(20),
 role varchar(50),
 salary decimal(18,2),
 password varchar(50)
)

create table tbCompany(
name varchar(200),
address text
)

create table tbCostofGood(
idCostofGood int not null primary key identity(1,1),
costname varchar(50),
cost decimal(18,2),
date date	
)

create table tbVehicleType(
idVehicleType int not null primary key identity(1,1),
name varchar(100),
class int
)

create table tbCustomer(
idCustomer int not null primary key identity(1,1),
name varchar(100),
phone varchar(11),
camo varchar(50),
camodel varchar(50),
address text,
points int,
idVehicleType int,
  CONSTRAINT fk_idVehicleType
  FOREIGN KEY (idVehicleType)
  REFERENCES tbVehicleType (idVehicleType)
)

create table tbService(
 idService int not null primary key identity(1,1),
 name varchar(100),
 price decimal(18,2)
)


CREATE TABLE tbCash (
    idCash INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    transno VARCHAR(50),
    price DECIMAL(18,2),
    date DATE,
    status VARCHAR(50),
    idVehicleType INT,
    idService INT,
    idCustomer INT,
    CONSTRAINT fk_tbCash_idVehicleType FOREIGN KEY (idVehicleType) REFERENCES tbVehicleType (idVehicleType),
    CONSTRAINT fk_tbCash_idService FOREIGN KEY (idService) REFERENCES tbService (idService),
    CONSTRAINT fk_tbCash_idCustomer FOREIGN KEY (idCustomer) REFERENCES tbCustomer (idCustomer)
);

CREATE TABLE tbProduct (
    idProduct INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    productName NVARCHAR(200),
    category NVARCHAR(100),
    price DECIMAL(18,2),
    stockQuantity INT,
    description TEXT
);

CREATE TABLE tbCategory (
    idCategory INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    categoryName NVARCHAR(100)
);

ALTER TABLE tbProduct
ADD idCategory INT,
CONSTRAINT fk_tbProduct_idCategory FOREIGN KEY (idCategory) REFERENCES tbCategory (idCategory);

-- Inserting data into tbEmployee
INSERT INTO tbEmployee (name, phone, address, dob, gender, role, salary, password)
VALUES 
('John Doe', '1234567890', '123 Main St', '1990-01-01', 'Male', 'Manager', 50000.00, 'password1'),
('Jane Smith', '1234567891', '124 Main St', '1992-02-02', 'Female', 'Assistant Manager', 45000.00, 'password2'),
('Bob Johnson', '1234567892', '125 Main St', '1993-03-03', 'Male', 'Clerk', 30000.00, 'password3'),
('Alice Brown', '1234567893', '126 Main St', '1994-04-04', 'Female', 'Cashier', 32000.00, 'password4'),
('Charlie Davis', '1234567894', '127 Main St', '1995-05-05', 'Male', 'Stock Manager', 40000.00, 'password5'),
('Eva Wilson', '1234567895', '128 Main St', '1996-06-06', 'Female', 'Sales Associate', 35000.00, 'password6'),
('Franklin Lee', '1234567896', '129 Main St', '1997-07-07', 'Male', 'Technician', 38000.00, 'password7'),
('Grace Kim', '1234567897', '130 Main St', '1998-08-08', 'Female', 'Customer Service', 33000.00, 'password8'),
('Harry White', '1234567898', '131 Main St', '1999-09-09', 'Male', 'Cleaner', 29000.00, 'password9'),
('Ivy King', '1234567899', '132 Main St', '2000-10-10', 'Female', 'Receptionist', 31000.00, 'password10');

-- Inserting data into tbCompany
INSERT INTO tbCompany (name, address)
VALUES ('My Company', '123 Corporate Ave');

-- Inserting data into tbCostofGood
INSERT INTO tbCostofGood (costname, cost, date)
VALUES
('Product A', 100.00, '2024-01-01'),
('Product B', 200.00, '2024-02-01'),
('Product C', 150.00, '2024-03-01'),
('Product D', 175.00, '2024-04-01'),
('Product E', 125.00, '2024-05-01'),
('Product F', 90.00, '2024-06-01'),
('Product G', 80.00, '2024-07-01'),
('Product H', 95.00, '2024-08-01'),
('Product I', 110.00, '2024-09-01'),
('Product J', 120.00, '2024-10-01');

-- Inserting data into tbVehicleType
INSERT INTO tbVehicleType (name, class)
VALUES
('Sedan', 1),
('SUV', 2),
('Truck', 3),
('Coupe', 4),
('Convertible', 5),
('Wagon', 6),
('Van', 7),
('Minivan', 8),
('Motorcycle', 9),
('Electric', 10);

-- Inserting data into tbCustomer
INSERT INTO tbCustomer (name, phone, camo, camodel, address, points, idVehicleType)
VALUES
('Customer1', '1234567890', 'Toyota', 'Camry', '123 Elm St', 100, 1),
('Customer2', '1234567891', 'Honda', 'Civic', '124 Elm St', 200, 2),
('Customer3', '1234567892', 'Ford', 'Focus', '125 Elm St', 150, 3),
('Customer4', '1234567893', 'Nissan', 'Altima', '126 Elm St', 250, 4),
('Customer5', '1234567894', 'BMW', '3 Series', '127 Elm St', 300, 5),
('Customer6', '1234567895', 'Audi', 'A4', '128 Elm St', 350, 6),
('Customer7', '1234567896', 'Mercedes', 'C-Class', '129 Elm St', 400, 7),
('Customer8', '1234567897', 'Hyundai', 'Elantra', '130 Elm St', 450, 8),
('Customer9', '1234567898', 'Kia', 'Optima', '131 Elm St', 500, 9),
('Customer10', '1234567899', 'Chevy', 'Malibu', '132 Elm St', 550, 10);

-- Inserting data into tbService
INSERT INTO tbService (name, price)
VALUES
('Oil Change', 29.99),
('Tire Rotation', 19.99),
('Brake Inspection', 49.99),
('Battery Check', 39.99),
('Engine Tune-Up', 59.99),
('Wheel Alignment', 69.99),
('Transmission Flush', 89.99),
('Air Filter Replacement', 25.99),
('Radiator Service', 79.99),
('Fuel System Cleaning', 99.99);

-- Inserting data into tbCash
INSERT INTO tbCash (transno, price, date, status, idVehicleType, idService, idCustomer)
VALUES
('TXN001', 50.00, '2024-01-01', 'Completed', 1, 1, 1),
('TXN002', 75.00, '2024-01-02', 'Pending', 2, 2, 2),
('TXN003', 100.00, '2024-01-03', 'Completed', 3, 3, 3),
('TXN004', 125.00, '2024-01-04', 'Completed', 4, 4, 4),
('TXN005', 150.00, '2024-01-05', 'Pending', 5, 5, 5),
('TXN006', 175.00, '2024-01-06', 'Completed', 6, 6, 6),
('TXN007', 200.00, '2024-01-07', 'Completed', 7, 7, 7),
('TXN008', 225.00, '2024-01-08', 'Pending', 8, 8, 8),
('TXN009', 250.00, '2024-01-09', 'Completed', 9, 9, 9),
('TXN010', 275.00, '2024-01-10', 'Completed', 10, 10, 10);

-- Inserting data into tbCategory
INSERT INTO tbCategory (categoryName)
VALUES
('Electronics'),
('Groceries'),
('Clothing'),
('Automotive'),
('Furniture'),
('Toys'),
('Books'),
('Health & Beauty'),
('Sports & Outdoors'),
('Home & Garden');

-- Inserting data into tbProduct
INSERT INTO tbProduct (productName, category, price, stockQuantity, description, idCategory)
VALUES
('Product1', 'Electronics', 199.99, 50, 'Description for Product 1', 1),
('Product2', 'Groceries', 9.99, 200, 'Description for Product 2', 2),
('Product3', 'Clothing', 29.99, 150, 'Description for Product 3', 3),
('Product4', 'Automotive', 99.99, 75, 'Description for Product 4', 4),
('Product5', 'Furniture', 299.99, 20, 'Description for Product 5', 5),
('Product6', 'Toys', 19.99, 300, 'Description for Product 6', 6),
('Product7', 'Books', 14.99, 100, 'Description for Product 7', 7),
('Product8', 'Health & Beauty', 24.99, 80, 'Description for Product 8', 8),
('Product9', 'Sports & Outdoors', 49.99, 60, 'Description for Product 9', 9),
('Product10', 'Home & Garden', 39.99, 90, 'Description for Product 10', 10);

