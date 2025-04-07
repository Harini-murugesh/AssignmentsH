// ====================== TicketB Namespace ======================
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketB
{
    // Task 8 - Q1. Venue class
    public class Venue
    {
        public string VenueName { get; set; }
        public string Address { get; set; }

        public Venue() { }
        public Venue(string venueName, string address)
        {
            VenueName = venueName;
            Address = address;
        }
    }

    // Task 8 - Q2. Event class (abstract)
    public abstract class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        public Event() { }

        public Event(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice, string eventType)
        {
            EventName = eventName;
            EventDate = eventDate;
            EventTime = eventTime;
            Venue = venue;
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            TicketPrice = ticketPrice;
            EventType = eventType;
        }

        public abstract void DisplayEventDetails();
    }

    // Task 8 - Q3. Event subclasses
    public class Movie : Event
    {
        public Movie(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Movie") { }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName} | Seats: {AvailableSeats}/{TotalSeats} | Price: {TicketPrice:C}");
        }
    }

    public class Sports : Event
    {
        public Sports(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Sports") { }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sports: {EventName} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName} | Seats: {AvailableSeats}/{TotalSeats} | Price: {TicketPrice:C}");
        }
    }

    public class Concert : Event
    {
        public Concert(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Concert") { }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName} | Seats: {AvailableSeats}/{TotalSeats} | Price: {TicketPrice:C}");
        }
    }

    // Task 8 - Q4. Customer class
    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Customer(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }

    // Task 8 - Q4. Booking class
    public class Booking
    {
        private static int counter = 1000;
        public int BookingId { get; private set; }
        public Event Event { get; private set; }
        public Customer Customer { get; private set; }
        public int NumTickets { get; private set; }
        public decimal TotalCost { get; private set; }

        public Booking(Customer customer, Event evt, int numTickets)
        {
            BookingId = counter++;
            Customer = customer;
            Event = evt;
            NumTickets = numTickets;
            TotalCost = evt.TicketPrice * numTickets;
        }
    }

    // ====================== Task 8 - Q5. Service Layer ======================
    public interface IEventServiceProvider
    {
        Event CreateEvent(string name, string date, string time, int totalSeats, decimal price, string type, Venue venue);
        List<Event> GetEventDetails();
        int GetAvailableNoOfTickets();
    }

    // Task 8 - Q6. Booking Service Interface
    public interface IBookingSystemServiceProvider
    {
        decimal CalculateBookingCost(int numTickets, decimal ticketPrice);
        Booking BookTickets(string eventName, int numTickets, Customer customer);
        bool CancelBooking(int bookingId);
        Booking GetBookingDetails(int bookingId);
    }

    // Task 8 - Q7. EventService Implementation
    public class EventServiceProviderImpl : IEventServiceProvider
    {
        protected List<Event> events = new();

        public Event CreateEvent(string name, string date, string time, int totalSeats, decimal price, string type, Venue venue)
        {
            Event evt = type.ToLower() switch
            {
                "movie" => new Movie(name, DateTime.Parse(date), time, venue, totalSeats, price),
                "sports" => new Sports(name, DateTime.Parse(date), time, venue, totalSeats, price),
                "concert" => new Concert(name, DateTime.Parse(date), time, venue, totalSeats, price),
                _ => null
            };
            if (evt != null) events.Add(evt);
            return evt;
        }

        public List<Event> GetEventDetails() => events;

        public int GetAvailableNoOfTickets() => events.Sum(e => e.AvailableSeats);
    }

    // Task 8 - Q8. BookingSystemServiceProviderImpl
    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, IBookingSystemServiceProvider
    {
        private List<Booking> bookings = new();

        public decimal CalculateBookingCost(int numTickets, decimal ticketPrice) => numTickets * ticketPrice;

        public Booking BookTickets(string eventName, int numTickets, Customer customer)
        {
            Event evt = events.Find(e => e.EventName == eventName);
            if (evt != null && evt.AvailableSeats >= numTickets)
            {
                evt.AvailableSeats -= numTickets;
                Booking booking = new Booking(customer, evt, numTickets);
                bookings.Add(booking);
                return booking;
            }
            Console.WriteLine("Booking failed. Either event not found or insufficient seats.");
            return null;
        }

        public bool CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking != null)
            {
                booking.Event.AvailableSeats += booking.NumTickets;
                bookings.Remove(booking);
                return true;
            }
            Console.WriteLine("Cancellation failed. Booking ID not found.");
            return false;
        }

        public Booking GetBookingDetails(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
                Console.WriteLine("No booking found for the provided ID.");
            return booking;
        }
    }

    // Q9 & Q10. App Layer: TicketBookingSystem Class
    class Task8
    {
        static void Main(string[] args)
        {
            BookingSystemServiceProviderImpl system = new();
            while (true)
            {
                Console.WriteLine("\nCommands: create_event, book_tickets, cancel_tickets, get_available_seats, get_event_details, exit");
                Console.Write("Enter command: ");
                string cmd = Console.ReadLine();

                switch (cmd)
                {
                    case "create_event":
                        Console.Write("Enter name, date(yyyy-mm-dd), time, seats, price, type: ");
                        string[] parts = Console.ReadLine().Split(',');
                        if (parts.Length != 6)
                        {
                            Console.WriteLine("Invalid input. Please provide all 6 details.");
                            break;
                        }
                        Venue venue = new Venue("City chennai", "Anna Nagar");
                        var result = system.CreateEvent(parts[0], parts[1], parts[2], int.Parse(parts[3]), decimal.Parse(parts[4]), parts[5], venue);
                        Console.WriteLine(result != null ? "Event created successfully!" : "Failed to create event. Check type.");
                        break;

                    case "book_tickets":
                        Console.Write("Enter event name: ");
                        string ename = Console.ReadLine();
                        Console.Write("Number of tickets: ");
                        if (!int.TryParse(Console.ReadLine(), out int ntickets))
                        {
                            Console.WriteLine("Invalid number of tickets.");
                            break;
                        }
                        Console.Write("Customer name, email, phone: ");
                        string[] custParts = Console.ReadLine().Split(',');
                        if (custParts.Length != 3)
                        {
                            Console.WriteLine("Invalid customer details.");
                            break;
                        }
                        Customer cust = new Customer(custParts[0], custParts[1], custParts[2]);
                        Booking booking = system.BookTickets(ename, ntickets, cust);
                        Console.WriteLine(booking != null ? $"Booking successful! Booking ID: {booking.BookingId}" : "Booking failed. Event not found or not enough tickets.");
                        break;

                    case "cancel_tickets":
                        Console.Write("Enter booking ID: ");
                        if (int.TryParse(Console.ReadLine(), out int bid))
                            Console.WriteLine(system.CancelBooking(bid) ? "Booking cancelled successfully." : "Cancellation failed. Booking ID not found.");
                        else
                            Console.WriteLine("Invalid booking ID format.");
                        break;

                    case "get_available_seats":
                        Console.WriteLine($"Total available seats: {system.GetAvailableNoOfTickets()}");
                        break;

                    case "get_event_details":
                        foreach (var e in system.GetEventDetails())
                            e.DisplayEventDetails();
                        break;

                    case "exit":
                        Console.WriteLine("Thank you for using the Ticket Booking System!");
                        return;

                    default:
                        Console.WriteLine("Invalid command. Try again.");
                        break;
                }
            }
        }
    }
}
