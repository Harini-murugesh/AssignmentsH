using System;
using System.Collections.Generic;
using Task4Namespace;  //Import Task 4 classes

namespace Task5Namespace
{
    //Movie Class (inherits from Event)
    public class Movie : Event
    {
        public string Genre { get; set; }
        public string LeadActor { get; set; }
        public string LeadActress { get; set; }

        public Movie(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string genre, string leadActor, string leadActress)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Movie")
        {
            Genre = genre;
            LeadActor = leadActor;
            LeadActress = leadActress;
        }

        public override void DisplayEventDetails()
        {
            base.DisplayEventDetails();
            Console.WriteLine($"Genre: {Genre} | Lead Actor: {LeadActor} | Lead Actress: {LeadActress}\n");
        }
    }

    // Concert Class (inherits from Event)
    public class Concert : Event
    {
        public string Artist { get; set; }
        public string Genre { get; set; }

        public Concert(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string artist, string genre)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Concert")
        {
            Artist = artist;
            Genre = genre;
        }

        public override void DisplayEventDetails()
        {
            base.DisplayEventDetails();
            Console.WriteLine($"Artist: {Artist} | Genre: {Genre}\n");
        }
    }

    // Sports Class (inherits from Event)
    public class Sports : Event
    {
        public string SportName { get; set; }
        public string Teams { get; set; }

        public Sports(string eventName, DateTime eventDate, string eventTime, string venueName, int totalSeats, decimal ticketPrice, string sportName, string teams)
            : base(eventName, eventDate, eventTime, venueName, totalSeats, ticketPrice, "Sports")
        {
            SportName = sportName;
            Teams = teams;
        }

        public override void DisplayEventDetails()
        {
            base.DisplayEventDetails();
            Console.WriteLine($"Sport: {SportName} | Teams: {Teams}\n");
        }
    }

    
    public class Task5
    {
        public static void DisplayEventDetails(Event e)
        {
            e.DisplayEventDetails(); 
        }

       public static void Main()
        {
            List<Event> events = new List<Event>();

            // Adding different event types
            events.Add(new Movie("Avengers", DateTime.Now.AddDays(5), "6:00 PM", "IMAX", 100, 12.00m, "Action", "Robert Downey Jr.", "Scarlett Johansson"));
            events.Add(new Concert("pradeepKumar Live", DateTime.Now.AddDays(7), "8:00 PM", "sky Garden", 200, 75.00m, "Pradeep", "Melody/pop"));
            events.Add(new Sports("Football Finals", DateTime.Now.AddDays(3), "5:00 PM", "National Stadium", 150, 45.00m, "Football", "India vs Argentina"));

            Console.WriteLine("\n Available Events:");
            for (int i = 0; i < events.Count; i++)
            {
                Console.WriteLine($"[{i}] {events[i].EventName} ({events[i].EventType})");
            }

            // User selects an event
            Console.Write("\nEnter event number to book tickets: ");
            int choice = int.Parse(Console.ReadLine());
            Console.Write("Enter number of tickets to book: ");
            int numTickets = int.Parse(Console.ReadLine());

            // Booking process
            Booking booking = new Booking(events[choice]);
            if (booking.book_tickets(numTickets))
            {
                Console.WriteLine($" Booking successful! Total cost: {booking.TotalCost:C}");
            }
            else
            {
                Console.WriteLine("Booking failed! Not enough tickets available.");
            }

            //  Call the polymorphic method here
            Console.WriteLine("\n Updated Event Details:");
            DisplayEventDetails(events[choice]);

            Console.WriteLine($"Available Tickets: {booking.GetAvailableNoOfTickets()}");
            Console.WriteLine($"Total Booked Tickets: {events[choice].GetBookedNoOfTickets()}");
            Console.WriteLine($"Total Revenue: {events[choice].CalculateTotalRevenue():C}");
        }
    }

}
