namespace CarRentalSystem.myexceptions
{
    using System;

    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base("Customer not found with the given ID.") { }

        public CustomerNotFoundException(string message) : base(message) { }
    }
}
