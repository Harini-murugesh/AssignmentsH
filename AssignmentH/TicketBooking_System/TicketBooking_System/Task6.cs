using System;
using System.Collections.Generic;

namespace TicketBookingSystemNamespace
{
    // Abstract Event Class
    public abstract class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public string VenueName { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; }

        public Event(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string eventType)
        {
            this.EventName = eventName;
            this.EventDate = eventDate;
            this.EventTime = eventTime;
            this.VenueName = venueName;
            this.TotalSeats = totalSeats;
            this.AvailableSeats = totalSeats;
            this.TicketPrice = ticketPrice;
            this.EventType = eventType;
        }

        public abstract void DisplayEventDetails();

        public bool BookTickets(int numTickets)
        {
            if (numTickets <= this.AvailableSeats)
            {
                this.AvailableSeats -= numTickets;
                return true;
            }
            return false;
        }

        public void CancelTickets(int numTickets)
        {
            this.AvailableSeats += numTickets;
            if (this.AvailableSeats > this.TotalSeats)
                this.AvailableSeats = this.TotalSeats;
        }
    }

    // Movie Class
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string LeadActor { get; set; }

        public Movie(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string genre, string leadActor)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Movie")
        {
            this.Genre = genre;
            this.LeadActor = leadActor;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Movie: {EventName} | Genre: {Genre} | Lead Actor: {LeadActor} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {VenueName} | Price: {TicketPrice:C} | Available Seats: {AvailableSeats}");
        }
    }

    // Concert Class
    public class Concert : Event
    {
        public string Artist { get; set; }

        public Concert(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string artist)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Concert")
        {
            this.Artist = artist;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Concert: {EventName} | Artist: {Artist} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {VenueName} | Price: {TicketPrice:C} | Available Seats: {AvailableSeats}");
        }
    }

    // Sport Class
    public class Sport : Event
    {
        public string Teams { get; set; }

        public Sport(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string teams)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Sport")
        {
            this.Teams = teams;
        }

        public override void DisplayEventDetails()
        {
            Console.WriteLine($"Sport: {EventName} | Teams: {Teams} | Date: {EventDate.ToShortDateString()} | Time: {EventTime} | Venue: {VenueName} | Price: {TicketPrice:C} | Available Seats: {AvailableSeats}");
        }
    }

    // Abstract BookingSystem
    public abstract class BookingSystem
    {
        protected List<Event> events = new List<Event>();

        public abstract void CreateEvent(Event e);
        public abstract bool BookTickets(int eventIndex, int numTickets);
        public abstract void CancelTickets(int eventIndex, int numTickets);
        public abstract void DisplayAvailableSeats();
    }

    // TicketBookingSystem (Concrete)
    public class TicketBookingSystem : BookingSystem
    {
        public override void CreateEvent(Event e)
        {
            events.Add(e);
            Console.WriteLine($"Event '{e.EventName}' added successfully!\n");
        }

        public override bool BookTickets(int eventIndex, int numTickets)
        {
            if (eventIndex >= 0 && eventIndex < events.Count)
            {
                if (events[eventIndex].BookTickets(numTickets))
                {
                    Console.WriteLine($"Successfully booked {numTickets} tickets for {events[eventIndex].EventName}!");
                    return true;
                }
                Console.WriteLine("Not enough tickets available.");
            }
            else
            {
                Console.WriteLine("Invalid event index.");
            }
            return false;
        }

        public override void CancelTickets(int eventIndex, int numTickets)
        {
            if (eventIndex >= 0 && eventIndex < events.Count)
            {
                events[eventIndex].CancelTickets(numTickets);
                Console.WriteLine($"{numTickets} tickets canceled for {events[eventIndex].EventName}.");
            }
            else
            {
                Console.WriteLine("Invalid event index.");
            }
        }

        public override void DisplayAvailableSeats()
        {
            Console.WriteLine("\nAvailable Events:");
            for (int i = 0; i < events.Count; i++)
            {
                Console.Write($"[{i}] ");
                events[i].DisplayEventDetails();
            }
        }
    }

    // Main Class
    class Task6
    {
        static void Main()
        {
            TicketBookingSystem system = new TicketBookingSystem();

            // Preload Events
            system.CreateEvent(new Movie("Avengers", DateTime.Now.AddDays(5), "7:00 PM", "IMAX", 100, 12.00m, "Action", "Robert Downey Jr."));
            system.CreateEvent(new Concert("Live with Arijit", DateTime.Now.AddDays(10), "8:00 PM", "Stadium A", 150, 20.00m, "Arijit Singh"));
            system.CreateEvent(new Concert("Pop Night", DateTime.Now.AddDays(12), "6:00 PM", "City Hall", 120, 18.50m, "Taylor Swift"));
            system.CreateEvent(new Sport("Cricket Clash", DateTime.Now.AddDays(15), "4:00 PM", "National Ground", 200, 25.00m, "India vs Australia"));
            system.CreateEvent(new Sport("Football Derby", DateTime.Now.AddDays(18), "5:30 PM", "Arena X", 180, 22.00m, "Team A vs Team B"));

            Console.WriteLine("🎟️ Welcome to the Ticket Booking System!");

            while (true)
            {
                Console.WriteLine("\nCommands: create_event, book_tickets, cancel_tickets, get_available_seats, exit");
                Console.Write("Enter command: ");
                string command = Console.ReadLine().ToLower();

                if (command == "create_event")
                {
                    Console.Write("Enter event type (Movie/Concert/Sport): ");
                    string type = Console.ReadLine();

                    Console.Write("Event name: ");
                    string name = Console.ReadLine();

                    Console.Write("Event date (yyyy-mm-dd): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());

                    Console.Write("Event time (e.g., 7:00 PM): ");
                    string time = Console.ReadLine();

                    Console.Write("Venue: ");
                    string venue = Console.ReadLine();

                    Console.Write("Total seats: ");
                    int seats = int.Parse(Console.ReadLine());

                    Console.Write("Ticket price: ");
                    decimal price = decimal.Parse(Console.ReadLine());

                    if (type.ToLower() == "movie")
                    {
                        Console.Write("Genre: ");
                        string genre = Console.ReadLine();
                        Console.Write("Lead Actor: ");
                        string actor = Console.ReadLine();
                        system.CreateEvent(new Movie(name, date, time, venue, seats, price, genre, actor));
                    }
                    else if (type.ToLower() == "concert")
                    {
                        Console.Write("Artist: ");
                        string artist = Console.ReadLine();
                        system.CreateEvent(new Concert(name, date, time, venue, seats, price, artist));
                    }
                    else if (type.ToLower() == "sport")
                    {
                        Console.Write("Teams: ");
                        string teams = Console.ReadLine();
                        system.CreateEvent(new Sport(name, date, time, venue, seats, price, teams));
                    }
                    else
                    {
                        Console.WriteLine("Invalid event type.");
                    }
                }
                else if (command == "book_tickets")
                {
                    system.DisplayAvailableSeats();
                    Console.Write("Enter event number: ");
                    int index = int.Parse(Console.ReadLine());
                    Console.Write("Number of tickets to book: ");
                    int count = int.Parse(Console.ReadLine());
                    system.BookTickets(index, count);
                }
                else if (command == "cancel_tickets")
                {
                    system.DisplayAvailableSeats();
                    Console.Write("Enter event number: ");
                    int index = int.Parse(Console.ReadLine());
                    Console.Write("Number of tickets to cancel: ");
                    int count = int.Parse(Console.ReadLine());
                    system.CancelTickets(index, count);
                }
                else if (command == "get_available_seats")
                {
                    system.DisplayAvailableSeats();
                }
                else if (command == "exit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command.");
                }
            }
        }
    }
}
