use TicketBookingSystem
go
--task 1(question 1,2,4)
--venue table
CREATE TABLE Venue (
    venue_id INT IDENTITY(1,1) PRIMARY KEY,
    venue_name VARCHAR(255) NOT NULL,
    address VARCHAR(255) NOT NULL
);


--event table
CREATE TABLE Event (
    event_id INT IDENTITY(1,1) PRIMARY KEY, 
    event_name VARCHAR(255) NOT NULL, 
    event_date DATE NOT NULL, 
    event_time TIME NOT NULL, 
    venue_id INT NOT NULL REFERENCES Venue(venue_id), 
    total_seats INT NOT NULL, 
    available_seats INT NOT NULL, 
    ticket_price DECIMAL(10,2) NOT NULL, 
    event_type VARCHAR(50) CHECK (event_type IN ('Movie', 'Sports', 'Concert'))
);


--cutomer table
CREATE TABLE Customer (
    customer_id INT IDENTITY(11,1) PRIMARY KEY, 
    customer_name VARCHAR(255) NOT NULL, 
    email VARCHAR(255) UNIQUE NOT NULL, 
    phone_number VARCHAR(15) UNIQUE NOT NULL
);


--booking table
CREATE TABLE Booking (
    booking_id INT IDENTITY(101,1) PRIMARY KEY, 
    customer_id INT NOT NULL REFERENCES Customer(customer_id), 
    event_id INT NOT NULL REFERENCES Event(event_id), 
    num_tickets INT NOT NULL, 
    total_cost DECIMAL(10,2) NOT NULL, 
    booking_date DATE NOT NULL
);

