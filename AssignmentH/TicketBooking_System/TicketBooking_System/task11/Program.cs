// ==========================
// File: main/Program.cs
// ==========================

using task11.dao;
using task11.entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;

namespace task11.main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IBookingSystemRepository repo = new BookingSystemRepositoryImpl();

            while (true)
            {
                Console.WriteLine("\n--- Ticket Booking System ---");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. View Customers");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. Add Venue");
                Console.WriteLine("5. View Venues");
                Console.WriteLine("6. Delete Venue");
                Console.WriteLine("7. Add Event");
                Console.WriteLine("8. View Events");
                Console.WriteLine("9. Delete Event");
                Console.WriteLine("10. Book Ticket");
                Console.WriteLine("11. View Bookings");
                Console.WriteLine("12. Delete Booking");
                Console.WriteLine("13. Exit");
                
               
               



                Console.Write("Choose an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Customer c = new Customer();
                        Console.Write("Enter customer name: ");
                        c.customer_name = Console.ReadLine();
                        Console.Write("Enter email: ");
                        c.email = Console.ReadLine();
                        Console.Write("Enter phone number: ");
                        c.phone_number = Console.ReadLine();
                        repo.AddCustomer(c);
                        Console.WriteLine("Customer added.");
                        break;

                    case "2":
                        foreach (var cust in repo.GetAllCustomers())
                            Console.WriteLine($"{cust.customer_id} - {cust.customer_name} - {cust.email} - {cust.phone_number}");
                        break;
                    case "3":
                        Console.Write("Enter customer ID to delete: ");
                        int custIdToDelete = int.Parse(Console.ReadLine());
                        repo.DeleteCustomer(custIdToDelete);
                        Console.WriteLine("Customer deleted.");
                        break;

                    case "4":
                       
                        Console.WriteLine("Enter Venue ID:");
                        int id = int.Parse(Console.ReadLine());
                        Console.Write("Enter venue name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter address: ");
                       string address = Console.ReadLine();
                        Venue v = new Venue
                        {
                            venue_id = id,
                            venue_name = name,
                            address = address
                        };
                        repo.AddVenue(v);
                        Console.WriteLine("Venue added.");
                        break;

                    case "5":
                        foreach (var venue in repo.GetAllVenues())
                            Console.WriteLine($"{venue.venue_id} - {venue.venue_name} - {venue.address}");
                        break;
                    case "6":
                        Console.Write("Enter Venue ID to delete: ");
                        int delVenueId = int.Parse(Console.ReadLine());
                        repo.DeleteVenue(delVenueId);
                        Console.WriteLine("Venue deleted.");
                        break;

                    case "7":
                        Console.WriteLine("Select Event Type (Movie / Concert / Sports):");
                        string type = Console.ReadLine();
                        Event e = type.ToLower() switch
                        {
                            "movie" => new Movie(),
                            "concert" => new Concert(),
                            "sports" => new Sports(),
                            _ => null
                        };
                        if (e == null)
                        {
                            Console.WriteLine("Invalid event type.");
                            break;
                        }

                        Console.Write("Event Name: ");
                        e.event_name = Console.ReadLine();
                        Console.Write("Event Date (yyyy-mm-dd): ");
                        e.event_date = DateTime.Parse(Console.ReadLine());
                        Console.Write("Event Time (hh:mm): ");
                        e.event_time = TimeSpan.Parse(Console.ReadLine());
                        Console.Write("Venue ID: ");
                        e.venue_id = int.Parse(Console.ReadLine());
                        Console.Write("Total Seats: ");
                        e.total_seats = int.Parse(Console.ReadLine());
                        Console.Write("Available Seats: ");
                        e.available_seats = int.Parse(Console.ReadLine());
                        Console.Write("Ticket Price: ");
                        e.ticket_price = decimal.Parse(Console.ReadLine());

                        repo.AddEvent(e);
                        Console.WriteLine("Event added.");
                        break;

                    case "8":
                        foreach (var ev in repo.GetAllEvents())
                            Console.WriteLine($"{ev.event_id} - {ev.event_name} - {ev.event_date.ToShortDateString()} {ev.event_time} - {ev.GetEventType()}");
                        break;
                    case "9":
                        Console.Write("Enter Event ID to delete: ");
                        int delEventId = int.Parse(Console.ReadLine());
                        repo.DeleteEvent(delEventId);
                        Console.WriteLine("Event deleted.");
                        break;

                    case "10":
                        Booking b = new Booking();
                        Console.Write("Customer ID: ");
                        b.customer_id = int.Parse(Console.ReadLine());
                        Console.Write("Event ID: ");
                        b.event_id = int.Parse(Console.ReadLine());
                        Console.Write("Number of Tickets: ");
                        b.num_tickets = int.Parse(Console.ReadLine());
                        Console.Write("Total Cost: ");
                        b.total_cost = decimal.Parse(Console.ReadLine());
                        b.booking_date = DateTime.Now;
                        repo.AddBooking(b);
                        Console.WriteLine("Booking done.");
                        break;

                    case "11":
                        foreach (var bk in repo.GetAllBookings())
                            Console.WriteLine($"{bk.booking_id} - Customer ID: {bk.customer_id} - Event ID: {bk.event_id} - Tickets: {bk.num_tickets} - Cost: {bk.total_cost} - Date: {bk.booking_date}");
                        break;
                    case "12":
                        Console.Write("Enter Booking ID to delete: ");
                        int delBookingId = int.Parse(Console.ReadLine());
                        repo.DeleteBooking(delBookingId);
                        Console.WriteLine("Booking deleted.");
                        break;

                    case "13":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
