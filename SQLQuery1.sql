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

