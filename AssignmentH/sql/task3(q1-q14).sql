--TASK 3
--Q1
SELECT event_name, AVG(ticket_price) AS avg_ticket_price 
FROM Event 
GROUP BY event_name;


--Q2
SELECT e.event_name, SUM(b.total_cost) AS total_revenue
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name;


--Q3
SELECT TOP 1 e.event_name, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name
ORDER BY total_tickets_sold DESC;



--Q4
SELECT e.event_name, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name;



--Q5
SELECT e.event_name
FROM Event e
LEFT JOIN Booking b ON e.event_id = b.event_id
WHERE b.event_id IS NULL;



--Q6
SELECT TOP 1 c.customer_name, SUM(b.num_tickets) AS total_tickets_booked
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name
ORDER BY total_tickets_booked DESC;



--Q7
SELECT MONTH(booking_date) AS month, e.event_name, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY MONTH(booking_date), e.event_name
ORDER BY month;

--Q8
SELECT v.venue_name, AVG(e.ticket_price) AS avg_ticket_price
FROM Event e
JOIN Venue v ON e.venue_id = v.venue_id
GROUP BY v.venue_name;


--Q9
SELECT e.event_type, SUM(b.num_tickets) AS total_tickets_sold
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_type;



--Q10
SELECT YEAR(e.event_date) AS event_year, SUM(b.num_tickets * e.ticket_price) AS total_revenue
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY YEAR(e.event_date)
ORDER BY event_year;



--Q11
SELECT c.customer_name, COUNT(DISTINCT b.event_id) AS total_events_booked
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name
HAVING COUNT(DISTINCT b.event_id) > 1;



--Q12
SELECT c.customer_name, SUM(b.total_cost) AS total_revenue
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name;


--Q13
SELECT e.event_type, v.venue_name, AVG(e.ticket_price) AS avg_ticket_price
FROM Event e
JOIN Venue v ON e.venue_id = v.venue_id
GROUP BY e.event_type, v.venue_name;





--Q14
SELECT c.customer_name, SUM(b.num_tickets) AS total_tickets_purchased
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
WHERE b.booking_date >= DATEADD(DAY, -30, GETDATE())
GROUP BY c.customer_name;

