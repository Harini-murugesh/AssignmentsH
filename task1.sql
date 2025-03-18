CREATE DATABASE TicketBookingSystem;
USE TicketBookingSystem;

-- Create Venue_Table
CREATE TABLE Venue (
    venue_id INT PRIMARY KEY,
    venue_name VARCHAR(100) NOT NULL,
    address VARCHAR(255) NOT NULL
);

-- Create Event Table
CREATE TABLE Event (
    event_id INT PRIMARY KEY,
    event_name VARCHAR(100) NOT NULL,
    event_date DATE NOT NULL,
    event_time TIME NOT NULL,
    venue_id INT NOT NULL REFERENCES Venue(venue_id),
    total_seats INT NOT NULL,
    available_seats INT NOT NULL,
    ticket_price DECIMAL(10,2) NOT NULL,
    event_type VARCHAR(50) NOT NULL CHECK (event_type IN ('Movie', 'Sports', 'Concert')),
    booking_id INT REFERENCES Booking(booking_id) 
);

-- Create Customer Table
CREATE TABLE Customer (
    customer_id INT PRIMARY KEY,
    customer_name NVARCHAR(30) NOT NULL,
    email nVARCHAR(30) NOT NULL,
    phone_number VARCHAR(20) NOT NULL,
booking_id INT REFERENCES Booking(booking_id)
);

-- Create Booking Table
CREATE TABLE Booking (
    booking_id INT PRIMARY KEY,
    customer_id INT REFERENCES Customer(customer_id),
    event_id INT REFERENCES Event(event_id),
    num_tickets INT NOT NULL,
    total_cost DECIMAL(10,2) NOT NULL,
    booking_date DATETIME NOT NULL DEFAULT GETDATE()
);
