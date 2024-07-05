CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY not null identity(1,1),
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Address VARCHAR(255),
    Points INT
);

CREATE TABLE Vehicle (
    VehicleId INT PRIMARY KEY not null identity(1,1),
    CustomerId INT,
    LicensePlate VARCHAR(50),
    Make VARCHAR(255),
    Model VARCHAR(255),
    Year INT,
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

CREATE TABLE Employee (
    EmployeeId INT PRIMARY KEY not null identity(1,1),
    Name VARCHAR(255) NOT NULL,
    Phone VARCHAR(50),
    Address VARCHAR(255),
    DateOfBirth DATE,
    Gender VARCHAR(10)
);

CREATE TABLE Account (
    AccountId INT not null identity(1,1),
    EmployeeId INT,
    UserName VARCHAR(50) NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(50),
    PRIMARY KEY (AccountId, EmployeeId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

CREATE TABLE Product (
    ProductId INT PRIMARY KEY not null identity(1,1),
    Name VARCHAR(255) NOT NULL,
    StockQuantity INT,
    Price DECIMAL(10, 2),
    Description TEXT
);

CREATE TABLE Service (
    ServiceId INT PRIMARY KEY not null identity(1,1),
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2),
    Description TEXT
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY not null identity(1,1),
    TransactionNo VARCHAR(50),
    UnitPrice DECIMAL(10, 2),
    Date DATE,
    Status VARCHAR(50),
    Quantity INT,
    TotalPrice DECIMAL(10, 2),
    VehicleId INT,
    CustomerId INT,
    EmployeeId INT,
    ProductId INT,
    ServiceId INT,
    FOREIGN KEY (VehicleId) REFERENCES Vehicle(VehicleId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId)
);

CREATE TABLE OrderProduct (
    OrderProductId INT PRIMARY KEY not null identity(1,1),
    OrderId INT,
    ProductId INT,
    UnitPrice DECIMAL(10, 2),
    Quantity INT,
    TotalPrice DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

CREATE TABLE OrderService (
    OrderServiceId INT PRIMARY KEY not null identity(1,1),
    OrderId INT,
    ServiceId INT,
    EmployeeId INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ServiceId) REFERENCES Service(ServiceId),
    FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

CREATE TABLE CostOfGood (
    CostOfGoodId INT PRIMARY KEY not null identity(1,1),
    ProductId INT,
    ProductName VARCHAR(255),
    UnitPrice DECIMAL(10, 2),
    TotalPrice DECIMAL(10, 2),
    Quantity INT,
    Date DATE,
    FOREIGN KEY (ProductId) REFERENCES Product(ProductId)
);

INSERT INTO Customer (Name, Phone, Address, Points) VALUES
('John Doe', '123-456-7890', '123 Main St', 100),
('Jane Smith', '987-654-3210', '456 Elm St', 150),
('Jim Brown', '555-555-5555', '789 Oak St', 200);

INSERT INTO Vehicle (CustomerId, LicensePlate, Make, Model, Year) VALUES
(1, 'ABC123', 'Toyota', 'Camry', 2020),
(2, 'XYZ789', 'Honda', 'Civic', 2019),
(3, 'LMN456', 'Ford', 'Fusion', 2018);

INSERT INTO Employee (Name, Phone, Address, DateOfBirth, Gender) VALUES
('Alice Johnson', '222-222-2222', '321 Maple St', '1990-01-01', 'Female'),
('Bob Williams', '333-333-3333', '654 Pine St', '1985-05-05', 'Male'),
('Carol Davis', '444-444-4444', '987 Birch St', '1975-12-12', 'Female');

INSERT INTO Account (EmployeeId, UserName, Password, Role) VALUES
(1, 'alicej', 'password1', 'Manager'),
(2, 'bobw', 'password2', 'Sales'),
(3, 'carold', 'password3', 'Technician');

INSERT INTO Product (Name, StockQuantity, Price, Description) VALUES
('Oil Filter', 50, 15.99, 'High quality oil filter'),
('Air Filter', 30, 12.99, 'Durable air filter'),
('Brake Pads', 20, 35.99, 'Reliable brake pads');

INSERT INTO Service (Name, Price, Description) VALUES
('Oil Change', 29.99, 'Complete oil change service'),
('Tire Rotation', 19.99, 'Tire rotation service'),
('Brake Inspection', 24.99, 'Comprehensive brake inspection');

INSERT INTO Orders (TransactionNo, UnitPrice, Date, Status, Quantity, TotalPrice, VehicleId, CustomerId, EmployeeId, ProductId, ServiceId) VALUES
('T001', 15.99, '2023-01-10', 'Completed', 2, 31.98, 1, 1, 1, 1, 1),
('T002', 19.99, '2023-02-15', 'Completed', 1, 19.99, 2, 2, 2, 2, 2),
('T003', 35.99, '2023-03-20', 'Pending', 1, 35.99, 3, 3, 3, 3, 3);

INSERT INTO OrderProduct (OrderId, ProductId, UnitPrice, Quantity, TotalPrice) VALUES
(1, 1, 15.99, 2, 31.98),
(2, 2, 12.99, 1, 12.99),
(3, 3, 35.99, 1, 35.99);

INSERT INTO OrderService (OrderId, ServiceId, EmployeeId, Price) VALUES
(1, 1, 1, 29.99),
(2, 2, 2, 19.99),
(3, 3, 3, 24.99);

INSERT INTO CostOfGood (ProductId, ProductName, UnitPrice, TotalPrice, Quantity, Date) VALUES
(1, 'Oil Filter', 10.00, 100.00, 10, '2023-01-01'),
(2, 'Air Filter', 8.00, 80.00, 10, '2023-01-15'),
(3, 'Brake Pads', 20.00, 200.00, 10, '2023-02-01');