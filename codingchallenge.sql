create DATABASE coding;
go
CREATE TABLE Vehicle (
    vehicleID INT PRIMARY KEY,
    make VARCHAR(50) NOT NULL,
    model VARCHAR(50) NOT NULL,
    year INT NOT NULL,
    dailyRate DECIMAL(10,2) NOT NULL,
    status VARCHAR(20) CHECK (status IN ('available', 'notAvailable')),
    passengerCapacity INT NOT NULL,
    engineCapacity INT NOT NULL
);

CREATE TABLE Customer (
    customerID INT PRIMARY KEY,
    firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phoneNumber VARCHAR(20) UNIQUE NOT NULL
);

CREATE TABLE Lease (
    leaseID INT PRIMARY KEY,
    vehicleID INT,
    customerID INT,
    startDate DATE NOT NULL,
    endDate DATE NOT NULL,
    leaseType VARCHAR(20) CHECK (leaseType IN ('Daily', 'Monthly')),
    FOREIGN KEY (vehicleID) REFERENCES Vehicle(vehicleID),
    FOREIGN KEY (customerID) REFERENCES Customer(customerID)
);

CREATE TABLE Payment (
    paymentID INT PRIMARY KEY,
    leaseID INT,
    paymentDate DATE NOT NULL,
    amount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (leaseID) REFERENCES Lease(leaseID) ON DELETE CASCADE
);

-- Insert data into Vehicle table
INSERT INTO Vehicle (vehicleID, make, model, year, dailyRate, status, passengerCapacity, engineCapacity)
VALUES
(1, 'Toyota', 'Camry', 2022, 50.00, 'available', 4, 1450),
(2, 'Honda', 'Civic', 2023, 45.00, 'available', 7, 1500),
(3, 'Ford', 'Focus', 2022, 48.00, 'notAvailable', 4, 1400),
(4, 'Nissan', 'Altima', 2023, 52.00, 'available', 7, 1200),
(5, 'Chevrolet', 'Malibu', 2022, 47.00, 'available', 4, 1800),
(6, 'Hyundai', 'Sonata', 2023, 49.00, 'notAvailable', 7, 1400),
(7, 'BMW', '3 Series', 2023, 60.00, 'available', 7, 2499),
(8, 'Mercedes', 'C-Class', 2022, 58.00, 'available', 8, 2599),
(9, 'Audi', 'A4', 2022, 55.00, 'notAvailable', 4, 2500),
(10, 'Lexus', 'ES', 2023, 54.00, 'available', 4, 2500);

-- Insert data into Customer table
INSERT INTO Customer (customerID, firstName, lastName, email, phoneNumber)
VALUES
(1, 'John', 'Doe', 'johndoe@example.com', '555-555-5555'),
(2, 'Jane', 'Smith', 'janesmith@example.com', '555-123-4567'),
(3, 'Robert', 'Johnson', 'robert@example.com', '555-789-1234'),
(4, 'Sarah', 'Brown', 'sarah@example.com', '555-456-7890'),
(5, 'David', 'Lee', 'david@example.com', '555-987-6543'),
(6, 'Laura', 'Hall', 'laura@example.com', '555-234-5678'),
(7, 'Michael', 'Davis', 'michael@example.com', '555-876-5432'),
(8, 'Emma', 'Wilson', 'emma@example.com', '555-432-1098'),
(9, 'William', 'Taylor', 'william@example.com', '555-321-6547'),
(10, 'Olivia', 'Adams', 'olivia@example.com', '555-765-4321');

-- Insert data into Lease table
INSERT INTO Lease (leaseID, vehicleID, customerID, startDate, endDate, leaseType)
VALUES
(1, 1, 1, '2023-01-01', '2023-01-05', 'Daily'),
(2, 2, 2, '2023-02-15', '2023-02-28', 'Monthly'),
(3, 3, 3, '2023-03-10', '2023-03-15', 'Daily'),
(4, 4, 4, '2023-04-20', '2023-04-30', 'Monthly'),
(5, 5, 5, '2023-05-05', '2023-05-10', 'Daily'),
(6, 4, 3, '2023-06-15', '2023-06-30', 'Monthly'),
(7, 7, 7, '2023-07-01', '2023-07-10', 'Daily'),
(8, 8, 8, '2023-08-12', '2023-08-15', 'Monthly'),
(9, 3, 3, '2023-09-07', '2023-09-10', 'Daily'),
(10, 10, 10, '2023-10-10', '2023-10-31', 'Monthly');

