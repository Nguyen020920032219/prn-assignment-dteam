CREATE DATABASE CarWash;
GO

USE CarWash;
GO

CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Address VARCHAR(255)
);

INSERT INTO Customer (Name, Phone, Address) VALUES
('John Doe', '0912345678', '123 Main St'),
('Jane Smith', '0987654321', '456 Oak St'),
('Mike Johnson', '0901234567', '789 Pine St'),
('Emily Davis', '0934567890', '101 Maple St'),
('David Wilson', '0945678901', '202 Cedar St'),
('Sophia Martinez', '0923456789', '303 Birch St'),
('James Brown', '0919876543', '404 Elm St'),
('Olivia Jones', '0908765432', '505 Aspen St'),
('Daniel Garcia', '0987654320', '606 Spruce St'),
('Ava Lee', '0912345670', '707 Redwood St');

CREATE TABLE Vehicle (
    VehicleId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    CustomerId INT,
    LicensePlate VARCHAR(50),
    Make VARCHAR(255),
    Model VARCHAR(255),
    Year INT,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

INSERT INTO Vehicle (CustomerId, LicensePlate, Make, Model, Year) VALUES
(1, 'ABC123', 'Toyota', 'Camry', 2015),
(2, 'DEF456', 'Honda', 'Civic', 2018),
(3, 'GHI789', 'Ford', 'Focus', 2017),
(4, 'JKL012', 'Mazda', '3', 2016),
(5, 'MNO345', 'Hyundai', 'Elantra', 2019),
(6, 'PQR678', 'Kia', 'Cerato', 2020),
(7, 'STU901', 'BMW', 'X5', 2014),
(8, 'VWX234', 'Mercedes', 'C200', 2013),
(9, 'YZA567', 'Audi', 'A4', 2012),
(10, 'BCD890', 'Chevrolet', 'Cruze', 2011);

CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Address VARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(10),
    IsActive BIT NOT NULL DEFAULT 1
);

INSERT INTO Employee (Name, Phone, Address, DateOfBirth, Gender, IsActive) VALUES
('John Smith', '0912345671', '123 Main St', '1980-01-01', 'Male', 1),
('Jane Johnson', '0987654322', '456 Oak St', '1985-02-02', 'Female', 1),
('Michael Williams', '0901234568', '789 Pine St', '1990-03-03', 'Male', 1),
('Emily Brown', '0934567891', '101 Maple St', '1995-04-04', 'Female', 1),
('David Jones', '0945678902', '202 Cedar St', '2000-05-05', 'Male', 1),
('Sophia Garcia', '0923456790', '303 Birch St', '1982-06-06', 'Female', 1),
('James Martinez', '0919876544', '404 Elm St', '1987-07-07', 'Male', 1),
('Olivia Rodriguez', '0908765433', '505 Aspen St', '1992-08-08', 'Female', 1),
('Daniel Hernandez', '0987654321', '606 Spruce St', '1997-09-09', 'Male', 1),
('Ava Wilson', '0912345672', '707 Redwood St', '1984-10-10', 'Female', 1);

CREATE TABLE Account (
    AccountId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    EmployeeId INT,
    UserName VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(50),
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);
INSERT INTO Account (EmployeeId, UserName, Password, Role, IsActive) VALUES
(1, 'johnsmith', 'password1', 'Admin', 1),
(2, 'janejohnson', 'password2', 'Staff', 1),
(3, 'michaelwilliams', 'password3', 'Staff', 1),
(4, 'emilybrown', 'password4', 'Staff', 1),
(5, 'davidjones', 'password5', 'Staff', 1),
(6, 'sophiagarcia', 'password6', 'Staff', 1),
(7, 'jamesmartinez', 'password7', 'Staff', 1),
(8, 'oliviarodriguez', 'password8', 'Staff', 1),
(9, 'danielhernandez', 'password9', 'Staff', 1),
(10, 'avawilson', 'password10', 'Staff', 1);

CREATE TABLE Product (
    ProductId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    StockQuantity INT,
    Price DECIMAL(10, 2),
    Description TEXT,
    IsDiscontinued BIT NOT NULL DEFAULT 0
);

INSERT INTO Product (Name, StockQuantity, Price, Description, IsDiscontinued)
VALUES
('Motor Oil', 100, 15.00, 'High quality motor oil', 0),
('Tire', 50, 200.00, 'Durable tire', 0),
('Air Filter', 200, 5.00, 'High quality air filter', 0),
('Brake Pads', 70, 30.00, 'Safe brake pads', 0),
('Battery', 30, 120.00, 'Long-lasting battery', 0),
('Headlight', 150, 70.00, 'Super bright headlight', 0),
('Windshield Wiper', 100, 8.00, 'Durable windshield wiper', 0),
('Rearview Mirror', 80, 40.00, 'Anti-glare rearview mirror', 0),
('Leather Seat', 20, 500.00, 'Premium leather seat', 0),
('Timing Belt', 90, 15.00, 'Durable timing belt', 0);


