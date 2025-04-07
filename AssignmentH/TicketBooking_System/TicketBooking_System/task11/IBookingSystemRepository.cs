// ==========================
// File: dao/IBookingSystemRepository.cs
// ==========================

using task11.entity;
using System.Collections.Generic;

namespace task11.dao
{
    public interface IBookingSystemRepository
    {
        void AddCustomer(Customer customer);
        List<Customer> GetAllCustomers();
        void DeleteCustomer(int customerId);
        void AddVenue(Venue venue);
        List<Venue> GetAllVenues();
        void DeleteVenue(int venueId);
        void AddEvent(Event e);
        List<Event> GetAllEvents();
        void DeleteEvent(int eventId);
        void AddBooking(Booking booking);
        List<Booking> GetAllBookings();

        
        void DeleteBooking(int bookingId);
    }
}