-- Insert data into Payment table
INSERT INTO Payment (paymentID, leaseID, paymentDate, amount)
VALUES
(1, 1, '2023-01-03', 200.00),
(2, 2, '2023-02-20', 1000.00),
(3, 3, '2023-03-12', 75.00),
(4, 4, '2023-04-25', 900.00),
(5, 5, '2023-05-07', 60.00),
(6, 6, '2023-06-18', 1200.00),
(7, 7, '2023-07-03', 40.00),
(8, 8, '2023-08-14', 1100.00),
(9, 9, '2023-09-09', 80.00),
(10, 10, '2023-10-25', 1500.00);




SELECT * FROM Payment
select * from Vehicle
select * from Customer
select * from Lease

--Q1
UPDATE Vehicle
SET dailyRate = 68
WHERE make = 'Mercedes';

select * from Vehicle


--Q2
DELETE FROM Payment WHERE leaseID IN (SELECT leaseID FROM Lease WHERE customerID = 1);
DELETE FROM Lease WHERE customerID = 1;
DELETE FROM Customer WHERE customerID = 1;
SELECT * FROM Customer;
SELECT * FROM Lease;
SELECT * FROM Payment;

--Q3
EXEC sp_rename 'dbo.Payment.paymentDate', 'transactionDate', 'COLUMN';
select * from Payment

--Q4
SELECT * FROM Customer WHERE email = 'janesmith@example.com';

--Q5
SELECT * FROM Lease 
WHERE customerID = 5
AND endDate >= GETDATE();

--Q6
SELECT P.paymentID, P.leaseID, P.transactionDate, P.amount, 
       C.customerID, C.firstName, C.lastName, C.phoneNumber 
FROM Payment P
JOIN Lease L ON P.leaseID = L.leaseID
JOIN Customer C ON L.customerID = C.customerID
WHERE C.phoneNumber = '555-123-4567'; 

--Q7
SELECT AVG(dailyRate) AS avgDailyRate
FROM Vehicle
WHERE status = 'available';
--Q8
SELECT TOP 1 *
FROM Vehicle
ORDER BY dailyRate DESC;

--Q9
SELECT * FROM Vehicle V
WHERE EXISTS (
    SELECT 1 FROM Lease L 
    WHERE L.vehicleID = V.vehicleID 
    AND L.customerID = 2
);


--Q10
SELECT TOP 1 * FROM Lease 
ORDER BY startDate DESC;

--Q11
SELECT * FROM Payment 
WHERE YEAR(transactionDate) = 2023;

--Q12
SELECT *
FROM Customer C
WHERE NOT EXISTS (
    SELECT 1 
    FROM Lease L
    JOIN Payment P ON L.leaseID = P.leaseID
    WHERE L.customerID = C.customerID
);
--Q13
SELECT V.vehicleID, V.make, V.model, V.year, V.dailyRate,
       (SELECT SUM(P.amount) 
        FROM Payment P 
        JOIN Lease L ON P.leaseID = L.leaseID
        WHERE L.vehicleID = V.vehicleID) AS totalPayments
FROM Vehicle V
ORDER BY totalPayments DESC;


--Q14
SELECT C.customerID, C.firstName,SUM(P.amount) AS totalPayments
FROM Customer C
JOIN Lease L ON C.customerID = L.customerID
JOIN Payment P ON L.leaseID = P.leaseID
GROUP BY C.customerID, C.firstName, C.lastName;

--Q15
SELECT *
FROM Lease L
JOIN Vehicle V ON L.vehicleID = V.vehicleID
ORDER BY L.leaseID;

--Q16
SELECT *
FROM Lease L
JOIN Customer C ON L.customerID = C.customerID
JOIN Vehicle V ON L.vehicleID = V.vehicleID
WHERE L.endDate >= GETDATE()
ORDER BY L.startDate;

--Q17
SELECT TOP 1 C.customerID, C.firstName, C.lastName, SUM(P.amount) AS totalSpent
FROM Customer C
JOIN Lease L ON C.customerID = L.customerID
JOIN Payment P ON L.leaseID = P.leaseID
GROUP BY C.customerID, C.firstName, C.lastName
ORDER BY totalSpent DESC;


--Q18
SELECT *
FROM Vehicle V
LEFT JOIN Lease L ON V.vehicleID = L.vehicleID
LEFT JOIN Customer C ON L.customerID = C.customerID;
















select * from Payment






