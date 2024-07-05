CREATE DATABASE CarWash;

USE CarWash;

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

-- Table Employee
CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Address VARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(10)
);

INSERT INTO Employee (Name, Phone, Address, DateOfBirth, Gender) VALUES
('John Smith', '0912345671', '123 Main St', '1980-01-01', 'Male'),
('Jane Johnson', '0987654322', '456 Oak St', '1985-02-02', 'Female'),
('Michael Williams', '0901234568', '789 Pine St', '1990-03-03', 'Male'),
('Emily Brown', '0934567891', '101 Maple St', '1995-04-04', 'Female'),
('David Jones', '0945678902', '202 Cedar St', '2000-05-05', 'Male'),
('Sophia Garcia', '0923456790', '303 Birch St', '1982-06-06', 'Female'),
('James Martinez', '0919876544', '404 Elm St', '1987-07-07', 'Male'),
('Olivia Rodriguez', '0908765433', '505 Aspen St', '1992-08-08', 'Female'),
('Daniel Hernandez', '0987654321', '606 Spruce St', '1997-09-09', 'Male'),
('Ava Wilson', '0912345672', '707 Redwood St', '1984-10-10', 'Female');

CREATE TABLE Account (
    AccountId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    EmployeeId INT,
    UserName VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(50),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

INSERT INTO Account (EmployeeId, UserName, Password, Role) VALUES
(1, 'johnsmith', 'password1', 'Admin'),
(2, 'janejohnson', 'password2', 'Staff'),
(3, 'michaelwilliams', 'password3', 'Staff'),
(4, 'emilybrown', 'password4', 'Staff'),
(5, 'davidjones', 'password5', 'Staff'),
(6, 'sophiagarcia', 'password6', 'Staff'),
(7, 'jamesmartinez', 'password7', 'Staff'),
(8, 'oliviarodriguez', 'password8', 'Staff'),
(9, 'danielhernandez', 'password9', 'Staff'),
(10, 'avawilson', 'password10', 'Staff');

CREATE TABLE Product (
    ProductId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    StockQuantity INT,
    Price DECIMAL(10, 2),
    Description TEXT
);

INSERT INTO Product (Name, StockQuantity, Price, Description) VALUES
('Motor Oil', 100, 15.00, 'High quality motor oil'),
('Tire', 50, 200.00, 'Durable tire'),
('Air Filter', 200, 5.00, 'High quality air filter'),
('Brake Pads', 70, 30.00, 'Safe brake pads'),
('Battery', 30, 120.00, 'Long-lasting battery'),
('Headlight', 150, 70.00, 'Super bright headlight'),
('Windshield Wiper', 100, 8.00, 'Durable windshield wiper'),
('Rearview Mirror', 80, 40.00, 'Anti-glare rearview mirror'),
('Leather Seat', 20, 500.00, 'Premium leather seat'),
('Timing Belt', 90, 15.00, 'Durable timing belt');

CREATE TABLE Service (
    ServiceId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2),
    Description TEXT
);

INSERT INTO Service (Name, Price, Description) VALUES
('Car Wash', 100.00, 'Professional car wash service'),
('Oil Change', 200.00, 'Quick oil change'),
('Brake Repair', 300.00, 'Safe brake repair'),
('Tire Replacement', 400.00, 'Quality tire replacement'),
('Regular Maintenance', 500.00, 'Periodic car maintenance'),
('Comprehensive Check', 600.00, 'Comprehensive car check-up'),
('Engine Repair', 700.00, 'Engine repair service'),
('Battery Replacement', 800.00, 'Quick battery replacement'),
('Headlight Installation', 900.00, 'Super bright headlight installation'),
('Air Conditioning Repair', 1000.00, 'Air conditioning repair service');

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    TransactionNo VARCHAR(50) NOT NULL,
    UnitPrice DECIMAL(10, 2),
    Date DATE,
    Status VARCHAR(50),
    Quantity INT,
    TotalPrice DECIMAL(10, 2),
    VehicleId INT,
    CustomerId INT,
    EmployeeId INT,
    FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

