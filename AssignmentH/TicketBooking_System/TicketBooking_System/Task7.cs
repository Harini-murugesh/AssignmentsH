using System;
using System.Collections.Generic;

namespace TicketBookingApp
{
    class Venue
    {
        public string VenueName { get; set; }
        public string Address { get; set; }

        public Venue() { }
        public Venue(string venueName, string address)
        {
            VenueName = venueName;
            Address = address;
        }
        public void DisplayVenueDetails()
        {
            Console.WriteLine($"Venue: {VenueName} | Address: {Address}");
        }
    }

    abstract class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public Venue Venue { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

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

        public bool BookTickets(int numTickets)
        {
            if (numTickets <= AvailableSeats)
            {
                AvailableSeats -= numTickets;
                return true;
            }
            Console.WriteLine("Not enough tickets available!");
            return false;
        }

        public void CancelBooking(int numTickets)
        {
            AvailableSeats += numTickets;
            if (AvailableSeats > TotalSeats)
                AvailableSeats = TotalSeats;
        }

        public decimal CalculateTotalRevenue()
        {
            return (TotalSeats - AvailableSeats) * TicketPrice;
        }

        public abstract void DisplayEventDetails();
    }

    class Movie : Event
    {
        public string Genre { get; set; }
        public string LeadActor { get; set; }

