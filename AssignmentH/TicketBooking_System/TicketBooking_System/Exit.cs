using System;

class Exit
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nTicket Categories: Silver, Gold, Diamond");
            Console.Write("Enter ticket type (or type 'Exit' to quit): ");
            string ticketType = Console.ReadLine();

            if (ticketType == "Exit" || ticketType == "exit")
            {
                Console.WriteLine("Thank you for using the Ticket Booking System!");
                break;
            }

            Console.Write("Enter the number of tickets to book: ");
            bool isValidNumber = int.TryParse(Console.ReadLine(), out int numberOfTickets);

            // Separate conditions
            if (!isValidNumber)
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                continue;
            }
            if (numberOfTickets <= 0)
            {
                Console.WriteLine("Number of tickets must be greater than 0.");
                continue;
            }

            int ticketPrice = 0;

            if (ticketType == "Silver" || ticketType == "silver")
            {
                ticketPrice = 100;
            }
            else if (ticketType == "Gold" || ticketType == "gold")
            {
                ticketPrice = 200;
            }
            else if (ticketType == "Diamond" || ticketType == "diamond")
            {
                ticketPrice = 500;
            }
            else
            {
                Console.WriteLine("Invalid ticket type! Please choose from Silver, Gold, or Diamond.");
                continue;
            }

            int totalCost = ticketPrice * numberOfTickets;
            Console.WriteLine($"Total cost for {numberOfTickets} {ticketType} ticket(s): {totalCost} Rs");
        }
    }
}
