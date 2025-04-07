using System;

class TicketCategory
{
    static void Main()
    {
        Console.WriteLine("Ticket Categories: Silver, Gold, Diamond");
        Console.Write("Enter ticket type: ");
        string ticketType = Console.ReadLine().ToLower();

        Console.Write("Enter the number of tickets to book: ");
        int numberOfTickets = int.Parse(Console.ReadLine());

        int ticketPrice = 0;

        if (ticketType == "silver")
        {
            ticketPrice = 100;
        }
        else if (ticketType == "gold")
        {
            ticketPrice = 200;
        }
        else if (ticketType == "diamond")
        {
            ticketPrice = 500;
        }
        else
        {
            Console.WriteLine("Invalid ticket type!");
            return;
        }

        int totalCost = ticketPrice * numberOfTickets;
        Console.WriteLine($"Total cost for {numberOfTickets} {ticketType} tickets: {totalCost} Rs");
    }
}