CREATE TABLE Service (
    ServiceId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2),
    Description TEXT,
    IsDiscontinued BIT NOT NULL DEFAULT 0
);

INSERT INTO Service (Name, Price, Description, IsDiscontinued)
VALUES
('Car Wash', 100.00, 'Professional car wash service', 0),
('Oil Change', 200.00, 'Quick oil change', 0),
('Brake Repair', 300.00, 'Safe brake repair', 0),
('Tire Replacement', 400.00, 'Quality tire replacement', 0),
('Regular Maintenance', 500.00, 'Periodic car maintenance', 0),
('Comprehensive Check', 600.00, 'Comprehensive car check-up', 0),
('Engine Repair', 700.00, 'Engine repair service', 0),
('Battery Replacement', 800.00, 'Quick battery replacement', 0),
('Headlight Installation', 900.00, 'Super bright headlight installation', 0),
('Air Conditioning Repair', 1000.00, 'Air conditioning repair service', 0);


CREATE TABLE Orders (
    OrderId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    TransactionNo VARCHAR(50) NOT NULL UNIQUE,
    Date DATE,
    Status BIT NOT NULL DEFAULT 0, -- 0 for Pending, 1 for Completed
    TotalPrice DECIMAL(10, 2),
    VehicleId INT,
    CustomerId INT,
    EmployeeId INT,
    FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

INSERT INTO Orders (TransactionNo, Date, Status, TotalPrice, VehicleId, CustomerId, EmployeeId) VALUES
('202407010001', '2024-07-01', 1, 200.00, 1, 1, 1),
('202407010002', '2024-07-01', '1', 300.00, 2, 2, 2),
('202407020001', '2024-07-02', '0', 400.00, 3, 3, 3),
('202407030001', '2024-07-03', '1', 500.00, 4, 4, 4),
('202407040001', '2024-07-04', '1', 600.00, 5, 5, 5),
('202407050001', '2024-07-05', '0', 700.00, 6, 6, 6),
('202407060001', '2024-07-06', '1', 800.00, 7, 7, 7),
('202407070001', '2024-07-07', '0', 900.00, 8, 8, 8),
('202407080001', '2024-07-08', '0', 1000.00, 9, 9, 9),
('202407090001', '2024-07-09', '1', 1100.00, 10, 10, 10);

CREATE TABLE OrderProducts (
    OrderProductsId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    OrderId INT,
    ProductId INT,
    Quantity INT,
    UnitPrice DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO OrderProducts (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1, 1, 2, 15.00),
(1, 2, 1, 200.00),
(2, 3, 3, 5.00),
(2, 4, 1, 30.00),
(3, 5, 2, 120.00),
(3, 6, 1, 70.00),
(4, 7, 4, 8.00),
(4, 8, 1, 40.00),
(5, 9, 1, 500.00),
(5, 10, 2, 15.00),
(6, 1, 2, 15.00),
(6, 2, 1, 200.00),
(7, 3, 3, 5.00),
(7, 4, 1, 30.00),
(8, 5, 2, 120.00),
(8, 6, 1, 70.00),
(9, 7, 4, 8.00),
(9, 8, 1, 40.00),
(10, 9, 1, 500.00),
(10, 10, 2, 15.00);

CREATE TABLE OrderServices (
    OrderServiceId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    OrderId INT,
    ServiceId INT,
    UnitPrice DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId)
);

INSERT INTO OrderServices (OrderId, ServiceId, UnitPrice) VALUES
(1, 1, 100.00),
(2, 2, 200.00),
(3, 3, 300.00),
(4, 4, 400.00),
(5, 5, 500.00),
(6, 6, 600.00),
(7, 7, 700.00),
(8, 8, 800.00),
(9, 9, 900.00),
(10, 10, 1000.00);

CREATE TABLE CostOfGood (
    CostOfGoodId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    ProductId INT,
    Cost DECIMAL(10, 2),
    Date DATE,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO CostOfGood (ProductId, Cost, Date) VALUES
(1, 10.00, '2024-07-01'),
(2, 150.00, '2024-07-01'),
(3, 3.00, '2024-07-01'),
(4, 20.00, '2024-07-01'),
(5, 80.00, '2024-07-01'),
(6, 50.00, '2024-07-01'),
(7, 5.00, '2024-07-01'),
(8, 25.00, '2024-07-01'),
(9, 350.00, '2024-07-01'),
(10, 10.00, '2024-07-01');
