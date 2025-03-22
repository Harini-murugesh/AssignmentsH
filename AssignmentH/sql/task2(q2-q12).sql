--task2
--Q2
SELECT * FROM Event

--Q3
SELECT * 
FROM Event 
WHERE available_seats > 0;

--Q4
SELECT * 
FROM Event 
WHERE event_name LIKE '%cup%';

--Q5
SELECT * 
FROM Event 
WHERE ticket_price BETWEEN 1000 AND 2500;


--Q6
SELECT * 
FROM Event 
WHERE event_date BETWEEN '2025-05-01' AND '2025-12-31';


--Q7
SELECT * 
FROM Event 
WHERE available_seats > 0 AND event_name LIKE '%Concert%';


--Q8
SELECT * 
FROM Customer 
ORDER BY customer_id 
OFFSET 5 ROWS 
FETCH NEXT 5 ROWS ONLY;

--Q9
SELECT * 
FROM Booking 
WHERE num_tickets > 4;

--Q10
SELECT * 
FROM Customer 
WHERE phone_number LIKE '%000';

--Q11
SELECT * 
FROM Event 
WHERE total_seats > 15000 
ORDER BY total_seats DESC;

--Q12
SELECT * 
FROM Event 
WHERE event_name NOT LIKE 'x%' 
AND event_name NOT LIKE 'y%' 
AND event_name NOT LIKE 'z%';
