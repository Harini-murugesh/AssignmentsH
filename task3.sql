SELECT event_name, AVG(ticket_price) AS avg_ticket_price
FROM Event
GROUP BY event_name;


SELECT e.event_name, SUM(b.total_cost) AS total_revenue
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name;


SELECT TOP 1 e.event_name, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name
ORDER BY total_tickets_sold DESC;


SELECT e.event_name, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name;


SELECT e.event_name
FROM Event e
LEFT JOIN Booking b ON e.event_id = b.event_id
WHERE b.event_id IS NULL;

SELECT TOP 1 c.customer_name, SUM(b.num_tickets) AS total_tickets
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name
ORDER BY total_tickets DESC;

SELECT 
    FORMAT(b.booking_date, 'yyyy-MM') AS booking_month, 
    e.event_name, 
    SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY FORMAT(b.booking_date, 'yyyy-MM'), e.event_name
ORDER BY booking_month;

SELECT v.venue_name, AVG(e.ticket_price) AS avg_ticket_price
FROM Event e
JOIN Venue v ON e.venue_id = v.venue_id
GROUP BY v.venue_name;

SELECT e.event_type, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_type;


SELECT 
    YEAR(b.booking_date) AS booking_year, 
    SUM(b.total_cost) AS total_revenue
FROM Booking b
GROUP BY YEAR(b.booking_date)
ORDER BY booking_year;

SELECT c.customer_name, COUNT(DISTINCT b.event_id) AS event_count
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name
HAVING COUNT(DISTINCT b.event_id) > 1;

SELECT c.customer_name, SUM(b.total_cost) AS total_revenue
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name;

SELECT e.event_type, v.venue_name, AVG(e.ticket_price) AS avg_ticket_price
FROM Event e
JOIN Venue v ON e.venue_id = v.venue_id
GROUP BY e.event_type, v.venue_name;


SELECT c.customer_name, SUM(b.num_tickets) AS total_tickets
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
WHERE b.booking_date >= DATEADD(DAY, -30, GETDATE())
GROUP BY c.customer_name;
