use TicketBookingSystem
go
INTO Venue (venue_id, venue_name, address) VALUES
(1, 'Chennai Stadium', 'Chennai'),
(2, 'Coimbatore Hall', 'Coimbatore'),
(3, 'Madurai Theater', 'Madurai'),
(4, 'Trichy Arena', 'Trichy'),
(5, 'Salem Cinema', 'Salem'),
(6, 'Tirunelveli Expo', 'Tirunelveli'),
(7, 'Vellore Sports Complex', 'Vellore'),
(8, 'Erode Convention Center', 'Erode'),
(9, 'Thanjavur Open Air', 'Thanjavur'),
(10, 'Kanyakumari Club', 'Kanyakumari');

INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) 
VALUES
('Rock Concert', '2025-04-15', '18:30:00', 2, 5000, 2000, 1500.00, 'Concert'),
('Football Cup Final', '2025-05-10', '20:00:00', 1, 30000, 5000, 2000.00, 'Sports'),
('Movie Premiere', '2025-06-05', '19:00:00', 5, 250, 100, 800.00, 'Movie'),
('Jazz Night', '2025-07-21', '21:00:00', 2, 2000, 500, 1200.00, 'Concert'),
('Theater Drama', '2025-08-10', '18:00:00', 3, 500, 200, 900.00, 'Concert'),
('Basketball Finals', '2025-09-12', '19:30:00', 1, 20000, 7000, 2500.00, 'Sports'),
('Opera Night', '2025-10-25', '20:00:00', 4, 1200, 600, 3000.00, 'Concert'),
('Comedy Show', '2025-11-18', '17:30:00', 6, 1500, 700, 1100.00, 'Concert'),
('Boxing Championship', '2025-12-05', '21:30:00', 7, 10000, 2000, 1800.00, 'Sports'),
('World Cup Final', '2026-01-22', '19:45:00', 1, 50000, 10000, 3500.00, 'Sports');

INSERT INTO Customer (customer_name, email, phone_number) VALUES
('Arun', 'arun@email.com', '9876543210'),
('Priya', 'priya@email.com', '9823456789'),
('Vikram', 'vikram@email.com', '9765432109'),
('Divya', 'divya@email.com', '9856123478'),
('Rahul', 'rahul@email.com', '9745632187'),
('Sneha', 'sneha@email.com', '9632587410'),
('Karan', 'karan@email.com', '9521478523'),
('Ananya', 'ananya@email.com', '9412365478'),
('Rohit', 'rohit@email.com', '9301254789'),
('Ash', 'ash@email.com', '9192837465'); 

INSERT INTO Booking (customer_id, event_id, num_tickets, total_cost, booking_date) VALUES
(11, 13, 2, 3000.00, '2025-04-10'),
(12, 14, 4, 8000.00, '2025-05-05'),
(13, 15, 1, 800.00, '2025-06-02'),
(14, 16, 3, 3600.00, '2025-07-15'),
(15, 17, 5, 4500.00, '2025-08-08'),
(16, 18, 2, 5000.00, '2025-09-10'),
(17, 19, 1, 3000.00, '2025-10-20'),
(18, 20, 6, 6600.00, '2025-11-15'),
(19, 21, 4, 7200.00, '2025-12-02'),
(20, 22, 8, 28000.00, '2026-01-18');
