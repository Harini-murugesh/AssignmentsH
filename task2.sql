USE TicketBookingSystem;
GO
-- Insert into Event table
INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type, booking_id) 
VALUES
('Rock Concert', '2025-04-15', '18:30:00', 2, 5000, 2000, 1500, 'Concert', 101),
('Football Cup Final', '2025-05-10', '20:00:00', 1, 30000, 5000, 2000, 'Sports', 102),
('Movie Premiere', '2025-06-05', '19:00:00', 5, 250, 100, 800, 'Movie', 103),
('Jazz Night', '2025-07-21', '21:00:00', 2, 2000, 500, 1200, 'Concert', 104),
('Theater Drama', '2025-08-10', '18:00:00', 3, 500, 200, 900, 'Concert', 105),
('Basketball Finals', '2025-09-12', '19:30:00', 1, 20000, 7000, 2500, 'Sports', 106),
('Opera Night', '2025-10-25', '20:00:00', 4, 1200, 600, 3000, 'Concert', 107),
('Comedy Show', '2025-11-18', '17:30:00', 6, 1500, 700, 1100, 'Concert', 108),
('Boxing Championship', '2025-12-05', '21:30:00', 7, 10000, 2000, 1800, 'Sports', 109),
('World Cup Final', '2026-01-22', '19:45:00', 1, 50000, 10000, 3500, 'Sports', 110);


-- Insert into Customer table
INSERT INTO Customer (customer_name, email, phone_number, booking_id) 
VALUES
('Anand', 'anand@example.com', '987654000', 101),
('Bala', 'bala@example.com', '9123456789', 102),
('Chandra', 'chandra@example.com', '9234567890', 103),
('Devi', 'devi@example.com', '9345678901', 104),
('Eswar', 'eswar@example.com', '9456789012', 105),
('Kumar', 'kumar@example.com', '9567890123', 106),
('Lakshmi', 'lakshmi@example.com', '9678901234', 107),
('Murali', 'murali@example.com', '9789012345', 108),
('Nandhini', 'nandhini@example.com', '9890123456', 109),
('Priya', 'priya@example.com', '9901234567', 110);

--booking table values
INSERT INTO Booking (booking_id, customer_id, event_id, num_tickets, total_cost, booking_date) 
VALUES
(101, 1, 3, 3, 6000, '2025-03-10'), 
(102, 2, 4, 2, 1800, '2025-03-11'),  
(103, 3, 5, 4, 4400, '2025-03-12'),  
(104, 4, 6, 1, 1500, '2025-03-13'),  
(105, 5, 7, 2, 1600, '2025-03-14'),  
(106, 6, 8, 5, 12500, '2025-03-15'), 
(107, 7, 9, 2, 6000, '2025-03-16'), 
(108, 8, 10, 3, 3600, '2025-03-17'),
(109, 9, 3, 6, 10800, '2025-03-18'),
(110, 10, 5, 4, 14000, '2025-03-19');
 
--2
SELECT * FROM Event;
--3
SELECT * FROM Event WHERE available_seats > 0;
--4
SELECT * FROM Event WHERE event_name LIKE '%cup%';
--5
SELECT * FROM Event WHERE ticket_price >= 1000 AND ticket_price <= 2500;

SELECT * FROM Event WHERE event_date BETWEEN '2025-06-01' AND '2025-09-30';

SELECT * FROM Event 
WHERE available_seats > 0 
AND event_name LIKE '%Concert%';

SELECT * FROM Customer 
ORDER BY customer_id 
OFFSET 5 ROWS 
FETCH NEXT 5 ROWS ONLY;


SELECT * FROM Booking WHERE num_tickets > 4;

SELECT * FROM Customer WHERE phone_number LIKE '%000';

SELECT * FROM Event WHERE total_seats > 15000 ORDER BY total_seats DESC;

SELECT * FROM Event WHERE event_name NOT LIKE 'X%' 
AND event_name NOT LIKE 'Y%' 
AND event_name NOT LIKE 'Z%';
