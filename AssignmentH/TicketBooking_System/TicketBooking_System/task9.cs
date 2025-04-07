// ====================== ExceptionValue Namespace ======================
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExceptionValue
{
    // Venue class
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

    // Event class (abstract)
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

    // Event subclasses
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

    // Customer class
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

    // Booking class
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

    // Task 9 - Q1, Q2, Q3. Custom Exceptions
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(string message) : base(message) { }
    }

    public class InvalidBookingIDException : Exception
    {
        public InvalidBookingIDException(string message) : base(message) { }
    }

    // Service Layer
    public interface IEventServiceProvider
    {
        Event CreateEvent(string name, string date, string time, int totalSeats, decimal price, string type, Venue venue);
        List<Event> GetEventDetails();
        int GetAvailableNoOfTickets();
    }

    public interface IBookingSystemServiceProvider
    {
        decimal CalculateBookingCost(int numTickets, decimal ticketPrice);
        Booking BookTickets(string eventName, int numTickets, Customer customer);
        bool CancelBooking(int bookingId);
        Booking GetBookingDetails(int bookingId);
    }

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
                _ => throw new ArgumentException("Invalid event type")
            };
            events.Add(evt);
            return evt;
        }

        public List<Event> GetEventDetails() => events;

        public int GetAvailableNoOfTickets() => events.Sum(e => e.AvailableSeats);
    }

    public class BookingSystemServiceProviderImpl : EventServiceProviderImpl, IBookingSystemServiceProvider
    {
        private List<Booking> bookings = new();

        public decimal CalculateBookingCost(int numTickets, decimal ticketPrice) => numTickets * ticketPrice;

        public Booking BookTickets(string eventName, int numTickets, Customer customer)
        {
            Event evt = events.Find(e => e.EventName.Trim().Equals(eventName.Trim(), StringComparison.OrdinalIgnoreCase));
            if (evt == null)
                throw new EventNotFoundException($"Event '{eventName}' not found.");
            if (evt.AvailableSeats < numTickets)
                return null;

            evt.AvailableSeats -= numTickets;
            Booking booking = new Booking(customer, evt, numTickets);
            bookings.Add(booking);
            return booking;
        }

        public bool CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
                throw new InvalidBookingIDException($"Booking ID '{bookingId}' not found.");
            booking.Event.AvailableSeats += booking.NumTickets;
            bookings.Remove(booking);
            return true;
        }

        public Booking GetBookingDetails(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking == null)
                throw new InvalidBookingIDException($"Booking ID '{bookingId}' not found.");
            return booking;
        }
    }

    // Task 9 - Q1, Q2, Q3. App Layer: TicketBookingSystem Class
    class Task9
    {
        static void Main(string[] args)
        {
            BookingSystemServiceProviderImpl system = new();
            while (true)
            {
                try
                {
                    Console.WriteLine("\n1. create_event\n2. book_tickets\n3. cancel_tickets\n4. get_available_seats\n5. get_event_details\n6. exit");
                    Console.Write("Enter choice (1-6): ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter name, date(yyyy-mm-dd), time, seats, price, type: ");
                            string[] parts = Console.ReadLine().Split(',');
                            if (parts.Length != 6) { Console.WriteLine("Invalid input. Please provide all 6 details."); break; }
                            Venue venue = new Venue("City Arena", "Downtown");
                            var result = system.CreateEvent(parts[0].Trim(), parts[1].Trim(), parts[2].Trim(), int.Parse(parts[3]), decimal.Parse(parts[4]), parts[5].Trim(), venue);
                            Console.WriteLine("Event created successfully!");
                            break;

                        case "2":
                            Console.Write("Enter event name: ");
                            string ename = Console.ReadLine();
                            Console.Write("Number of tickets: ");
                            int ntickets = int.Parse(Console.ReadLine());
                            Console.Write("Customer name, email, phone: ");
                            string[] custParts = Console.ReadLine().Split(',');
                            if (custParts.Length != 3) { Console.WriteLine("Invalid customer details."); break; }
                            Customer cust = new Customer(custParts[0], custParts[1], custParts[2]);
                            Booking booking = system.BookTickets(ename, ntickets, cust);
                            Console.WriteLine(booking != null ? $"Booking successful! Booking ID: {booking.BookingId}" : "Not enough tickets available.");
                            break;

                        case "3":
                            Console.Write("Enter booking ID: ");
                            if (int.TryParse(Console.ReadLine(), out int bid))
                                Console.WriteLine(system.CancelBooking(bid) ? "Booking cancelled successfully." : "Cancellation failed.");
                            else
                                Console.WriteLine("Invalid booking ID format.");
                            break;

                        case "4":
                            Console.WriteLine($"Total available seats: {system.GetAvailableNoOfTickets()}");
                            break;

                        case "5":
                            foreach (var e in system.GetEventDetails())
                                e.DisplayEventDetails();
                            break;

                        case "6":
                            Console.WriteLine("Thank you for using the Ticket Booking System!");
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (EventNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (InvalidBookingIDException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Null value encountered. Please ensure all inputs are correct.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}