INSERT INTO Orders (TransactionNo, UnitPrice, Date, Status, Quantity, TotalPrice, VehicleId, CustomerId, EmployeeId) VALUES
('TXN001', 200.00, '2024-07-01', 'Completed', 1, 200.00, 1, 1, 1),
('TXN002', 300.00, '2024-07-02', 'Completed', 1, 300.00, 2, 2, 2),
('TXN003', 400.00, '2024-07-03', 'Pending', 1, 400.00, 3, 3, 3),
('TXN004', 500.00, '2024-07-04', 'Completed', 1, 500.00, 4, 4, 4),
('TXN005', 600.00, '2024-07-05', 'Completed', 1, 600.00, 5, 5, 5),
('TXN006', 700.00, '2024-07-06', 'Pending', 1, 700.00, 6, 6, 6),
('TXN007', 800.00, '2024-07-07', 'Completed', 1, 800.00, 7, 7, 7),
('TXN008', 900.00, '2024-07-08', 'Pending', 1, 900.00, 8, 8, 8),
('TXN009', 1000.00, '2024-07-09', 'Pending', 1, 1000.00, 9, 9, 9),
('TXN010', 1100.00, '2024-07-10', 'Completed', 1, 1100.00, 10, 10, 10);

CREATE TABLE OrderService (
    OrderServiceId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    OrderId INT,
    ServiceId INT,
    EmployeeId INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

INSERT INTO OrderService (OrderId, ServiceId, EmployeeId, Price) VALUES
(1, 1, 1, 100.00),
(2, 2, 2, 200.00),
(3, 3, 3, 300.00),
(4, 4, 4, 400.00),
(5, 5, 5, 500.00),
(6, 6, 6, 600.00),
(7, 7, 7, 700.00),
(8, 8, 8, 800.00),
(9, 9, 9, 900.00),
(10, 10, 10, 1000.00);

CREATE TABLE OrderProduct (
    OrderProductId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    OrderId INT,
    ProductId INT,
    UnitPrice DECIMAL(10, 2),
    Quantity INT,
    TotalPrice DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO OrderProduct (OrderId, ProductId, UnitPrice, Quantity, TotalPrice) VALUES
(1, 1, 15.00, 1, 15.00),
(2, 2, 200.00, 1, 200.00),
(3, 3, 5.00, 1, 5.00),
(4, 4, 30.00, 1, 30.00),
(5, 5, 120.00, 1, 120.00),
(6, 6, 70.00, 1, 70.00),
(7, 7, 8.00, 1, 8.00),
(8, 8, 40.00, 1, 40.00),
(9, 9, 500.00, 1, 500.00),
(10, 10, 15.00, 1, 15.00);

CREATE TABLE CostOfGood (
    CostOfGoodId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    ProductId INT,
    ProductName VARCHAR(255),
    UnitPrice DECIMAL(10, 2),
    TotalPrice DECIMAL(10, 2),
    Quantity INT,
    Date DATE,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO CostOfGood (ProductId, ProductName, UnitPrice, TotalPrice, Quantity, Date) VALUES
(1, 'Motor Oil', 12.00, 24.00, 2, '2024-07-01'),
(2, 'Tire', 180.00, 180.00, 1, '2024-07-02'),
(3, 'Air Filter', 4.00, 4.00, 1, '2024-07-03'),
(4, 'Brake Pads', 25.00, 25.00, 1, '2024-07-04'),
(5, 'Battery', 100.00, 100.00, 1, '2024-07-05'),
(6, 'Headlight', 50.00, 50.00, 1, '2024-07-06'),
(7, 'Windshield Wiper', 6.00, 12.00, 2, '2024-07-07'),
(8, 'Rearview Mirror', 30.00, 30.00, 1, '2024-07-08'),
(9, 'Leather Seat', 400.00, 400.00, 1, '2024-07-09'),
(10, 'Timing Belt', 10.00, 20.00, 2, '2024-07-10');
