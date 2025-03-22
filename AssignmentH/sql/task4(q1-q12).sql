--TASK 4
--Q1
SELECT v.venue_name, 
       (SELECT AVG(e.ticket_price) 
        FROM Event e 
        WHERE e.venue_id = v.venue_id) AS avg_ticket_price
FROM Venue v;

--Q2
SELECT event_name 
FROM Event 
WHERE (total_seats - available_seats) > (total_seats * 0.5);

--Q3
SELECT e.event_name, 
       (SELECT SUM(b.num_tickets) 
        FROM Booking b 
        WHERE b.event_id = e.event_id) AS total_tickets_sold
FROM Event e;

--Q4
SELECT c.customer_name 
FROM Customer c 
WHERE NOT EXISTS (
    SELECT 1 
    FROM Booking b 
    WHERE b.customer_id = c.customer_id
);

--Q5
SELECT event_name 
FROM Event 
WHERE event_id NOT IN (
    SELECT DISTINCT event_id FROM Booking
);

--Q6
SELECT event_type, SUM(tickets_sold) AS total_tickets_sold
FROM (
    SELECT e.event_type, 
           (SELECT SUM(b.num_tickets) 
            FROM Booking b 
            WHERE b.event_id = e.event_id
           ) AS tickets_sold
    FROM Event e
) AS event_summary
GROUP BY event_type;

--Q7
SELECT event_name, ticket_price 
FROM Event 
WHERE ticket_price > (SELECT AVG(ticket_price) FROM Event);

--Q8
SELECT c.customer_name, 
       (SELECT SUM(b.total_cost) 
        FROM Booking b 
        WHERE b.customer_id = c.customer_id) AS total_revenue
FROM Customer c;

--Q9
SELECT DISTINCT c.customer_name 
FROM Customer c 
WHERE c.customer_id IN (
    SELECT b.customer_id 
    FROM Booking b 
    WHERE b.event_id IN (
        SELECT event_id 
        FROM Event 
        WHERE venue_id = (SELECT venue_id FROM Venue WHERE venue_name = 'Chennai Stadium')
    )
);
--q10
SELECT e.event_type, 
       (SELECT SUM(b.num_tickets) 
        FROM Booking b 
        JOIN Event e2 ON b.event_id = e2.event_id 
        WHERE e2.event_type = e.event_type
       ) AS total_tickets_sold
FROM (SELECT DISTINCT event_type FROM Event) e;

--Q11
SELECT c.customer_name, 
       FORMAT(b.booking_date, 'yyyy-MM') AS booking_month
FROM Customer c
JOIN Booking b ON c.customer_id = b.customer_id
WHERE FORMAT(b.booking_date, 'yyyy-MM') IN (
    SELECT DISTINCT FORMAT(booking_date, 'yyyy-MM') FROM Booking
);


--Q12
SELECT v.venue_name, 
       (SELECT AVG(e.ticket_price) 
        FROM Event e 
        WHERE e.venue_id = v.venue_id) AS avg_ticket_price
FROM Venue v;
