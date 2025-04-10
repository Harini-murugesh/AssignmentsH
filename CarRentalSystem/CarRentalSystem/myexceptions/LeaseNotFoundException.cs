namespace CarRentalSystem.myexceptions
{
    using System;

    public class LeaseNotFoundException : Exception
    {
        public LeaseNotFoundException() : base("Lease not found with the given ID.") { }

        public LeaseNotFoundException(string message) : base(message) { }
    }
}
