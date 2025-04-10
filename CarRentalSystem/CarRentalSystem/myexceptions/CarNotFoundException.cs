namespace CarRentalSystem.myexceptions
{
    using System;

    public class CarNotFoundException : Exception
    {
        public CarNotFoundException() : base("Car not found with the given ID.") { }

        public CarNotFoundException(string message) : base(message) { }
    }
}
