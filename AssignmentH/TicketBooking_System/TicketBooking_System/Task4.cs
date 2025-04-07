using System;
namespace Task4Namespace
{
    // Event Class
    public class Event
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTime { get; set; }
        public string VenueName { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public string EventType { get; set; } // Movie, Sports, Concert

        // Constructor Overloading
        public Event() { }
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

   
        public decimal CalculateTotalRevenue()
        {
            return (this.TotalSeats - this.AvailableSeats) * this.TicketPrice;
        }

        
        public int GetBookedNoOfTickets()
        {
            return this.TotalSeats - this.AvailableSeats;
        }

        // Book tickets
        public bool BookTickets(int numTickets)
        {
            if (numTickets <= this.AvailableSeats)
            {
                this.AvailableSeats -= numTickets;
                return true;
            }
            Console.WriteLine("Not enough tickets available!");
            return false;
        }

        // Cancel booking
        public void CancelBooking(int numTickets)
        {
            this.AvailableSeats += numTickets;
            if (this.AvailableSeats > this.TotalSeats)
                this.AvailableSeats = this.TotalSeats;
        }

        // Display event details
        public virtual void DisplayEventDetails()
        {
            Console.WriteLine($"Event: {this.EventName} | Date: {this.EventDate.ToShortDateString()} | Time: {this.EventTime} | Venue: {this.VenueName}");
            Console.WriteLine($"Total Seats: {this.TotalSeats} | Available Seats: {this.AvailableSeats} | Ticket Price: {this.TicketPrice:C} | Type: {this.EventType}\n");
        }
    }

   public class Venue
    {
        public string VenueName { get; set; }
        public string Address { get; set; }

        public Venue() { }
        public Venue(string venueName, string address)
        {
            this.VenueName = venueName;
            this.Address = address;
        }

        public void DisplayVenueDetails()
        {
            Console.WriteLine($"Venue: {this.VenueName} | Address: {this.Address}");
        }
    }

    // Customer Class
   public class Customer
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Customer() { }
        public Customer(string customerName, string email, string phoneNumber)
        {
            this.CustomerName = customerName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public void DisplayCustomerDetails()
        {
            Console.WriteLine($"Customer: {this.CustomerName} | Email: {this.Email} | Phone: {this.PhoneNumber}\n");
        }
    }

    // Booking Class
  public class Booking
    {
        private Event eventDetails;
        public int NumberOfTickets;
        public decimal TotalCost;

        public Booking(Event e)
        {
            eventDetails = e;
            NumberOfTickets = 0;
            TotalCost = 0;
        }

        public bool book_tickets(int numTickets)
        {
            if (eventDetails.BookTickets(numTickets))
            {
                NumberOfTickets += numTickets;
                calculate_booking_cost(NumberOfTickets);
                return true;
            }
            return false;
        }

        public void cancel_booking(int numTickets)
        {
            eventDetails.CancelBooking(numTickets);
            NumberOfTickets -= numTickets;
            if (NumberOfTickets < 0) NumberOfTickets = 0;
            calculate_booking_cost(NumberOfTickets);
        }

        public void calculate_booking_cost(int numTickets)
        {
            TotalCost = numTickets * eventDetails.TicketPrice;
        }

        public int GetAvailableNoOfTickets()
        {
            return eventDetails.AvailableSeats;
        }

        public void GetEventDetails()
        {
            eventDetails.DisplayEventDetails();
        }
    }

   
    public class Task4
    {
        static void Main()
        {
            // Creating an Event
            Event concertEvent = new Event("Rock Concert", DateTime.Now.AddDays(5), "7:00 PM", "Stadium XYZ", 100, 500.00m, "Concert");
            concertEvent.DisplayEventDetails();

            // Creating Customer
            Customer customer = new Customer("Arun", "arun@gmail.com", "1234567890");
            customer.DisplayCustomerDetails();

            // Booking Tickets
            Booking booking = new Booking(concertEvent);
            Console.Write("Enter number of tickets to book: ");
            int numTickets = int.Parse(Console.ReadLine());

            if (booking.book_tickets(numTickets))
            {
                Console.WriteLine($"Booking successful! Total cost: {booking.TotalCost:C}");
            }
            else
            {
                Console.WriteLine("Booking failed!");
            }

            Console.WriteLine($"Available Tickets after booking: {booking.GetAvailableNoOfTickets()}");
            Console.WriteLine($"Total Booked Tickets: {concertEvent.GetBookedNoOfTickets()}");
        }
    }
}
