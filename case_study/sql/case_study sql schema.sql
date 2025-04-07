-- Create the Vehicle table
CREATE TABLE Vehicle (
    vehicleID INT PRIMARY KEY IDENTITY(1,1),
    make NVARCHAR(50) NOT NULL,
    model NVARCHAR(50) NOT NULL,
    year INT NOT NULL,
    dailyRate DECIMAL(10, 2) NOT NULL,
    status NVARCHAR(20) CHECK (status IN ('available', 'notAvailable')) NOT NULL,
    passengerCapacity INT NOT NULL,
    engineCapacity FLOAT NOT NULL
);

-- Create the Customer table
CREATE TABLE Customer (
    customerID INT PRIMARY KEY IDENTITY(1,1),
    firstName NVARCHAR(50) NOT NULL,
    lastName NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    phoneNumber NVARCHAR(20) NOT NULL
);

-- Create the Lease table
CREATE TABLE Lease (
    leaseID INT PRIMARY KEY IDENTITY(1,1),
    vehicleID INT REFERENCES Vehicle(vehicleID) NOT NULL,
    customerID INT REFERENCES Customer(customerID) NOT NULL,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    type NVARCHAR(20) CHECK (type IN ('DailyLease', 'MonthlyLease')) NOT NULL,
    CONSTRAINT CK_EndDate CHECK (endDate >= startDate)
);

-- Create the Payment table
CREATE TABLE Payment (
    paymentID INT PRIMARY KEY IDENTITY(1,1),
    leaseID INT REFERENCES Lease(leaseID) NOT NULL,
    paymentDate DATE NOT NULL,
    amount DECIMAL(10, 2) NOT NULL
);