        public Movie(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice, string genre, string leadActor)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Movie")
        {
            Genre = genre;
            LeadActor = leadActor;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName} | Genre: {Genre} | Lead Actor: {LeadActor}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats} | Ticket Price: {TicketPrice:C}\n");
        }
    }

    class Concert : Event
    {
        public string Artist { get; set; }
        public Concert(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice, string artist)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Concert")
        {
            Artist = artist;
        }
        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName} | Artist: {Artist}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats} | Ticket Price: {TicketPrice:C}\n");
        }
    }

    class Sports : Event
    {
        public string SportName { get; set; }
        public Sports(string eventName, DateTime eventDate, string eventTime, Venue venue, int totalSeats, decimal ticketPrice, string sportName)
            : base(eventName, eventDate, eventTime, venue, totalSeats, ticketPrice, "Sports")
        {
            SportName = sportName;
        }
        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sports Event: {EventName} | Sport: {SportName}");
            Console.WriteLine($"Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {Venue.VenueName}");
            Console.WriteLine($"Available Seats: {AvailableSeats} | Ticket Price: {TicketPrice:C}\n");
        }
    }

    class Customer
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Customer(string customerName, string email, string phoneNumber)
        {
            CustomerName = customerName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    class Booking
    {
        private static int idCounter = 1;
        public int BookingId { get; private set; }
        public List<Customer> Customers { get; private set; }
        public Event EventBooked { get; private set; }
        public int NumTickets { get; private set; }
        public decimal TotalCost { get; private set; }
        public DateTime BookingDate { get; private set; }

        public Booking(List<Customer> customers, Event eventBooked, int numTickets)
        {
            BookingId = idCounter++;
            Customers = customers;
            EventBooked = eventBooked;
            NumTickets = numTickets;
            TotalCost = numTickets * eventBooked.TicketPrice;
            BookingDate = DateTime.Now;
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingId} | Event: {EventBooked.EventName} | Tickets: {NumTickets}");
            Console.WriteLine($"Total Cost: {TotalCost:C} | Booking Date: {BookingDate}");
            Console.WriteLine("Customers:");
            foreach (var c in Customers)
                Console.WriteLine($"- {c.CustomerName}, {c.Email}, {c.PhoneNumber}");
        }
    }

    class BookingSystem
    {
        private List<Event> events = new List<Event>();
        private List<Booking> bookings = new List<Booking>();

        public Event CreateEvent(string eventName, string date, string time, int totalSeats, float ticketPrice, string eventType, Venue venue)
        {
            DateTime eventDate = DateTime.Parse(date);
            Event newEvent = null;

            switch (eventType.ToLower())
            {
                case "movie":
                    newEvent = new Movie(eventName, eventDate, time, venue, totalSeats, (decimal)ticketPrice, "Genre", "Actor");
                    break;
                case "concert":
                    newEvent = new Concert(eventName, eventDate, time, venue, totalSeats, (decimal)ticketPrice, "Artist");
                    break;
                case "sports":
                    newEvent = new Sports(eventName, eventDate, time, venue, totalSeats, (decimal)ticketPrice, "Sport");
                    break;
            }

            if (newEvent != null)
            {
                events.Add(newEvent);
                Console.WriteLine("Event created successfully!\n");
            }
            return newEvent;
        }

        public void BookTickets(string eventName, int numTickets, List<Customer> arrayOfCustomer)
        {
            Event eventToBook = events.Find(e => e.EventName == eventName);
            if (eventToBook != null && eventToBook.BookTickets(numTickets))
            {
                Booking newBooking = new Booking(arrayOfCustomer, eventToBook, numTickets);
                bookings.Add(newBooking);
                Console.WriteLine("Booking successful!");
                newBooking.DisplayBookingDetails();
            }
            else
            {
                Console.WriteLine("Booking failed. Event not found or insufficient tickets.");
            }
        }

        public void CancelBooking(int bookingId)
        {
            Booking booking = bookings.Find(b => b.BookingId == bookingId);
            if (booking != null)
            {
                booking.EventBooked.CancelBooking(booking.NumTickets);
                bookings.Remove(booking);
                Console.WriteLine("Booking canceled successfully.");
            }
            else
            {
                Console.WriteLine("Booking ID not found.");
            }
        }

        public int GetAvailableNoOfTickets(string eventName)
        {
            Event e = events.Find(ev => ev.EventName == eventName);
            return e?.AvailableSeats ?? 0;
        }

        public void GetEventDetails(string eventName)
        {
            Event e = events.Find(ev => ev.EventName == eventName);
            e?.DisplayEventDetails();
        }
    }

    class Task7
    {
        static void Main()
        {
            BookingSystem system = new BookingSystem();
            Venue venue = new Venue("City Hall", "Main Street 101");

            while (true)
            {
                Console.WriteLine("\nAvailable Commands: create_event, book_tickets, cancel_tickets, get_available_seats, get_event_details, exit");
                Console.Write("Enter Command: ");
                string command = Console.ReadLine();

                if (command == "exit") break;

                switch (command)
                {
                    case "create_event":
                        Console.Write("Event Name: "); string name = Console.ReadLine();
                        Console.Write("Date (yyyy-mm-dd): "); string date = Console.ReadLine();
                        Console.Write("Time: "); string time = Console.ReadLine();
                        Console.Write("Seats: "); int seats = int.Parse(Console.ReadLine());
                        Console.Write("Price: "); float price = float.Parse(Console.ReadLine());
                        Console.Write("Type (movie/concert/sports): "); string type = Console.ReadLine();
                        system.CreateEvent(name, date, time, seats, price, type, venue);
                        break;

                    case "book_tickets":
                        Console.Write("Event Name: "); string ename = Console.ReadLine();
                        Console.Write("Number of Tickets: "); int count = int.Parse(Console.ReadLine());
                        List<Customer> customers = new List<Customer>();
                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine($"Enter details for ticket #{i + 1}");
                            Console.Write("Name: "); string cname = Console.ReadLine();
                            Console.Write("Email: "); string email = Console.ReadLine();
                            Console.Write("Phone: "); string phone = Console.ReadLine();
                            customers.Add(new Customer(cname, email, phone));
                        }
                        system.BookTickets(ename, count, customers);
                        break;

                    case "cancel_tickets":
                        Console.Write("Enter Booking ID: ");
                        int id = int.Parse(Console.ReadLine());
                        system.CancelBooking(id);
                        break;

                    case "get_available_seats":
                        Console.Write("Event Name: ");
                        string eventForSeats = Console.ReadLine();
                        int available = system.GetAvailableNoOfTickets(eventForSeats);
                        Console.WriteLine($"Available Tickets: {available}");
                        break;

                    case "get_event_details":
                        Console.Write("Event Name: "); string eventForDetails = Console.ReadLine();
                        system.GetEventDetails(eventForDetails);
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}
