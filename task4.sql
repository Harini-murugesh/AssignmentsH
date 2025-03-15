SELECT v.venue_name, 
       (SELECT AVG(e.ticket_price) 
        FROM Event e 
        WHERE e.venue_id = v.venue_id) AS avg_ticket_price
FROM Venue v;

SELECT event_name
FROM Event
WHERE event_id IN (
    SELECT event_id
    FROM Booking
    GROUP BY event_id
    HAVING SUM(num_tickets) > (SELECT total_seats / 2 FROM Event WHERE Event.event_id = Booking.event_id)
);

SELECT event_name, 
       (SELECT COALESCE(SUM(num_tickets), 0) 
        FROM Booking 
        WHERE Booking.event_id = Event.event_id) AS total_tickets_sold
FROM Event;


SELECT customer_name
FROM Customer c
WHERE NOT EXISTS (
    SELECT 1 FROM Booking b WHERE b.customer_id = c.customer_id
);

SELECT event_name
FROM Event
WHERE event_id NOT IN (
    SELECT DISTINCT event_id FROM Booking
);

SELECT e.event_type, ticket_sales.total_tickets_sold
FROM (
    SELECT event_id, SUM(num_tickets) AS total_tickets_sold
    FROM Booking
    GROUP BY event_id
) ticket_sales
JOIN Event e ON e.event_id = ticket_sales.event_id
GROUP BY e.event_type, ticket_sales.total_tickets_sold;

SELECT event_name, ticket_price
FROM Event
WHERE ticket_price > (SELECT AVG(ticket_price) FROM Event);

SELECT c.customer_name, 
       (SELECT SUM(b.total_cost) 
        FROM Booking b 
        WHERE b.customer_id = c.customer_id) AS total_revenue
FROM Customer c;

SELECT customer_name
FROM Customer
WHERE customer_id IN (
    SELECT DISTINCT b.customer_id
    FROM Booking b
    JOIN Event e ON b.event_id = e.event_id
    WHERE e.venue_id = (SELECT venue_id FROM Venue WHERE venue_name = 'Stadium A')
);

SELECT e.event_type, SUM(b.num_tickets) AS total_tickets_sold
FROM Event e
JOIN Booking b ON e.event_id = b.event_id
GROUP BY e.event_type;

SELECT DISTINCT c.customer_name
FROM Customer c
WHERE c.customer_id IN (
    SELECT DISTINCT b.customer_id
    FROM Booking b
    WHERE FORMAT(b.booking_date, 'yyyy-MM') = '2025-03'
);

SELECT v.venue_name, 
       (SELECT AVG(ticket_price) FROM Event e WHERE e.venue_id = v.venue_id) AS avg_ticket_price
FROM Venue v;
