// File: entity/Event.cs
// ==========================

namespace task11.entity
{
    public abstract class Event
    {
        public int event_id { get; set; }
        public string event_name { get; set; }
        public DateTime event_date { get; set; }
        public TimeSpan event_time { get; set; }
        public int venue_id { get; set; }
        public int total_seats { get; set; }
        public int available_seats { get; set; }
        public decimal ticket_price { get; set; }

        public abstract string GetEventType();
    }
}