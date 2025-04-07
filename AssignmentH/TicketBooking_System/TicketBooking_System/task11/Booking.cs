// File: entity/Booking.cs
// ==========================

namespace task11.entity
{
    public class Booking
    {
        public int booking_id { get; set; }
        public int customer_id { get; set; }
        public int event_id { get; set; }
        public int num_tickets { get; set; }
        public decimal total_cost { get; set; }
        public DateTime booking_date { get; set; }
    }
}