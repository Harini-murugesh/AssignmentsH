using System;

class BookingSystem
{
    static void Main()
    {
        Console.Write("Enter the number of available tickets: ");
        int availableTickets = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of tickets to book: ");
        int noOfBookingTickets = int.Parse(Console.ReadLine());

        if (noOfBookingTickets <= availableTickets)
        {
            availableTickets -= noOfBookingTickets;
            Console.WriteLine($"Booking successful! Remaining tickets: {availableTickets}");
        }
        else
        {
            Console.WriteLine("Sorry, tickets unavailable!");
        }
    }
}
