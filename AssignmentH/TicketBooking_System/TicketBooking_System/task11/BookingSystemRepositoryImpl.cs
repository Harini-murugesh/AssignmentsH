// File: dao/BookingSystemRepositoryImpl.cs
// ==========================

using task11.entity;
using task11.util;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Data;

namespace task11.dao
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
    {
        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Customer (customer_name, email, phone_number) VALUES (@name, @mail, @phone)", conn);
                cmd.Parameters.AddWithValue("@name", customer.customer_name);
                cmd.Parameters.AddWithValue("@mail", customer.email);
                cmd.Parameters.AddWithValue("@phone", customer.phone_number);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        customer_id = (int)reader["customer_id"],
                        customer_name = reader["customer_name"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = reader["phone_number"].ToString()
                    });
                }
            }
            return customers;
        }
        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE customer_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", customerId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddVenue(Venue venue)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Venue WHERE venue_id = @id", conn);
                checkCmd.Parameters.AddWithValue("@id", venue.venue_id);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Venue ID already exists. Choose a different ID.");
                    return;
                }
                SqlCommand cmd = new SqlCommand(
            "INSERT INTO Venue (venue_id, venue_name, address) VALUES (@id, @name, @addr)", conn);

                cmd.Parameters.AddWithValue("@id", venue.venue_id);      // Manually provided
                cmd.Parameters.AddWithValue("@name", venue.venue_name);
                cmd.Parameters.AddWithValue("@addr", venue.address);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"Venue added with ID: {venue.venue_id}");
            }
        }


        public List<Venue> GetAllVenues()
        {
            List<Venue> venues = new List<Venue>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Venue", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    venues.Add(new Venue
                    {
                        venue_id = (int)reader["venue_id"],
                        venue_name = reader["venue_name"].ToString(),
                        address = reader["address"].ToString()
                    });
                }
                reader.Close();
            }
            return venues;
        }

        public void DeleteVenue(int venueId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Venue WHERE venue_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", venueId);
                cmd.ExecuteNonQuery();
            }
        }
        public void AddEvent(Event e)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) VALUES (@name, @date, @time, @venue, @total, @avail, @price, @type)", conn);
                cmd.Parameters.AddWithValue("@name", e.event_name);
                cmd.Parameters.AddWithValue("@date", e.event_date);
                cmd.Parameters.AddWithValue("@time", e.event_time);
                cmd.Parameters.AddWithValue("@venue", e.venue_id);
                cmd.Parameters.AddWithValue("@total", e.total_seats);
                cmd.Parameters.AddWithValue("@avail", e.available_seats);
                cmd.Parameters.AddWithValue("@price", e.ticket_price);
                cmd.Parameters.AddWithValue("@type", e.GetEventType());
                cmd.ExecuteNonQuery();
            }
        }

        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Event", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string type = reader["event_type"].ToString();
                    Event e = type switch
                    {
                        "Movie" => new Movie(),
                        "Concert" => new Concert(),
                        "Sports" => new Sports(),
                        _ => null
                    };
                    if (e != null)
                    {
                        e.event_id = (int)reader["event_id"];
                        e.event_name = reader["event_name"].ToString();
                        e.event_date = DateTime.Parse(reader["event_date"].ToString());
                        e.event_time = TimeSpan.Parse(reader["event_time"].ToString());
                        e.venue_id = (int)reader["venue_id"];
                        e.total_seats = (int)reader["total_seats"];
                        e.available_seats = (int)reader["available_seats"];
                        e.ticket_price = Convert.ToDecimal(reader["ticket_price"]);
                        events.Add(e);
                    }
                }
            }
            return events;
        }
        public void DeleteEvent(int eventId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Event WHERE event_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", eventId);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddBooking(Booking booking)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Booking (customer_id, event_id, num_tickets, total_cost, booking_date) VALUES (@cust, @event, @tick, @cost, @date)", conn);
                cmd.Parameters.AddWithValue("@cust", booking.customer_id);
                cmd.Parameters.AddWithValue("@event", booking.event_id);
                cmd.Parameters.AddWithValue("@tick", booking.num_tickets);
                cmd.Parameters.AddWithValue("@cost", booking.total_cost);
                cmd.Parameters.AddWithValue("@date", booking.booking_date);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Booking", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(new Booking
                    {
                        booking_id = (int)reader["booking_id"],
                        customer_id = (int)reader["customer_id"],
                        event_id = (int)reader["event_id"],
                        num_tickets = (int)reader["num_tickets"],
                        total_cost = (decimal)reader["total_cost"],
                        booking_date = DateTime.Parse(reader["booking_date"].ToString())
                    });
                }
            }
            return bookings;
        }
        public void DeleteBooking(int bookingId)
        {
            using (SqlConnection conn = DBUtil.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Booking WHERE booking_id = @id", conn);
                cmd.Parameters.AddWithValue("@id", bookingId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
